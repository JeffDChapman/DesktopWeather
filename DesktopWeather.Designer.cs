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
            this.tmoBackgd = new System.Windows.Forms.Label();
            this.thermometer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pieHumidity = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmoBackgd
            // 
            this.tmoBackgd.BackColor = System.Drawing.Color.Blue;
            this.tmoBackgd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tmoBackgd.Location = new System.Drawing.Point(38, 22);
            this.tmoBackgd.Name = "tmoBackgd";
            this.tmoBackgd.Size = new System.Drawing.Size(35, 414);
            this.tmoBackgd.TabIndex = 0;
            // 
            // thermometer
            // 
            this.thermometer.BackColor = System.Drawing.Color.Red;
            this.thermometer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.thermometer.Location = new System.Drawing.Point(38, 223);
            this.thermometer.Name = "thermometer";
            this.thermometer.Size = new System.Drawing.Size(35, 213);
            this.thermometer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 419);
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
            // pieHumidity
            // 
            this.pieHumidity.BackColor = System.Drawing.Color.Black;
            this.pieHumidity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.pieHumidity.InitialRotation = -225D;
            this.pieHumidity.IsClockwise = true;
            this.pieHumidity.Location = new System.Drawing.Point(139, 46);
            this.pieHumidity.MaxAngle = 360D;
            this.pieHumidity.MaxValue = 130D;
            this.pieHumidity.MinValue = 0D;
            this.pieHumidity.Name = "pieHumidity";
            this.pieHumidity.Size = new System.Drawing.Size(200, 200);
            this.pieHumidity.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(139, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "Humidity";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // weatherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 618);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pieHumidity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.thermometer);
            this.Controls.Add(this.tmoBackgd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "weatherForm";
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
    }
}

