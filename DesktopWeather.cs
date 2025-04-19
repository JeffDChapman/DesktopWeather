using HeadlessBrowser;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class weatherForm : Form
    {
        private WebBroForm myWebBrowser;
        private double humidityValue = 100;
        private bool weAreOffline = false;
        private double pressureValue = 30.5;
        private List<string> directionRotation = new List<string>
            {"N", "NE", "E", "SE", "S", "SW", "W", "NW"};
        private int windValue = 0;
        private string weatherURL = "https://forecast.weather.gov/data/obhistory/KVNY.html";
        private string browseStatus;
        private WebBrowser returnedWebPage;
        private string returnedAddr;

        public weatherForm()
        {
            InitializeComponent();
            DrawHumidity();
            DrawPressure();
            DrawWind();
            myWebBrowser = new WebBroForm(this);
            tmrStartup.Enabled = true;
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
                this.lblStatusBox.Text = "Offline: " + browseStatus;
                weAreOffline = true;
                return;
            }
            returnedWebPage = myWebBrowser.myBrowser;
            returnedAddr = myWebBrowser.myAddrBar.Text;
            ParseValuesFrom(returnedWebPage.DocumentText.ToString());
        }

        private void ParseValuesFrom(string webPageData)
        {
            this.lblStatusBox.Visible = false;
            weAreOffline = false;
            int dataTabStart = webPageData.IndexOf("<TBODY>");
            string weatherData = webPageData.Substring(dataTabStart, 400);
            string weatherData2 = weatherData.Replace("</TD>", "");
            weatherData = weatherData2.Replace("<TD>", "*");
            weatherData2 = weatherData.Replace("\r\n", "");
            string[] cellValues = weatherData2.Split('*');
            bool debugstop = true;
        }

        private void tmr30Seconds_Tick(object sender, EventArgs e)
        {
            if (weAreOffline) 
            { 
                tryGettingData();
                return;
            }
        }

        private void tryGettingData()
        {
            browseStatus = "";
            try { myWebBrowser.myBrowser.Navigate(weatherURL); }
            catch { }
        }

        private void tmrStartup_Tick(object sender, EventArgs e)
        {
            tmrStartup.Enabled = false;
            tryGettingData();
        }
    }
}
