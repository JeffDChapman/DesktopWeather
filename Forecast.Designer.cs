namespace DesktopWeather
{
    partial class Forecast
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
            this.pbToday = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbTonight = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pbTomorrow = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbNextDay = new System.Windows.Forms.PictureBox();
            this.rtbForecast = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTonight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTomorrow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextDay)).BeginInit();
            this.SuspendLayout();
            // 
            // pbToday
            // 
            this.pbToday.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbToday.Location = new System.Drawing.Point(38, 55);
            this.pbToday.Name = "pbToday";
            this.pbToday.Size = new System.Drawing.Size(88, 88);
            this.pbToday.TabIndex = 0;
            this.pbToday.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Today";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(162, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tonight";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbTonight
            // 
            this.pbTonight.Location = new System.Drawing.Point(160, 55);
            this.pbTonight.Name = "pbTonight";
            this.pbTonight.Size = new System.Drawing.Size(88, 88);
            this.pbTonight.TabIndex = 2;
            this.pbTonight.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(283, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Tomorrow";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbTomorrow
            // 
            this.pbTomorrow.Location = new System.Drawing.Point(282, 55);
            this.pbTomorrow.Name = "pbTomorrow";
            this.pbTomorrow.Size = new System.Drawing.Size(88, 88);
            this.pbTomorrow.TabIndex = 4;
            this.pbTomorrow.TabStop = false;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(403, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Next Day";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbNextDay
            // 
            this.pbNextDay.Location = new System.Drawing.Point(404, 55);
            this.pbNextDay.Name = "pbNextDay";
            this.pbNextDay.Size = new System.Drawing.Size(88, 88);
            this.pbNextDay.TabIndex = 6;
            this.pbNextDay.TabStop = false;
            // 
            // rtbForecast
            // 
            this.rtbForecast.Location = new System.Drawing.Point(31, 169);
            this.rtbForecast.Name = "rtbForecast";
            this.rtbForecast.Size = new System.Drawing.Size(461, 269);
            this.rtbForecast.TabIndex = 8;
            this.rtbForecast.Text = "";
            // 
            // Forecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 469);
            this.Controls.Add(this.rtbForecast);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pbNextDay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbTomorrow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbTonight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbToday);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "Forecast";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forecast";
            ((System.ComponentModel.ISupportInitialize)(this.pbToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTonight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTomorrow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbNextDay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbToday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbTonight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbTomorrow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pbNextDay;
        private System.Windows.Forms.RichTextBox rtbForecast;
    }
}