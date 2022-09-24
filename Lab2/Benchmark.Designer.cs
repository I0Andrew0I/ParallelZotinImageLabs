using System.Windows.Forms;

namespace Lab2
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
			this.shapePanel = new System.Windows.Forms.Panel();
			this.rectangleShapeRadio = new System.Windows.Forms.RadioButton();
			this.roundShapeRadio = new System.Windows.Forms.RadioButton();
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
			this.laplacianBox = new System.Windows.Forms.CheckBox();
			this.medianBox = new System.Windows.Forms.CheckBox();
			this.linearBox = new System.Windows.Forms.CheckBox();
			this.meanBox = new System.Windows.Forms.CheckBox();
			this.minMaxBox = new System.Windows.Forms.CheckBox();
			this.button5 = new System.Windows.Forms.Button();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.overallProgress = new System.Windows.Forms.ProgressBar();
			this.localProgress = new System.Windows.Forms.ProgressBar();
			this.label12 = new System.Windows.Forms.Label();
			this.testsBox = new System.Windows.Forms.NumericUpDown();
			this.startButton = new System.Windows.Forms.Button();
			this.sizePanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			this.shapePanel.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).BeginInit();
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
			this.sizePanel.Location = new System.Drawing.Point(171, 297);
			this.sizePanel.Name = "sizePanel";
			this.sizePanel.Size = new System.Drawing.Size(127, 75);
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
			// shapePanel
			// 
			this.shapePanel.Controls.Add(this.rectangleShapeRadio);
			this.shapePanel.Controls.Add(this.roundShapeRadio);
			this.shapePanel.Location = new System.Drawing.Point(19, 297);
			this.shapePanel.Name = "shapePanel";
			this.shapePanel.Size = new System.Drawing.Size(146, 75);
			this.shapePanel.TabIndex = 19;
			// 
			// rectangleShapeRadio
			// 
			this.rectangleShapeRadio.AutoSize = true;
			this.rectangleShapeRadio.Checked = true;
			this.rectangleShapeRadio.Location = new System.Drawing.Point(11, 11);
			this.rectangleShapeRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.rectangleShapeRadio.Name = "rectangleShapeRadio";
			this.rectangleShapeRadio.Size = new System.Drawing.Size(77, 19);
			this.rectangleShapeRadio.TabIndex = 6;
			this.rectangleShapeRadio.TabStop = true;
			this.rectangleShapeRadio.Text = "Rectangle";
			this.rectangleShapeRadio.UseVisualStyleBackColor = true;
			// 
			// roundShapeRadio
			// 
			this.roundShapeRadio.AutoSize = true;
			this.roundShapeRadio.Location = new System.Drawing.Point(11, 36);
			this.roundShapeRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.roundShapeRadio.Name = "roundShapeRadio";
			this.roundShapeRadio.Size = new System.Drawing.Size(60, 19);
			this.roundShapeRadio.TabIndex = 7;
			this.roundShapeRadio.Text = "Round";
			this.roundShapeRadio.UseVisualStyleBackColor = true;
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
			this.groupBox1.Controls.Add(this.laplacianBox);
			this.groupBox1.Controls.Add(this.medianBox);
			this.groupBox1.Controls.Add(this.linearBox);
			this.groupBox1.Controls.Add(this.meanBox);
			this.groupBox1.Controls.Add(this.minMaxBox);
			this.groupBox1.Location = new System.Drawing.Point(305, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(127, 139);
			this.groupBox1.TabIndex = 29;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Testing methods";
			// 
			// laplacianBox
			// 
			this.laplacianBox.AutoSize = true;
			this.laplacianBox.Checked = true;
			this.laplacianBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.laplacianBox.Location = new System.Drawing.Point(3, 94);
			this.laplacianBox.Name = "laplacianBox";
			this.laplacianBox.Size = new System.Drawing.Size(76, 19);
			this.laplacianBox.TabIndex = 4;
			this.laplacianBox.Text = "Laplacian";
			this.laplacianBox.UseVisualStyleBackColor = true;
			// 
			// medianBox
			// 
			this.medianBox.AutoSize = true;
			this.medianBox.Checked = true;
			this.medianBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.medianBox.Location = new System.Drawing.Point(3, 69);
			this.medianBox.Name = "medianBox";
			this.medianBox.Size = new System.Drawing.Size(90, 19);
			this.medianBox.TabIndex = 3;
			this.medianBox.Text = "Fast median";
			this.medianBox.UseVisualStyleBackColor = true;
			// 
			// linearBox
			// 
			this.linearBox.AutoSize = true;
			this.linearBox.Checked = true;
			this.linearBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.linearBox.Location = new System.Drawing.Point(3, 119);
			this.linearBox.Name = "linearBox";
			this.linearBox.Size = new System.Drawing.Size(58, 19);
			this.linearBox.TabIndex = 2;
			this.linearBox.Text = "Linear";
			this.linearBox.UseVisualStyleBackColor = true;
			// 
			// meanBox
			// 
			this.meanBox.AutoSize = true;
			this.meanBox.Checked = true;
			this.meanBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.meanBox.Location = new System.Drawing.Point(3, 44);
			this.meanBox.Name = "meanBox";
			this.meanBox.Size = new System.Drawing.Size(106, 19);
			this.meanBox.TabIndex = 1;
			this.meanBox.Text = "Mean recursive";
			this.meanBox.UseVisualStyleBackColor = true;
			// 
			// minMaxBox
			// 
			this.minMaxBox.AutoSize = true;
			this.minMaxBox.Checked = true;
			this.minMaxBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.minMaxBox.Location = new System.Drawing.Point(3, 19);
			this.minMaxBox.Name = "minMaxBox";
			this.minMaxBox.Size = new System.Drawing.Size(70, 19);
			this.minMaxBox.TabIndex = 0;
			this.minMaxBox.Text = "MinMax";
			this.minMaxBox.UseVisualStyleBackColor = true;
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
			this.overallProgress.Size = new System.Drawing.Size(432, 23);
			this.overallProgress.TabIndex = 33;
			// 
			// localProgress
			// 
			this.localProgress.Location = new System.Drawing.Point(7, 386);
			this.localProgress.Name = "localProgress";
			this.localProgress.Size = new System.Drawing.Size(432, 23);
			this.localProgress.TabIndex = 34;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(311, 299);
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
			this.testsBox.Location = new System.Drawing.Point(359, 297);
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
			// 
			// button6
			// 
			this.startButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.startButton.Location = new System.Drawing.Point(336, 331);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(96, 36);
			this.startButton.TabIndex = 37;
			this.startButton.Text = "Start";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.OnStart);
			// 
			// Benchmark
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(451, 450);
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
			this.Controls.Add(this.shapePanel);
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
			this.shapePanel.ResumeLayout(false);
			this.shapePanel.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).EndInit();
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
		private Panel shapePanel;
		private RadioButton rectangleShapeRadio;
		private RadioButton roundShapeRadio;
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
		private CheckBox laplacianBox;
		private CheckBox medianBox;
		private CheckBox linearBox;
		private CheckBox meanBox;
		private CheckBox minMaxBox;
		private Button button5;
		private TextBox textBox5;
		private Label label7;
		private ProgressBar overallProgress;
		private ProgressBar localProgress;
		private Label label12;
		private NumericUpDown testsBox;
		private Button startButton;
	}
}