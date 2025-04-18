using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class weatherForm : Form
    {
        private double humidityValue = 100;
        private bool weAreOffline = false;
        private double pressureValue = 30.5;

        public weatherForm()
        {
            InitializeComponent();
            DrawHumidity();
            DrawPressure();
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
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.End;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
