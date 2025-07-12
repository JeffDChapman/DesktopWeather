using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class SettingsForm : Form
    {
        private weatherForm myParent;
        private string optWeatherURL;
        private string optNatlMapURL;
        private string optForecastPage;
        private string weatherURLdef = "https://forecast.weather.gov/data/obhistory/KVNY.html";
        private string natlMapURLdef = "https://www.wpc.ncep.noaa.gov/sfc/usfntsfcwbg.gif";
        private string forecastPagedef = "https://forecast.weather.gov/MapClick.php?x=264&y=129&site=lox&zmx=&zmy=&map_x=264&map_y=129";

        public SettingsForm(DesktopWeather.weatherForm Parent)
        {
            myParent = Parent;
            InitializeComponent();
            getParentSettings();
        }

        private void getParentSettings()
        {
            optWeatherURL = myParent.weatherURL;
            optNatlMapURL = myParent.natlMapURL;
            optForecastPage = myParent.forecastPage;
            tbGaugeSource.Text = optWeatherURL;
            tbNationalMap.Text = optNatlMapURL;
            tbForecastSource.Text = optForecastPage;
            this.Refresh();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            RegistryKey ThisUser = Registry.CurrentUser;
            RegistryKey weatherURL = ThisUser.CreateSubKey("Software\\DesktopWeather\\weatherURL");
            weatherURL.SetValue("URLText", tbGaugeSource.Text);
            RegistryKey natlMapURL = ThisUser.CreateSubKey("Software\\DesktopWeather\\natlMapURL");
            natlMapURL.SetValue("URLText", tbNationalMap.Text);
            RegistryKey forecastPage = ThisUser.CreateSubKey("Software\\DesktopWeather\\forecastPage");
            forecastPage.SetValue("URLText", tbForecastSource.Text);

            myParent.restartProgramFlag = true;
            myParent.tmrStartup.Enabled = true;
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbGaugeSource.Text = weatherURLdef;
            tbNationalMap.Text = natlMapURLdef;
            tbForecastSource.Text = forecastPagedef;
        }
    }
}
