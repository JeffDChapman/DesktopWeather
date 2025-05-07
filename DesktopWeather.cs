using HeadlessBrowser;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class weatherForm : Form
    {
        #region private variables
        private WebBroForm myWebBrowser;
        private WebBrowser returnedWebPage;
        private string browseStatus;
        private int humidityValue = 100;
        private double pressureValue = 29.95;
        private string windText;
        private List<string> directionRotation = new List<string>
            {"N", "NE", "E", "SE", "S", "SW", "W", "NW"};
        private int windDirIndex;
        private int windValue = 0;
        private DateTime lastDataFetch;
        private string returnedAddr;
        private int temperatureValue = 70;
        private bool hadAforceStop = false;
        private tinyDisplay myTinyDisplay = new tinyDisplay();
        private Forecast myForecastForm = new Forecast(true);
        private bool computerRestarted = false;
        private DateTime last30Ticker;
        private int retryOnRestore = 0;
        private DateTime lastNewDay;
        private DateTime checkTheTime;
        private DateTime lastWebRefresh;
        private DateTime lastForecast = DateTime.MinValue;
        #endregion

        public bool weAreOffline = false;
        public bool itsBeenAday = false;
        public bool restartProgramFlag = false;
        public string weatherURL = "https://forecast.weather.gov/data/obhistory/KVNY.html";
        public string natlMapURL = "https://www.wpc.ncep.noaa.gov/sfc/usfntsfcwbg.gif";
        public string forecastPage = "https://forecast.weather.gov/MapClick.php?x=264&y=129&site=lox&zmx=&zmy=&map_x=264&map_y=129";

        public weatherForm()
        {
            InitializeComponent();
            pbNatlWeather.ImageLocation = natlMapURL;
            DrawTemperature();
            DrawHumidity();
            DrawPressure();
            DrawWind();
            myTinyDisplay = new tinyDisplay(this);
            myTinyDisplay.Show();
            lastDataFetch = DateTime.Now;
            lastNewDay = DateTime.Now;
            last30Ticker = DateTime.Now;
            tmrStartup.Enabled = true;
            this.Height = 550;
        }

        private void DrawTemperature()
        {
            int tmoSize = this.tmoBackgd.Height;
            int newTempHeight = tmoSize * (temperatureValue - 20) / 100;
            this.thermometer.Top = tmoBackgd.Bottom - newTempHeight;
            this.thermometer.Height = newTempHeight;
            this.lblTemperature.Text = temperatureValue.ToString();
            this.lblTemperature.Top = thermometer.Top - 10;
        }

        private void DrawWind()
        {
            pieWind.Series = new ISeries[]
                {
                    new PieSeries<int> { Values = new int[] { 5 } }
                };
            pieWind.InitialRotation = -95 + windValue;
        }

        private void DrawPressure()
        {
            this.piePressure.Series =
                GaugeGenerator.BuildSolidGauge(
                new GaugeItem(
                    pressureValue,   // the gauge value
                    series =>       // the series style
                    {
                        series.MaxRadialColumnWidth = 14;
                        series.DataLabelsSize = 12;
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.ChartCenter;
                        series.DataLabelsPaint = new SolidColorPaint(SKColors.White);
                        series.Fill = new SolidColorPaint(SKColors.DarkGreen);
                        series.CornerRadius = 2;
                    })
                );
        }

        private void DrawHumidity()
        {
            this.pieHumidity.Series =
                GaugeGenerator.BuildSolidGauge(
                new GaugeItem(
                    humidityValue,   // the gauge value
                    series =>       // the series style
                    {
                        series.MaxRadialColumnWidth = 14;
                        series.DataLabelsSize = 12;
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.End;
                        series.DataLabelsPaint = new SolidColorPaint(SKColors.White);
                        series.Fill = new SolidColorPaint(SKColors.Blue);
                        series.CornerRadius = 2;
                    })
                );
        }

        internal void BrowserIsFinished()
        {
            browseStatus = myWebBrowser.CurrentStatus;
            if (browseStatus != "Ready")
            {
                DisplayStatus("Offline: " + browseStatus);
                weAreOffline = true;
                return;
            }
            returnedWebPage = myWebBrowser.myBrowser;
            returnedAddr = myWebBrowser.myAddrBar.Text;

            tmrForceStopBrowser.Enabled = false;
            this.lblStatusBox.Visible = false;
            this.Refresh();
            Application.DoEvents();
            weAreOffline = false;

            try { ParseValuesFrom(returnedWebPage.DocumentText.ToString()); }
            catch
            {
                weAreOffline = true;
                if (hadAforceStop)
                {
                    restartProgramFlag = true;
                    tmrStartup.Enabled = true;
                    DisplayStatus("Conx Err, Restarting");
                    return;
                }
                DisplayStatus("Parse Error");
                return;
            }

            lastWebRefresh = DateTime.Now;
            UpdateGaugeDisplays();
        }

        public void UpdateGaugeDisplays()
        {
            computerRestarted = false;
            if (WindowState == FormWindowState.Normal)
            {
                lblLastUpdate.Text = lastWebRefresh.TimeOfDay.ToString().Substring(0,5);
                lblWindBot.Visible = false;
                lblWindTop.Visible = false;
                if ((windDirIndex > 2) && (windDirIndex < 6))
                {
                    lblWindTop.Visible = true;
                    lblWindTop.Text = windText;
                }
                else
                {
                    lblWindBot.Visible = true;
                    lblWindBot.Text = windText;
                }
                DrawTemperature();
                DrawHumidity();
                DrawPressure();
                DrawWind();
            }
            myTinyDisplay.tinyTempValue = temperatureValue;
            myTinyDisplay.humidityValue = humidityValue;
            myTinyDisplay.pressureValue = pressureValue;
            myTinyDisplay.windValue = windValue;
            myTinyDisplay.RedrawTiny();
        }

        private void ParseValuesFrom(string webPageData)
        {
            int dataTabStart = webPageData.IndexOf("<TBODY>");
            string weatherData = webPageData.Substring(dataTabStart, 400);
            string weatherData2 = weatherData.Replace("</TD>", "");
            weatherData = weatherData2.Replace("<TD>", "*");
            weatherData2 = weatherData.Replace("\r\n", "");
            string[] cellValues = weatherData2.Split('*');

            double tempAsGiven = Convert.ToDouble(cellValues[5]);
            temperatureValue = Convert.ToInt16(tempAsGiven);
            double dewpoint = Convert.ToDouble(cellValues[6]);
            humidityValue = Convert.ToInt16(CalculateRelativeHumidity(70, dewpoint));
            if (temperatureValue > 75)
                { humidityValue = Convert.ToInt16(CalculateRelativeHumidity(75, dewpoint)); }
            pressureValue = Convert.ToDouble(cellValues[12]);

            windText = cellValues[2].Trim();
            string[] windValues = windText.Split(' ');
            windDirIndex = directionRotation.IndexOf(windValues[0]);
            windValue = windDirIndex * 45;
        }

        public static double CalculateRelativeHumidity(double temperatureFahrenheit, double dewPointFahrenheit)
        {
            // Convert Fahrenheit to Celsius
            double temperatureCelsius = (temperatureFahrenheit - 32) * 5 / 9;
            double dewPointCelsius = (dewPointFahrenheit - 32) * 5 / 9;

            // Use the August-Roche-Magnus formula
            double saturationVaporPressure = 6.1094 * Math.Exp((17.625 * dewPointCelsius) / (dewPointCelsius + 243.04));
            double actualVaporPressure = 6.1094 * Math.Exp((17.625 * temperatureCelsius) / (temperatureCelsius + 243.04));

            // Calculate relative humidity
            return (saturationVaporPressure / actualVaporPressure ) * 100;
        }

        private void tmr30Seconds_Tick(object sender, EventArgs e)
        {
            checkTheTime = DateTime.Now;
            if (restartProgramFlag) 
            { 
                tmrStartup.Enabled = true;
                return; 
            }
            HasItBeenADay();
            if (!weAreOffline && itsBeenAday && (WindowState == FormWindowState.Normal)) 
            { 
                restartProgramFlag = true;
                tmrStartup.Enabled = true;
                return; 
            }
            if (weAreOffline && (WindowState == FormWindowState.Normal))
            { 
                tryGettingData();
                last30Ticker = checkTheTime;
                return;
            }
            if ((computerRestarted) && (retryOnRestore < 5)) 
            {
                tryGettingData();
                retryOnRestore++;
                last30Ticker = checkTheTime;
                return;
            }
            TimeSpan timeElapsedSinceCheck = checkTheTime - last30Ticker;
            if (timeElapsedSinceCheck.TotalMinutes > 2) 
            { 
                computerRestarted = true; 
                retryOnRestore = 0;
                weAreOffline = true;
            }
            last30Ticker = checkTheTime;
            HasItBeenTwentyMins();
        }

        private void HasItBeenADay()
        {
            // causes restart between 4 and 10 a.m.
            TimeSpan fullDayCheck = checkTheTime - lastNewDay;
            if (fullDayCheck.TotalHours < 8) { return; }
            if (checkTheTime.Hour < 4) { return; }
            if (checkTheTime.Hour > 10) { return; }
            itsBeenAday = true;
        }

        private void tryGettingData()
        {
            lastDataFetch = DateTime.Now;
            DisplayStatus("Retrieving Data...");
            myWebBrowser = new WebBroForm(this);
            browseStatus = "";
            hadAforceStop = false;
            tmrForceStopBrowser.Enabled = true;
            try { myWebBrowser.myBrowser.Navigate(weatherURL); }
            catch
            {
                DisplayStatus("Offline");
                weAreOffline = true;
            }
        }

        private void DisplayStatus(string theStatus)
        {
            this.lblStatusBox.Text = theStatus;
            this.lblStatusBox.Visible = true;
            this.Refresh();
            Application.DoEvents();
        }

        private void tmrStartup_Tick(object sender, EventArgs e)
        {
            if (restartProgramFlag) { Application.Restart(); return; }
            tmrStartup.Enabled = false;
            tryGettingData();
        }

        private void tmrForceStopBrowser_Tick(object sender, EventArgs e)
        {
            tmrForceStopBrowser.Enabled = false;
            hadAforceStop = true;
            myWebBrowser.processAforceStop();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_RESTORE = 0xF120;

            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_RESTORE)
                { HasItBeenTwentyMins(); }
            base.WndProc(ref m);
        }

        private void HasItBeenTwentyMins()
        {
            TimeSpan timeElapsedSinceCheck = checkTheTime - lastDataFetch;
            if (timeElapsedSinceCheck.TotalMinutes > 19) { tryGettingData(); }
        }

        private void weatherForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized) 
                { myTinyDisplay.Visible = true; }
        }

        private void btnForecast_Click(object sender, EventArgs e)
        {
            TimeSpan timeElapsedSinceCheck = DateTime.Now - lastForecast;
            if (timeElapsedSinceCheck.TotalMinutes > 180)
            { 
                lastForecast = DateTime.Now;
                myForecastForm = new Forecast(forecastPage);
                myForecastForm.ShowDialog();
            }
            else { myForecastForm.ShowDialog(); }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm mySettings = new SettingsForm(this);
            mySettings.ShowDialog();
            return;
        }
    }
}
