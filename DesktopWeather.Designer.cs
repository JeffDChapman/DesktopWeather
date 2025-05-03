namespace DesktopWeather
{
    partial class weatherForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(weatherForm));
            this.tmoBackgd = new System.Windows.Forms.Label();
            this.thermometer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStatusBox = new System.Windows.Forms.Label();
            this.tmr30Seconds = new System.Windows.Forms.Timer(this.components);
            this.tmrStartup = new System.Windows.Forms.Timer(this.components);
            this.lblTemperature = new System.Windows.Forms.Label();
            this.lblWindTop = new System.Windows.Forms.Label();
            this.lblWindBot = new System.Windows.Forms.Label();
            this.tmrForceStopBrowser = new System.Windows.Forms.Timer(this.components);
            this.pbNatlWeather = new System.Windows.Forms.PictureBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.btnForecast = new System.Windows.Forms.Button();
            this.pieWind = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.piePressure = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.pieHumidity = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            ((System.ComponentModel.ISupportInitialize)(this.pbNatlWeather)).BeginInit();
            this.SuspendLayout();
            // 
            // tmoBackgd
            // 
            this.tmoBackgd.BackColor = System.Drawing.Color.DarkBlue;
            this.tmoBackgd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmoBackgd.Location = new System.Drawing.Point(38, 17);
            this.tmoBackgd.Name = "tmoBackgd";
            this.tmoBackgd.Size = new System.Drawing.Size(35, 260);
            this.tmoBackgd.TabIndex = 0;
            // 
            // thermometer
            // 
            this.thermometer.BackColor = System.Drawing.Color.Red;
            this.thermometer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thermometer.Location = new System.Drawing.Point(38, 142);
            this.thermometer.Name = "thermometer";
            this.thermometer.Size = new System.Drawing.Size(35, 135);
            this.thermometer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "120";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(139, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "Humidity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(392, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 33);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pressure";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Black;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(646, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 33);
            this.label5.TabIndex = 9;
            this.label5.Text = "Wind";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStatusBox
            // 
            this.lblStatusBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatusBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusBox.Location = new System.Drawing.Point(264, 125);
            this.lblStatusBox.Name = "lblStatusBox";
            this.lblStatusBox.Size = new System.Drawing.Size(445, 64);
            this.lblStatusBox.TabIndex = 10;
            this.lblStatusBox.Text = "Retrieving Data...";
            this.lblStatusBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStatusBox.Visible = false;
            // 
            // tmr30Seconds
            // 
            this.tmr30Seconds.Enabled = true;
            this.tmr30Seconds.Interval = 30000;
            this.tmr30Seconds.Tick += new System.EventHandler(this.tmr30Seconds_Tick);
            // 
            // tmrStartup
            // 
            this.tmrStartup.Interval = 1500;
            this.tmrStartup.Tick += new System.EventHandler(this.tmrStartup_Tick);
            // 
            // lblTemperature
            // 
            this.lblTemperature.BackColor = System.Drawing.Color.DarkBlue;
            this.lblTemperature.ForeColor = System.Drawing.Color.White;
            this.lblTemperature.Location = new System.Drawing.Point(40, 115);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(32, 20);
            this.lblTemperature.TabIndex = 11;
            this.lblTemperature.Text = "70";
            this.lblTemperature.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWindTop
            // 
            this.lblWindTop.BackColor = System.Drawing.Color.Black;
            this.lblWindTop.ForeColor = System.Drawing.Color.White;
            this.lblWindTop.Location = new System.Drawing.Point(715, 80);
            this.lblWindTop.Name = "lblWindTop";
            this.lblWindTop.Size = new System.Drawing.Size(64, 20);
            this.lblWindTop.TabIndex = 12;
            this.lblWindTop.Text = "70";
            this.lblWindTop.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWindTop.Visible = false;
            // 
            // lblWindBot
            // 
            this.lblWindBot.BackColor = System.Drawing.Color.Black;
            this.lblWindBot.ForeColor = System.Drawing.Color.White;
            this.lblWindBot.Location = new System.Drawing.Point(715, 190);
            this.lblWindBot.Name = "lblWindBot";
            this.lblWindBot.Size = new System.Drawing.Size(64, 20);
            this.lblWindBot.TabIndex = 13;
            this.lblWindBot.Text = "70";
            this.lblWindBot.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblWindBot.Visible = false;
            // 
            // tmrForceStopBrowser
            // 
            this.tmrForceStopBrowser.Interval = 10000;
            this.tmrForceStopBrowser.Tick += new System.EventHandler(this.tmrForceStopBrowser_Tick);
            // 
            // pbNatlWeather
            // 
            this.pbNatlWeather.ImageLocation = "https://www.wpc.ncep.noaa.gov/sfc/usfntsfcwbg.gif";
            this.pbNatlWeather.Location = new System.Drawing.Point(88, 302);
            this.pbNatlWeather.Name = "pbNatlWeather";
            this.pbNatlWeather.Size = new System.Drawing.Size(670, 439);
            this.pbNatlWeather.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbNatlWeather.TabIndex = 14;
            this.pbNatlWeather.TabStop = false;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(793, 302);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(49, 20);
            this.lblLastUpdate.TabIndex = 15;
            this.lblLastUpdate.Text = "00:00";
            // 
            // btnForecast
            // 
            this.btnForecast.AutoSize = true;
            this.btnForecast.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnForecast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnForecast.Image = ((System.Drawing.Image)(resources.GetObject("btnForecast.Image")));
            this.btnForecast.Location = new System.Drawing.Point(778, 339);
            this.btnForecast.Name = "btnForecast";
            this.btnForecast.Size = new System.Drawing.Size(55, 55);
            this.btnForecast.TabIndex = 16;
            this.btnForecast.UseVisualStyleBackColor = true;
            this.btnForecast.Click += new System.EventHandler(this.btnForecast_Click);
            // 
            // pieWind
            // 
            this.pieWind.BackColor = System.Drawing.Color.Black;
            this.pieWind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pieWind.InitialRotation = -95D;
            this.pieWind.IsClockwise = true;
            this.pieWind.Location = new System.Drawing.Point(645, 37);
            this.pieWind.MaxAngle = 360D;
            this.pieWind.MaxValue = 130D;
            this.pieWind.MinValue = 0D;
            this.pieWind.Name = "pieWind";
            this.pieWind.Size = new System.Drawing.Size(200, 200);
            this.pieWind.TabIndex = 8;
            // 
            // piePressure
            // 
            this.piePressure.BackColor = System.Drawing.Color.Black;
            this.piePressure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.piePressure.InitialRotation = -225D;
            this.piePressure.IsClockwise = true;
            this.piePressure.Location = new System.Drawing.Point(392, 37);
            this.piePressure.MaxAngle = 360D;
            this.piePressure.MaxValue = 30.6D;
            this.piePressure.MinValue = 29.7D;
            this.piePressure.Name = "piePressure";
            this.piePressure.Size = new System.Drawing.Size(200, 200);
            this.piePressure.TabIndex = 6;
            // 
            // pieHumidity
            // 
            this.pieHumidity.BackColor = System.Drawing.Color.Black;
            this.pieHumidity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pieHumidity.InitialRotation = -225D;
            this.pieHumidity.IsClockwise = true;
            this.pieHumidity.Location = new System.Drawing.Point(139, 37);
            this.pieHumidity.MaxAngle = 360D;
            this.pieHumidity.MaxValue = 130D;
            this.pieHumidity.MinValue = 0D;
            this.pieHumidity.Name = "pieHumidity";
            this.pieHumidity.Size = new System.Drawing.Size(200, 200);
            this.pieHumidity.TabIndex = 4;
            // 
            // weatherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(878, 765);
            this.Controls.Add(this.btnForecast);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.pbNatlWeather);
            this.Controls.Add(this.lblWindBot);
            this.Controls.Add(this.lblWindTop);
            this.Controls.Add(this.lblTemperature);
            this.Controls.Add(this.lblStatusBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pieWind);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.piePressure);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pieHumidity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.thermometer);
            this.Controls.Add(this.tmoBackgd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "weatherForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Resize += new System.EventHandler(this.weatherForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbNatlWeather)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tmoBackgd;
        private System.Windows.Forms.Label thermometer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieHumidity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart piePressure;
        private System.Windows.Forms.Label label5;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart pieWind;
        private System.Windows.Forms.Label lblStatusBox;
        private System.Windows.Forms.Timer tmr30Seconds;
        public System.Windows.Forms.Timer tmrStartup;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.Label lblWindTop;
        private System.Windows.Forms.Label lblWindBot;
        private System.Windows.Forms.Timer tmrForceStopBrowser;
        public System.Windows.Forms.PictureBox pbNatlWeather;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Button btnForecast;
    }
}

