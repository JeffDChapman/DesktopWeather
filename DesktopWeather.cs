using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class weatherForm : Form
    {
        private double humidityValue;

        public weatherForm()
        {
            InitializeComponent();
            DrawHumidity();
        }

        private void DrawHumidity()
        {
            humidityValue = 100;

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
                        series.CornerRadius = 2;
                    }
                    ));
        }

        internal void BrowserIsFinished()
        {
            throw new NotImplementedException();
        }
    }
}
