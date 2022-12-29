using System.Windows.Forms;

namespace Lab1
{
	partial class Benchmark
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if ( disposing && ( components != null ) )
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
		private void InitializeComponent ()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.overallProgress = new System.Windows.Forms.ProgressBar();
			this.localProgress = new System.Windows.Forms.ProgressBar();
			this.label12 = new System.Windows.Forms.Label();
			this.testsBox = new System.Windows.Forms.NumericUpDown();
			this.startButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.contrastTrackBar = new System.Windows.Forms.TrackBar();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Фотография 1";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(19, 41);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(359, 23);
			this.textBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(384, 41);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Выбор";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnPic1Select);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(384, 90);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 22;
			this.button2.Text = "Выбор";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnPic2Select);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(19, 90);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(359, 23);
			this.textBox2.TabIndex = 21;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 15);
			this.label2.TabIndex = 20;
			this.label2.Text = "Фотография 2";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(384, 137);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 25;
			this.button3.Text = "Выбор";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.OnPic3Select);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(19, 137);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(359, 23);
			this.textBox3.TabIndex = 24;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 119);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(84, 15);
			this.label5.TabIndex = 23;
			this.label5.Text = "Фотография 3";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(384, 187);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 28;
			this.button4.Text = "Выбор";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.OnPic4Select);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(19, 187);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(359, 23);
			this.textBox4.TabIndex = 27;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 169);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 15);
			this.label6.TabIndex = 26;
			this.label6.Text = "Фотография 4";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "csv";
			this.saveFileDialog1.Filter = "CSV|*csv";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(384, 259);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 32;
			this.button5.Text = "Обзор";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.OnOutputPath);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(19, 259);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(359, 23);
			this.textBox5.TabIndex = 31;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 241);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(172, 15);
			this.label7.TabIndex = 30;
			this.label7.Text = "Сохранить результаты в файл";
			// 
			// overallProgress
			// 
			this.overallProgress.Location = new System.Drawing.Point(18, 446);
			this.overallProgress.Name = "overallProgress";
			this.overallProgress.Size = new System.Drawing.Size(334, 23);
			this.overallProgress.TabIndex = 33;
			// 
			// localProgress
			// 
			this.localProgress.Location = new System.Drawing.Point(18, 418);
			this.localProgress.Name = "localProgress";
			this.localProgress.Size = new System.Drawing.Size(334, 23);
			this.localProgress.TabIndex = 34;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(358, 371);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(101, 15);
			this.label12.TabIndex = 36;
			this.label12.Text = "Кол-во повторов";
			this.label12.Click += new System.EventHandler(this.label12_Click);
			// 
			// testsBox
			// 
			this.testsBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.testsBox.Location = new System.Drawing.Point(358, 389);
			this.testsBox.Maximum = new decimal(new int[] {
            28,
            0,
            0,
            0});
			this.testsBox.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.testsBox.Name = "testsBox";
			this.testsBox.Size = new System.Drawing.Size(64, 23);
			this.testsBox.TabIndex = 35;
			this.testsBox.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.testsBox.ValueChanged += new System.EventHandler(this.testsBox_ValueChanged);
			// 
			// startButton
			// 
			this.startButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.startButton.Location = new System.Drawing.Point(358, 418);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(96, 52);
			this.startButton.TabIndex = 37;
			this.startButton.Text = "Начать";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.OnStart);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(108, 345);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(25, 15);
			this.label3.TabIndex = 42;
			this.label3.Text = "255";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 345);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(30, 15);
			this.label4.TabIndex = 41;
			this.label4.Text = "-255";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(19, 305);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(51, 15);
			this.label8.TabIndex = 40;
			this.label8.Text = "Яркость";
			// 
			// brightnessTrackBar
			// 
			this.brightnessTrackBar.Location = new System.Drawing.Point(19, 323);
			this.brightnessTrackBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.brightnessTrackBar.Maximum = 255;
			this.brightnessTrackBar.Minimum = -255;
			this.brightnessTrackBar.Name = "brightnessTrackBar";
			this.brightnessTrackBar.Size = new System.Drawing.Size(114, 45);
			this.brightnessTrackBar.TabIndex = 39;
			this.brightnessTrackBar.Scroll += new System.EventHandler(this.brightnessTrackBar_Scroll);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(173, 340);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 15);
			this.label9.TabIndex = 47;
			this.label9.Text = "1";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(237, 338);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(13, 15);
			this.label10.TabIndex = 46;
			this.label10.Text = "5";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(150, 340);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(13, 15);
			this.label11.TabIndex = 45;
			this.label11.Text = "0";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(150, 305);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(57, 15);
			this.label13.TabIndex = 44;
			this.label13.Text = "Контраст";
			// 
			// contrastTrackBar
			// 
			this.contrastTrackBar.Location = new System.Drawing.Point(150, 322);
			this.contrastTrackBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.contrastTrackBar.Maximum = 50;
			this.contrastTrackBar.Name = "contrastTrackBar";
			this.contrastTrackBar.Size = new System.Drawing.Size(100, 45);
			this.contrastTrackBar.TabIndex = 43;
			this.contrastTrackBar.Value = 10;
			this.contrastTrackBar.Scroll += new System.EventHandler(this.contrastTrackBar_Scroll);
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(264, 323);
			this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.trackBar1.Maximum = 20;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(100, 45);
			this.trackBar1.TabIndex = 48;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(266, 305);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(43, 15);
			this.label14.TabIndex = 49;
			this.label14.Text = "Гамма";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(273, 350);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(13, 15);
			this.label15.TabIndex = 50;
			this.label15.Text = "0";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(349, 350);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(13, 15);
			this.label16.TabIndex = 51;
			this.label16.Text = "2";
			// 
			// Benchmark
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(471, 483);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.contrastTrackBar);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.brightnessTrackBar);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.testsBox);
			this.Controls.Add(this.localProgress);
			this.Controls.Add(this.overallProgress);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Benchmark";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Benchmark";
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
		private TextBox textBox1;
		private Button button1;
		private Button button2;
		private TextBox textBox2;
		private Label label2;
		private Button button3;
		private TextBox textBox3;
		private Label label5;
		private Button button4;
		private TextBox textBox4;
		private Label label6;
		private SaveFileDialog saveFileDialog1;
		private Button button5;
		private TextBox textBox5;
		private Label label7;
		private ProgressBar overallProgress;
		private ProgressBar localProgress;
		private Label label12;
		private NumericUpDown testsBox;
		private Button startButton;
		private Button setCurveButton;
		private Label label3;
		private Label label4;
		private Label label8;
		private TrackBar brightnessTrackBar;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label13;
		private TrackBar contrastTrackBar;
        private TrackBar trackBar1;
        private Label label14;
        private Label label15;
        private Label label16;
    }
}