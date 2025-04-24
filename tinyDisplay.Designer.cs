namespace DesktopWeather
{
    partial class tinyDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pieTinyHumidity = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.pieTinyPressure = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.pieTinyWind = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.tmoTinyBackgd = new System.Windows.Forms.Label();
            this.tinythermometer = new System.Windows.Forms.Label();
            this.pnlCatchClick = new DesktopWeather.tinyDisplay.TransparentPanel();
            this.SuspendLayout();
            // 
            // pieTinyHumidity
            // 
            this.pieTinyHumidity.InitialRotation = -225D;
            this.pieTinyHumidity.IsClockwise = true;
            this.pieTinyHumidity.Location = new System.Drawing.Point(32, -11);
            this.pieTinyHumidity.Margin = new System.Windows.Forms.Padding(0);
            this.pieTinyHumidity.MaxAngle = 360D;
            this.pieTinyHumidity.MaxValue = 130D;
            this.pieTinyHumidity.MinValue = 0D;
            this.pieTinyHumidity.Name = "pieTinyHumidity";
            this.pieTinyHumidity.Size = new System.Drawing.Size(150, 150);
            this.pieTinyHumidity.TabIndex = 0;
            this.pieTinyHumidity.Click += new System.EventHandler(this.tinyDisplay_Click);
            // 
            // pieTinyPressure
            // 
            this.pieTinyPressure.InitialRotation = -225D;
            this.pieTinyPressure.IsClockwise = true;
            this.pieTinyPressure.Location = new System.Drawing.Point(149, -9);
            this.pieTinyPressure.Margin = new System.Windows.Forms.Padding(0);
            this.pieTinyPressure.MaxAngle = 360D;
            this.pieTinyPressure.MaxValue = 30.8D;
            this.pieTinyPressure.MinValue = 29.5D;
            this.pieTinyPressure.Name = "pieTinyPressure";
            this.pieTinyPressure.Size = new System.Drawing.Size(150, 150);
            this.pieTinyPressure.TabIndex = 1;
            this.pieTinyPressure.Click += new System.EventHandler(this.tinyDisplay_Click);
            // 
            // pieTinyWind
            // 
            this.pieTinyWind.InitialRotation = -95D;
            this.pieTinyWind.IsClockwise = true;
            this.pieTinyWind.Location = new System.Drawing.Point(266, -9);
            this.pieTinyWind.Margin = new System.Windows.Forms.Padding(0);
            this.pieTinyWind.MaxAngle = 360D;
            this.pieTinyWind.MaxValue = 130D;
            this.pieTinyWind.MinValue = 0D;
            this.pieTinyWind.Name = "pieTinyWind";
            this.pieTinyWind.Size = new System.Drawing.Size(150, 150);
            this.pieTinyWind.TabIndex = 2;
            this.pieTinyWind.Click += new System.EventHandler(this.tinyDisplay_Click);
            // 
            // tmoTinyBackgd
            // 
            this.tmoTinyBackgd.BackColor = System.Drawing.Color.DarkBlue;
            this.tmoTinyBackgd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmoTinyBackgd.Location = new System.Drawing.Point(12, 12);
            this.tmoTinyBackgd.Name = "tmoTinyBackgd";
            this.tmoTinyBackgd.Size = new System.Drawing.Size(22, 92);
            this.tmoTinyBackgd.TabIndex = 3;
            // 
            // tinythermometer
            // 
            this.tinythermometer.BackColor = System.Drawing.Color.Red;
            this.tinythermometer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tinythermometer.Location = new System.Drawing.Point(12, 53);
            this.tinythermometer.Name = "tinythermometer";
            this.tinythermometer.Size = new System.Drawing.Size(22, 51);
            this.tinythermometer.TabIndex = 4;
            // 
            // pnlCatchClick
            // 
            this.pnlCatchClick.Location = new System.Drawing.Point(38, 3);
            this.pnlCatchClick.Name = "pnlCatchClick";
            this.pnlCatchClick.Size = new System.Drawing.Size(365, 114);
            this.pnlCatchClick.TabIndex = 5;
            this.pnlCatchClick.Click += new System.EventHandler(this.tinyDisplay_Click);
            // 
            // tinyDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 121);
            this.ControlBox = false;
            this.Controls.Add(this.pnlCatchClick);
            this.Controls.Add(this.tinythermometer);
            this.Controls.Add(this.tmoTinyBackgd);
            this.Controls.Add(this.pieTinyWind);
            this.Controls.Add(this.pieTinyPressure);
            this.Controls.Add(this.pieTinyHumidity);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(825, 0);
            this.Name = "tinyDisplay";
            this.Opacity = 0.6D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "tinyDisplay";
            this.Click += new System.EventHandler(this.tinyDisplay_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieTinyHumidity;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieTinyPressure;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieTinyWind;
        private System.Windows.Forms.Label tmoTinyBackgd;
        private System.Windows.Forms.Label tinythermometer;
        // private System.Windows.Forms.Panel pnlCatchClick;
        private TransparentPanel pnlCatchClick;
    }
}