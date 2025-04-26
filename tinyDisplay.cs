using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Forms;

namespace DesktopWeather
{
    public partial class tinyDisplay : Form
    {
        public int tinyTempValue;
        public int windValue;
        public double pressureValue;
        public double humidityValue;
        private weatherForm myparent;

        public tinyDisplay()
        {
            InitializeComponent();
        }

        public tinyDisplay(weatherForm parent)
        {
            InitializeComponent();
            myparent = parent;
        }

        private void DrawTemperature()
        {
            int tmoSize = this.tmoTinyBackgd.Height;
            int newTempHeight = tmoSize * (tinyTempValue - 20) / 100;
            this.tinythermometer.Top = tmoTinyBackgd.Bottom - newTempHeight;
            this.tinythermometer.Height = newTempHeight;
        }

        private void DrawWind()
        {
            pieTinyWind.Series = new ISeries[]
                {
                    new PieSeries<int> { Values = new int[] { 5 } }
                };
            pieTinyWind.InitialRotation = -95 + windValue;
        }

        private void DrawPressure()
        {
            this.pieTinyPressure.Series =
                GaugeGenerator.BuildSolidGauge(
                new GaugeItem(
                    pressureValue,   // the gauge value
                    series =>       // the series style
                    {
                        series.MaxRadialColumnWidth = 8;
                        series.DataLabelsSize = 8;
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.ChartCenter;
                        series.DataLabelsPaint = new SolidColorPaint(SKColors.White);
                        series.Fill = new SolidColorPaint(SKColors.DarkGreen);
                        series.CornerRadius = 1;
                    })
                );
        }

        private void DrawHumidity()
        {
            this.pieTinyHumidity.Series =
                GaugeGenerator.BuildSolidGauge(
                new GaugeItem(
                    humidityValue,   // the gauge value
                    series =>       // the series style
                    {
                        series.MaxRadialColumnWidth = 8;
                        series.DataLabelsSize = 8;
                        series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.End;
                        series.DataLabelsPaint = new SolidColorPaint(SKColors.White);
                        series.Fill = new SolidColorPaint(SKColors.Blue);
                        series.CornerRadius = 1;
                    })
                );
        }

        public void RedrawTiny()
        {
            DrawTemperature();
            DrawHumidity();
            DrawPressure();
            DrawWind();
        }

        private void tinyDisplay_Click(object sender, System.EventArgs e)
        {
            this.Visible = false;
            if (!myparent.weAreOffline && myparent.itsBeenAday)
                { Application.Restart(); return; }
            myparent.WindowState = FormWindowState.Normal;
            myparent.pbNatlWeather.Invalidate();
            if (myparent.weAreOffline) { myparent.tmrStartup.Enabled = true; }
                else { myparent.UpdateGaugeDisplays(); }
        }

        public class TransparentPanel : Panel
        {
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                    return cp;
                }
            }
            protected override void OnPaintBackground(PaintEventArgs e)
            {
                //base.OnPaintBackground(e);
            }
        }
    }
}
