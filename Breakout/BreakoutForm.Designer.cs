namespace Breakout
{
    partial class BreakoutForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BreakoutForm));
            this.ballPicBox = new System.Windows.Forms.PictureBox();
            this.platformPicBox = new System.Windows.Forms.PictureBox();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.MyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ballPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // ballPicBox
            // 
            this.ballPicBox.Location = new System.Drawing.Point(290, 5);
            this.ballPicBox.Name = "ballPicBox";
            this.ballPicBox.Size = new System.Drawing.Size(20, 20);
            this.ballPicBox.TabIndex = 0;
            this.ballPicBox.TabStop = false;
            // 
            // platformPicBox
            // 
            this.platformPicBox.Location = new System.Drawing.Point(254, 588);
            this.platformPicBox.Name = "platformPicBox";
            this.platformPicBox.Size = new System.Drawing.Size(75, 20);
            this.platformPicBox.TabIndex = 1;
            this.platformPicBox.TabStop = false;
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 1;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(492, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "print";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MyChart
            // 
            chartArea1.Name = "ChartArea1";
            this.MyChart.ChartAreas.Add(chartArea1);
            this.MyChart.Location = new System.Drawing.Point(26, 43);
            this.MyChart.Name = "MyChart";
            this.MyChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Name = "Series1";
            this.MyChart.Series.Add(series1);
            this.MyChart.Size = new System.Drawing.Size(284, 200);
            this.MyChart.TabIndex = 4;
            this.MyChart.Text = "chart1";
            // 
            // BreakoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(599, 611);
            this.Controls.Add(this.MyChart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.platformPicBox);
            this.Controls.Add(this.ballPicBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BreakoutForm";
            this.Text = "Breakout";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ballPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.platformPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ballPicBox;
        private System.Windows.Forms.PictureBox platformPicBox;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart MyChart;
    }
}

