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
        #region variables
        private WebBroForm myWebBrowser;
        private WebBrowser returnedWebPage;
        private string browseStatus;
        private int humidityValue = 100;
        private bool weAreOffline = false;
        private double pressureValue = 30.5;
        private List<string> directionRotation = new List<string>
            {"N", "NE", "E", "SE", "S", "SW", "W", "NW"};
        private int windValue = 0;
        private string weatherURL = "https://forecast.weather.gov/data/obhistory/KVNY.html";
        private DateTime lastDataFetch;
        private string returnedAddr;
        private int temperatureValue = 70;
        private bool hadAforceStop = false;
        private bool restartProgramFlag = false;
        #endregion

        public weatherForm()
        {
            InitializeComponent();
            DrawTemperature();
            DrawHumidity();
            DrawPressure();
            DrawWind();
            lastDataFetch = DateTime.Now;
            tmrStartup.Enabled = true;
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
            try { ParseValuesFrom(returnedWebPage.DocumentText.ToString()); }
            catch 
            {
                weAreOffline = true;
                if (hadAforceStop)
                {
                    restartProgramFlag = true;
                    DisplayStatus("Conx Err, Restarting");
                    return;
                }
                DisplayStatus("Parse Error");
            }
        }

        private void ParseValuesFrom(string webPageData)
        {
            tmrForceStopBrowser.Enabled = false;
            this.lblStatusBox.Visible = false;
            this.Refresh();
            Application.DoEvents();
            weAreOffline = false;
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
            pressureValue = Convert.ToDouble(cellValues[12]);
            string windText = cellValues[2].Trim();
            string[] windValues = windText.Split(' ');
            int windDirIndex = directionRotation.IndexOf(windValues[0]);
            windValue = windDirIndex * 45;
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
            if (WindowState == FormWindowState.Minimized) {return;}
            if (restartProgramFlag) { Application.Restart(); }
            if (weAreOffline) 
            { 
                tryGettingData();
                return;
            }
            HasItBeenTwentyMins();
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
            DateTime checkingTimeNow = DateTime.Now;
            TimeSpan timeElapsedSinceCheck = checkingTimeNow - lastDataFetch;
            if (timeElapsedSinceCheck.Minutes > 19) { tryGettingData(); }
        }
    }
}
