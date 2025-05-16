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
        }
    }
}
