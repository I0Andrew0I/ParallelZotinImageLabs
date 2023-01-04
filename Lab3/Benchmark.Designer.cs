using System.Windows.Forms;

namespace Lab3
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
			this.sizePanel = new System.Windows.Forms.Panel();
			this.widthBox = new System.Windows.Forms.NumericUpDown();
			this.heightBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.laplaceBox = new System.Windows.Forms.CheckBox();
			this.sobelBox = new System.Windows.Forms.CheckBox();
			this.roberstBox = new System.Windows.Forms.CheckBox();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.overallProgress = new System.Windows.Forms.ProgressBar();
			this.localProgress = new System.Windows.Forms.ProgressBar();
			this.label12 = new System.Windows.Forms.Label();
			this.testsBox = new System.Windows.Forms.NumericUpDown();
			this.startButton = new System.Windows.Forms.Button();
			this.multiplierTrack = new System.Windows.Forms.TrackBar();
			this.thresholdTrack = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.sizePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplierTrack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.thresholdTrack)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "720p";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(19, 41);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(178, 23);
			this.textBox1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(203, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "Select";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnPic1Select);
			// 
			// sizePanel
			// 
			this.sizePanel.Controls.Add(this.widthBox);
			this.sizePanel.Controls.Add(this.heightBox);
			this.sizePanel.Controls.Add(this.label3);
			this.sizePanel.Controls.Add(this.label4);
			this.sizePanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.sizePanel.Location = new System.Drawing.Point(248, 314);
			this.sizePanel.Name = "sizePanel";
			this.sizePanel.Size = new System.Drawing.Size(127, 63);
			this.sizePanel.TabIndex = 18;
			// 
			// widthBox
			// 
			this.widthBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.widthBox.Location = new System.Drawing.Point(71, 7);
			this.widthBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.widthBox.Name = "widthBox";
			this.widthBox.Size = new System.Drawing.Size(53, 23);
			this.widthBox.TabIndex = 8;
			this.widthBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// heightBox
			// 
			this.heightBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.heightBox.Location = new System.Drawing.Point(71, 32);
			this.heightBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.heightBox.Name = "heightBox";
			this.heightBox.Size = new System.Drawing.Size(53, 23);
			this.heightBox.TabIndex = 9;
			this.heightBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 15);
			this.label3.TabIndex = 10;
			this.label3.Text = "Width";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 34);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 15);
			this.label4.TabIndex = 11;
			this.label4.Text = "Height";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(203, 89);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 22;
			this.button2.Text = "Select";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnPic2Select);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(19, 90);
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(178, 23);
			this.textBox2.TabIndex = 21;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 15);
			this.label2.TabIndex = 20;
			this.label2.Text = "1080p";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(203, 136);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 25;
			this.button3.Text = "Select";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.OnPic3Select);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(19, 137);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(178, 23);
			this.textBox3.TabIndex = 24;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 119);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 15);
			this.label5.TabIndex = 23;
			this.label5.Text = "1440p";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(203, 186);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 28;
			this.button4.Text = "Select";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.OnPic4Select);
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(19, 187);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new System.Drawing.Size(178, 23);
			this.textBox4.TabIndex = 27;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 169);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(38, 15);
			this.label6.TabIndex = 26;
			this.label6.Text = "2160p";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "csv";
			this.saveFileDialog1.Filter = "CSV|*csv";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.laplaceBox);
			this.groupBox1.Controls.Add(this.sobelBox);
			this.groupBox1.Controls.Add(this.roberstBox);
			this.groupBox1.Location = new System.Drawing.Point(305, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(190, 94);
			this.groupBox1.TabIndex = 29;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Testing methods";
			// 
			// laplaceBox
			// 
			this.laplaceBox.AutoSize = true;
			this.laplaceBox.Checked = true;
			this.laplaceBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.laplaceBox.Location = new System.Drawing.Point(6, 72);
			this.laplaceBox.Name = "laplaceBox";
			this.laplaceBox.Size = new System.Drawing.Size(66, 19);
			this.laplaceBox.TabIndex = 4;
			this.laplaceBox.Text = "Laplace";
			this.laplaceBox.UseVisualStyleBackColor = true;
			// 
			// sobelBox
			// 
			this.sobelBox.AutoSize = true;
			this.sobelBox.Checked = true;
			this.sobelBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.sobelBox.Location = new System.Drawing.Point(6, 47);
			this.sobelBox.Name = "sobelBox";
			this.sobelBox.Size = new System.Drawing.Size(55, 19);
			this.sobelBox.TabIndex = 3;
			this.sobelBox.Text = "Sobel";
			this.sobelBox.UseVisualStyleBackColor = true;
			// 
			// roberstBox
			// 
			this.roberstBox.AutoSize = true;
			this.roberstBox.Checked = true;
			this.roberstBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.roberstBox.Location = new System.Drawing.Point(6, 22);
			this.roberstBox.Name = "roberstBox";
			this.roberstBox.Size = new System.Drawing.Size(66, 19);
			this.roberstBox.TabIndex = 0;
			this.roberstBox.Text = "Roberts";
			this.roberstBox.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(203, 264);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 32;
			this.button5.Text = "Select";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.OnOutputPath);
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(19, 265);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new System.Drawing.Size(178, 23);
			this.textBox5.TabIndex = 31;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 247);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(45, 15);
			this.label7.TabIndex = 30;
			this.label7.Text = "Output";
			// 
			// overallProgress
			// 
			this.overallProgress.Location = new System.Drawing.Point(7, 415);
			this.overallProgress.Name = "overallProgress";
			this.overallProgress.Size = new System.Drawing.Size(488, 23);
			this.overallProgress.TabIndex = 33;
			// 
			// localProgress
			// 
			this.localProgress.Location = new System.Drawing.Point(7, 386);
			this.localProgress.Name = "localProgress";
			this.localProgress.Size = new System.Drawing.Size(488, 23);
			this.localProgress.TabIndex = 34;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(399, 309);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(42, 15);
			this.label12.TabIndex = 36;
			this.label12.Text = "Retries";
			// 
			// testsBox
			// 
			this.testsBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.testsBox.Location = new System.Drawing.Point(445, 307);
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
			this.testsBox.Size = new System.Drawing.Size(50, 23);
			this.testsBox.TabIndex = 35;
			this.testsBox.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			// 
			// startButton
			// 
			this.startButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.startButton.Location = new System.Drawing.Point(399, 341);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(96, 36);
			this.startButton.TabIndex = 37;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.OnStart);
			// 
			// multiplierTrack
			// 
			this.multiplierTrack.Location = new System.Drawing.Point(135, 329);
			this.multiplierTrack.Maximum = 20;
			this.multiplierTrack.Name = "multiplierTrack";
			this.multiplierTrack.Size = new System.Drawing.Size(104, 45);
			this.multiplierTrack.TabIndex = 38;
			this.multiplierTrack.Scroll += new System.EventHandler(this.multiplierTrack_Scroll);
			// 
			// thresholdTrack
			// 
			this.thresholdTrack.Location = new System.Drawing.Point(15, 333);
			this.thresholdTrack.Maximum = 255;
			this.thresholdTrack.Minimum = 1;
			this.thresholdTrack.Name = "thresholdTrack";
			this.thresholdTrack.Size = new System.Drawing.Size(104, 45);
			this.thresholdTrack.TabIndex = 39;
			this.thresholdTrack.Value = 1;
			this.thresholdTrack.Scroll += new System.EventHandler(this.thresholdTrack_Scroll);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(139, 314);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(58, 15);
			this.label8.TabIndex = 40;
			this.label8.Text = "Multiplier";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(145, 354);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 15);
			this.label9.TabIndex = 41;
			this.label9.Text = "1";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(226, 354);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(13, 15);
			this.label10.TabIndex = 42;
			this.label10.Text = "5";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(93, 351);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(25, 15);
			this.label11.TabIndex = 45;
			this.label11.Text = "255";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(21, 351);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(13, 15);
			this.label13.TabIndex = 44;
			this.label13.Text = "1";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(21, 315);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(59, 15);
			this.label14.TabIndex = 43;
			this.label14.Text = "Threshold";
			// 
			// Benchmark
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(503, 450);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.thresholdTrack);
			this.Controls.Add(this.multiplierTrack);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.testsBox);
			this.Controls.Add(this.localProgress);
			this.Controls.Add(this.overallProgress);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.sizePanel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Name = "Benchmark";
			this.Text = "Benchmark";
			this.sizePanel.ResumeLayout(false);
			this.sizePanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplierTrack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.thresholdTrack)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
		private TextBox textBox1;
		private Button button1;
		private Panel sizePanel;
		private NumericUpDown widthBox;
		private NumericUpDown heightBox;
		private Label label3;
		private Label label4;
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
		private GroupBox groupBox1;
		private CheckBox laplaceBox;
		private CheckBox sobelBox;
		private CheckBox roberstBox;
		private Button button5;
		private TextBox textBox5;
		private Label label7;
		private ProgressBar overallProgress;
		private ProgressBar localProgress;
		private Label label12;
		private NumericUpDown testsBox;
		private Button startButton;
		private TrackBar multiplierTrack;
		private TrackBar thresholdTrack;
		private Label label8;
		private Label label9;
		private Label label10;
		private Label label11;
		private Label label13;
		private Label label14;
	}
}