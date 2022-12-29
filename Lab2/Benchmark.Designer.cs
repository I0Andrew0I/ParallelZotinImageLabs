﻿using System.Windows.Forms;

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
            this.medianCheckBox = new System.Windows.Forms.CheckBox();
            this.casaburiBox = new System.Windows.Forms.CheckBox();
            this.fastMedianBox = new System.Windows.Forms.CheckBox();
            this.linearBox = new System.Windows.Forms.CheckBox();
            this.meanBox = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.overallProgress = new System.Windows.Forms.ProgressBar();
            this.localProgress = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.testsBox = new System.Windows.Forms.NumericUpDown();
            this.startButton = new System.Windows.Forms.Button();
            this.sliderLabel = new System.Windows.Forms.Label();
            this.sliderBar = new System.Windows.Forms.TrackBar();
            this.sizePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            this.shapePanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Картинка 1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 55);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(203, 27);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(232, 53);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Выбрать";
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
            this.sizePanel.Location = new System.Drawing.Point(195, 396);
            this.sizePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sizePanel.Name = "sizePanel";
            this.sizePanel.Size = new System.Drawing.Size(145, 100);
            this.sizePanel.TabIndex = 18;
            // 
            // widthBox
            // 
            this.widthBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.widthBox.Location = new System.Drawing.Point(81, 9);
            this.widthBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.widthBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(61, 27);
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
            this.heightBox.Location = new System.Drawing.Point(81, 43);
            this.heightBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.heightBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.heightBox.Name = "heightBox";
            this.heightBox.Size = new System.Drawing.Size(61, 27);
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
            this.label3.Location = new System.Drawing.Point(11, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Height";
            // 
            // shapePanel
            // 
            this.shapePanel.Controls.Add(this.rectangleShapeRadio);
            this.shapePanel.Controls.Add(this.roundShapeRadio);
            this.shapePanel.Location = new System.Drawing.Point(22, 396);
            this.shapePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.shapePanel.Name = "shapePanel";
            this.shapePanel.Size = new System.Drawing.Size(167, 100);
            this.shapePanel.TabIndex = 19;
            // 
            // rectangleShapeRadio
            // 
            this.rectangleShapeRadio.AutoSize = true;
            this.rectangleShapeRadio.Checked = true;
            this.rectangleShapeRadio.Location = new System.Drawing.Point(13, 15);
            this.rectangleShapeRadio.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.rectangleShapeRadio.Name = "rectangleShapeRadio";
            this.rectangleShapeRadio.Size = new System.Drawing.Size(96, 24);
            this.rectangleShapeRadio.TabIndex = 6;
            this.rectangleShapeRadio.TabStop = true;
            this.rectangleShapeRadio.Text = "Rectangle";
            this.rectangleShapeRadio.UseVisualStyleBackColor = true;
            // 
            // roundShapeRadio
            // 
            this.roundShapeRadio.AutoSize = true;
            this.roundShapeRadio.Location = new System.Drawing.Point(13, 48);
            this.roundShapeRadio.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.roundShapeRadio.Name = "roundShapeRadio";
            this.roundShapeRadio.Size = new System.Drawing.Size(73, 24);
            this.roundShapeRadio.TabIndex = 7;
            this.roundShapeRadio.Text = "Round";
            this.roundShapeRadio.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(232, 119);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 31);
            this.button2.TabIndex = 22;
            this.button2.Text = "Выбрать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnPic2Select);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(22, 120);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(203, 27);
            this.textBox2.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "Картинка 2";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(232, 181);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 31);
            this.button3.TabIndex = 25;
            this.button3.Text = "Выбрать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnPic3Select);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(22, 183);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(203, 27);
            this.textBox3.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 20);
            this.label5.TabIndex = 23;
            this.label5.Text = "Картинка 3";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(232, 248);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 31);
            this.button4.TabIndex = 28;
            this.button4.Text = "Выбрать";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.OnPic4Select);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(22, 249);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(203, 27);
            this.textBox4.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Картинка 4";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "csv";
            this.saveFileDialog1.Filter = "CSV|*csv";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.medianCheckBox);
            this.groupBox1.Controls.Add(this.casaburiBox);
            this.groupBox1.Controls.Add(this.fastMedianBox);
            this.groupBox1.Controls.Add(this.linearBox);
            this.groupBox1.Controls.Add(this.meanBox);
            this.groupBox1.Location = new System.Drawing.Point(349, 53);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(295, 192);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Методы";
            // 
            // medianCheckBox
            // 
            this.medianCheckBox.AutoSize = true;
            this.medianCheckBox.Checked = true;
            this.medianCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.medianCheckBox.Location = new System.Drawing.Point(6, 160);
            this.medianCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.medianCheckBox.Name = "medianCheckBox";
            this.medianCheckBox.Size = new System.Drawing.Size(115, 24);
            this.medianCheckBox.TabIndex = 5;
            this.medianCheckBox.Text = "Медианный";
            this.medianCheckBox.UseVisualStyleBackColor = true;
            // 
            // casaburiBox
            // 
            this.casaburiBox.AutoSize = true;
            this.casaburiBox.Checked = true;
            this.casaburiBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.casaburiBox.Location = new System.Drawing.Point(6, 94);
            this.casaburiBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.casaburiBox.Name = "casaburiBox";
            this.casaburiBox.Size = new System.Drawing.Size(114, 24);
            this.casaburiBox.TabIndex = 4;
            this.casaburiBox.Text = "Jim Casaburi";
            this.casaburiBox.UseVisualStyleBackColor = true;
            // 
            // fastMedianBox
            // 
            this.fastMedianBox.AutoSize = true;
            this.fastMedianBox.Checked = true;
            this.fastMedianBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fastMedianBox.Location = new System.Drawing.Point(6, 61);
            this.fastMedianBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.fastMedianBox.Name = "fastMedianBox";
            this.fastMedianBox.Size = new System.Drawing.Size(179, 24);
            this.fastMedianBox.TabIndex = 3;
            this.fastMedianBox.Text = "Быстрый медианный";
            this.fastMedianBox.UseVisualStyleBackColor = true;
            // 
            // linearBox
            // 
            this.linearBox.AutoSize = true;
            this.linearBox.Checked = true;
            this.linearBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.linearBox.Location = new System.Drawing.Point(6, 128);
            this.linearBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.linearBox.Name = "linearBox";
            this.linearBox.Size = new System.Drawing.Size(105, 24);
            this.linearBox.TabIndex = 2;
            this.linearBox.Text = "Линейный";
            this.linearBox.UseVisualStyleBackColor = true;
            // 
            // meanBox
            // 
            this.meanBox.AutoSize = true;
            this.meanBox.Checked = true;
            this.meanBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.meanBox.Location = new System.Drawing.Point(6, 28);
            this.meanBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.meanBox.Name = "meanBox";
            this.meanBox.Size = new System.Drawing.Size(294, 24);
            this.meanBox.TabIndex = 1;
            this.meanBox.Text = "Рекурсивный среднеарифметический";
            this.meanBox.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(232, 352);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 31);
            this.button5.TabIndex = 32;
            this.button5.Text = "Открыть";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.OnOutputPath);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(22, 353);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(203, 27);
            this.textBox5.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 329);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "Сохранить в";
            // 
            // overallProgress
            // 
            this.overallProgress.Location = new System.Drawing.Point(8, 553);
            this.overallProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.overallProgress.Name = "overallProgress";
            this.overallProgress.Size = new System.Drawing.Size(494, 31);
            this.overallProgress.TabIndex = 33;
            // 
            // localProgress
            // 
            this.localProgress.Location = new System.Drawing.Point(8, 515);
            this.localProgress.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.localProgress.Name = "localProgress";
            this.localProgress.Size = new System.Drawing.Size(494, 31);
            this.localProgress.TabIndex = 34;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(340, 329);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 20);
            this.label12.TabIndex = 36;
            this.label12.Text = "Кол-во повторов";
            // 
            // testsBox
            // 
            this.testsBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.testsBox.Location = new System.Drawing.Point(340, 353);
            this.testsBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.testsBox.Size = new System.Drawing.Size(129, 27);
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
            this.startButton.Location = new System.Drawing.Point(534, 515);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(110, 69);
            this.startButton.TabIndex = 37;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.OnStart);
            // 
            // sliderLabel
            // 
            this.sliderLabel.AutoSize = true;
            this.sliderLabel.Location = new System.Drawing.Point(360, 410);
            this.sliderLabel.Name = "sliderLabel";
            this.sliderLabel.Size = new System.Drawing.Size(69, 20);
            this.sliderLabel.TabIndex = 39;
            this.sliderLabel.Text = "Резкость";
            // 
            // sliderBar
            // 
            this.sliderBar.Location = new System.Drawing.Point(355, 439);
            this.sliderBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sliderBar.Maximum = 50;
            this.sliderBar.Minimum = 1;
            this.sliderBar.Name = "sliderBar";
            this.sliderBar.Size = new System.Drawing.Size(137, 56);
            this.sliderBar.TabIndex = 38;
            this.sliderBar.Value = 1;
            // 
            // Benchmark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 600);
            this.Controls.Add(this.sliderLabel);
            this.Controls.Add(this.sliderBar);
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
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            ((System.ComponentModel.ISupportInitialize)(this.sliderBar)).EndInit();
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
		private CheckBox casaburiBox;
		private CheckBox fastMedianBox;
		private CheckBox linearBox;
		private CheckBox meanBox;
		private Button button5;
		private TextBox textBox5;
		private Label label7;
		private ProgressBar overallProgress;
		private ProgressBar localProgress;
		private Label label12;
		private NumericUpDown testsBox;
		private Button startButton;
        private CheckBox medianCheckBox;
        private Label sliderLabel;
        private TrackBar sliderBar;
    }
}