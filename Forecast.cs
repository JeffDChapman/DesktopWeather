using DesktopWeather.Properties;
using HeadlessBrowser;
using System;
using System.Collections;
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
        private ArrayList savedPeriods = new ArrayList();

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
            clearImageBorders();
            string fullImageLoc;

            parsedPeriod firstWeather = (parsedPeriod)savedPeriods[0];
            label1.Text = firstWeather.periodName;
            fullImageLoc = getLocalImage(firstWeather.weatherImage); 
            pbToday.ImageLocation = fullImageLoc;
            pbToday.Invalidate();
            pbToday.Tag = firstWeather.extendedDesc;

            parsedPeriod secondWeather = (parsedPeriod)savedPeriods[1];
            label2.Text = secondWeather.periodName;
            fullImageLoc = getLocalImage(secondWeather.weatherImage);
            pbTonight.ImageLocation = fullImageLoc;
            pbTonight.Invalidate();
            pbTonight.Tag = secondWeather.extendedDesc;

            parsedPeriod thirdWeather = (parsedPeriod)savedPeriods[2];
            if (thirdWeather.periodName.Contains("Night"))
                { thirdWeather = (parsedPeriod)savedPeriods[3]; }    
            label3.Text = thirdWeather.periodName;
            fullImageLoc = getLocalImage(thirdWeather.weatherImage);
            pbTomorrow.ImageLocation = fullImageLoc;
            pbTomorrow.Invalidate();
            pbTomorrow.Tag = thirdWeather.extendedDesc;

            parsedPeriod fourthWeather = (parsedPeriod)savedPeriods[4];
            if (fourthWeather.periodName.Contains("Night"))
                { fourthWeather = (parsedPeriod)savedPeriods[5]; }
            label4.Text = fourthWeather.periodName;
            fullImageLoc = getLocalImage(fourthWeather.weatherImage);
            pbNextDay.ImageLocation = fullImageLoc;
            pbNextDay.Invalidate();
            pbNextDay.Tag = fourthWeather.extendedDesc;

            rtbForecast.Text = firstWeather.extendedDesc;
            this.Refresh();
            Application.DoEvents();
            bool debugstop = true;
        }

        private string getLocalImage(string weatherImage)
        {
            return "";
        }

        private void clearImageBorders()
        {
            return;
        }

        private void ParseValuesFrom(string NoaaWeather)
        {
            string periodData = NoaaWeather;
            for (int i = 0; i < 6; i++)
            {
                parsedPeriod nextPeriodStruct = GetNextPeriod(periodData);
                savedPeriods.Add(nextPeriodStruct);
                periodData = nextPeriodStruct.restOfData;
            }
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
