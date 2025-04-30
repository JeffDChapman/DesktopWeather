using HeadlessBrowser;
using System;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class Forecast : Form
    {
        private string forecastPage = "https://forecast.weather.gov/MapClick.php?x=264&y=129&site=lox&zmx=&zmy=&map_x=264&map_y=129";
        private WebBroForm myWebBrowser;
        private bool hadAforceStop;
        private string browseStatus;
        private WebBrowser returnedWebPage;
        private string returnedAddr;
        private struct parsedPeriod
        {
            public string periodName;
            public string weatherImage;
            public string extendedDesc;
            public string restOfData;
        }

        public Forecast()
        {
            InitializeComponent();
            try { loadForeCastPage(); }
            catch
            {
                this.rtbForecast.Text = "Error Loading NOAA Web Page";
                this.Refresh();
                return;
            }
        }

        private void loadForeCastPage()
        {
            myWebBrowser = new WebBroForm(this);
            hadAforceStop = false;
            //tmrForceStopBrowser.Enabled = true;
            try { myWebBrowser.myBrowser.Navigate(forecastPage); }
            catch
            {
                this.rtbForecast.Text = "Error Loading NOAA Web Page";
                this.Refresh();
            }
        }

        internal void BrowserIsFinished()
        {
            browseStatus = myWebBrowser.CurrentStatus;
            if (browseStatus != "Ready")
            {
                this.rtbForecast.Text = "Error Loading NOAA Web Page";
                return;
            }
            returnedWebPage = myWebBrowser.myBrowser;
            returnedAddr = myWebBrowser.myAddrBar.Text;

            Application.DoEvents();

            try { ParseValuesFrom(returnedWebPage.DocumentText.ToString()); }
            catch
            {
                if (hadAforceStop)
                {
                    return;
                }
                this.rtbForecast.Text = "Error Parsing NOAA Web Page";
                return;
            }

            UpdateForecasts();
        }

        private void UpdateForecasts()
        {
            return;
        }

        private void ParseValuesFrom(string NoaaWeather)
        {
            string periodData = NoaaWeather;
            parsedPeriod nextPeriodStruct = GetNextPeriod(periodData);
            bool debugstop = true;
        }

        private static parsedPeriod GetNextPeriod(string periodData)
        {
            string findSeperator = "period-name>";
            int nextSep = periodData.IndexOf(findSeperator);
            string carryOn = periodData.Substring(nextSep + 12);
            int endOfPeriod = carryOn.IndexOf("<");
            string periodName = carryOn.Substring(0, endOfPeriod);
            string findDesc = "IGM title=\"";
            int nextDesc = carryOn.IndexOf(findDesc);
            string carryOn2 = carryOn.Substring(nextDesc + 11);
            int endOfDesc = carryOn2.IndexOf("\"");
            string extendedDesc = carryOn2.Substring(0, endOfDesc);
            string findImage = "src=\"";
            int nextImg = carryOn2.IndexOf(findImage);
            string carryOn3 = carryOn2.Substring(nextImg + 5);
            int endOfImg = carryOn3.IndexOf("\"");
            string weatherImage = carryOn3.Substring(0, endOfImg);
            string restOfData = carryOn3;

            parsedPeriod myPeriod = new parsedPeriod();
            myPeriod.periodName = periodName;
            myPeriod.weatherImage = weatherImage;
            myPeriod.extendedDesc = extendedDesc;
            myPeriod.restOfData = restOfData;
            return myPeriod;
        }
    }
}
