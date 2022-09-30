
using System.Windows.Forms;

namespace Lab1
{
    partial class Lab1Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.inputPictureBox = new System.Windows.Forms.PictureBox();
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			this.inputLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonYUV = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.contrastTrackBar = new System.Windows.Forms.TrackBar();
			this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonHR = new System.Windows.Forms.Button();
			this.buttonRH = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonSearch = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this._channelBox = new System.Windows.Forms.ComboBox();
			this.buttonViz = new System.Windows.Forms.Button();
			this.yuvRadio = new System.Windows.Forms.RadioButton();
			this.rgbRadio = new System.Windows.Forms.RadioButton();
			this.hlsRadio = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.buttonMod = new System.Windows.Forms.Button();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.radioButton6 = new System.Windows.Forms.RadioButton();
			this.histogramPanel = new System.Windows.Forms.Panel();
			this._threadCountBox = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this._testCountBox = new System.Windows.Forms.NumericUpDown();
			this._saveResultsBox = new System.Windows.Forms.CheckBox();
			this.testButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this._threadCountBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._testCountBox)).BeginInit();
			this.SuspendLayout();
			// 
			// inputPictureBox
			// 
			this.inputPictureBox.BackColor = System.Drawing.Color.White;
			this.inputPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.inputPictureBox.Location = new System.Drawing.Point(2, 27);
			this.inputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.inputPictureBox.Name = "inputPictureBox";
			this.inputPictureBox.Size = new System.Drawing.Size(597, 353);
			this.inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.inputPictureBox.TabIndex = 0;
			this.inputPictureBox.TabStop = false;
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.BackColor = System.Drawing.Color.White;
			this.outputPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.outputPictureBox.Location = new System.Drawing.Point(604, 27);
			this.outputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(597, 353);
			this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.outputPictureBox.TabIndex = 1;
			this.outputPictureBox.TabStop = false;
			// 
			// inputLabel
			// 
			this.inputLabel.AutoSize = true;
			this.inputLabel.Location = new System.Drawing.Point(4, 9);
			this.inputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.inputLabel.Name = "inputLabel";
			this.inputLabel.Size = new System.Drawing.Size(141, 15);
			this.inputLabel.TabIndex = 2;
			this.inputLabel.Text = "Исходное изображение:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(604, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Результат:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.buttonYUV);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.contrastTrackBar);
			this.groupBox1.Controls.Add(this.brightnessTrackBar);
			this.groupBox1.Location = new System.Drawing.Point(330, 506);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(535, 103);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Изменение яркости/контраста";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(263, 75);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 15);
			this.label9.TabIndex = 14;
			this.label9.Text = "1";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(410, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 15);
			this.label8.TabIndex = 13;
			this.label8.Text = "5";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(225, 68);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(13, 15);
			this.label7.TabIndex = 12;
			this.label7.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(185, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(25, 15);
			this.label6.TabIndex = 11;
			this.label6.Text = "255";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(7, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 15);
			this.label5.TabIndex = 10;
			this.label5.Text = "-255";
			// 
			// buttonYUV
			// 
			this.buttonYUV.Location = new System.Drawing.Point(435, 50);
			this.buttonYUV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonYUV.Name = "buttonYUV";
			this.buttonYUV.Size = new System.Drawing.Size(88, 27);
			this.buttonYUV.TabIndex = 9;
			this.buttonYUV.Text = "Изменить";
			this.buttonYUV.UseVisualStyleBackColor = true;
			this.buttonYUV.Click += new System.EventHandler(this.onTransformImage);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(230, 30);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 15);
			this.label3.TabIndex = 8;
			this.label3.Text = "Контраст";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 33);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(51, 15);
			this.label2.TabIndex = 7;
			this.label2.Text = "Яркость";
			// 
			// contrastTrackBar
			// 
			this.contrastTrackBar.Location = new System.Drawing.Point(221, 52);
			this.contrastTrackBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.contrastTrackBar.Maximum = 50;
			this.contrastTrackBar.Name = "contrastTrackBar";
			this.contrastTrackBar.Size = new System.Drawing.Size(202, 45);
			this.contrastTrackBar.TabIndex = 6;
			this.contrastTrackBar.Value = 10;
			this.contrastTrackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
			// 
			// brightnessTrackBar
			// 
			this.brightnessTrackBar.Location = new System.Drawing.Point(8, 50);
			this.brightnessTrackBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.brightnessTrackBar.Maximum = 255;
			this.brightnessTrackBar.Minimum = -255;
			this.brightnessTrackBar.Name = "brightnessTrackBar";
			this.brightnessTrackBar.Size = new System.Drawing.Size(202, 45);
			this.brightnessTrackBar.TabIndex = 0;
			this.brightnessTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.buttonHR);
			this.groupBox2.Controls.Add(this.buttonRH);
			this.groupBox2.Location = new System.Drawing.Point(330, 395);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(412, 66);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Смена цветовой модели";
			// 
			// buttonHR
			// 
			this.buttonHR.Location = new System.Drawing.Point(221, 31);
			this.buttonHR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonHR.Name = "buttonHR";
			this.buttonHR.Size = new System.Drawing.Size(177, 27);
			this.buttonHR.TabIndex = 6;
			this.buttonHR.Text = "HLS -> RGB";
			this.buttonHR.UseVisualStyleBackColor = true;
			// 
			// buttonRH
			// 
			this.buttonRH.Location = new System.Drawing.Point(26, 31);
			this.buttonRH.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonRH.Name = "buttonRH";
			this.buttonRH.Size = new System.Drawing.Size(177, 27);
			this.buttonRH.TabIndex = 0;
			this.buttonRH.Text = "RGB -> HLS";
			this.buttonRH.UseVisualStyleBackColor = true;
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "ofd";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 558);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Путь к файлу";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(4, 576);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(224, 23);
			this.textBox1.TabIndex = 9;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(236, 573);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(88, 27);
			this.buttonSearch.TabIndex = 10;
			this.buttonSearch.Text = "Обзор...";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._channelBox);
			this.groupBox3.Controls.Add(this.buttonViz);
			this.groupBox3.Controls.Add(this.yuvRadio);
			this.groupBox3.Controls.Add(this.rgbRadio);
			this.groupBox3.Controls.Add(this.hlsRadio);
			this.groupBox3.Location = new System.Drawing.Point(13, 395);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(153, 160);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Канал";
			// 
			// _channelBox
			// 
			this._channelBox.FormattingEnabled = true;
			this._channelBox.Location = new System.Drawing.Point(24, 98);
			this._channelBox.Name = "_channelBox";
			this._channelBox.Size = new System.Drawing.Size(108, 23);
			this._channelBox.TabIndex = 24;
			// 
			// buttonViz
			// 
			this.buttonViz.Location = new System.Drawing.Point(25, 127);
			this.buttonViz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonViz.Name = "buttonViz";
			this.buttonViz.Size = new System.Drawing.Size(98, 27);
			this.buttonViz.TabIndex = 3;
			this.buttonViz.Text = "Show";
			this.buttonViz.UseVisualStyleBackColor = true;
			this.buttonViz.Click += new System.EventHandler(this.onFilterChannels);
			// 
			// yuvRadio
			// 
			this.yuvRadio.AutoSize = true;
			this.yuvRadio.Location = new System.Drawing.Point(24, 76);
			this.yuvRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.yuvRadio.Name = "yuvRadio";
			this.yuvRadio.Size = new System.Drawing.Size(47, 19);
			this.yuvRadio.TabIndex = 23;
			this.yuvRadio.TabStop = true;
			this.yuvRadio.Text = "YUV";
			this.yuvRadio.UseVisualStyleBackColor = true;
			// 
			// rgbRadio
			// 
			this.rgbRadio.AutoSize = true;
			this.rgbRadio.Checked = true;
			this.rgbRadio.Location = new System.Drawing.Point(24, 26);
			this.rgbRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.rgbRadio.Name = "rgbRadio";
			this.rgbRadio.Size = new System.Drawing.Size(47, 19);
			this.rgbRadio.TabIndex = 21;
			this.rgbRadio.TabStop = true;
			this.rgbRadio.Text = "RGB";
			this.rgbRadio.UseVisualStyleBackColor = true;
			// 
			// hlsRadio
			// 
			this.hlsRadio.AutoSize = true;
			this.hlsRadio.Location = new System.Drawing.Point(25, 51);
			this.hlsRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.hlsRadio.Name = "hlsRadio";
			this.hlsRadio.Size = new System.Drawing.Size(46, 19);
			this.hlsRadio.TabIndex = 22;
			this.hlsRadio.TabStop = true;
			this.hlsRadio.Text = "HLS";
			this.hlsRadio.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.buttonMod);
			this.groupBox4.Controls.Add(this.radioButton5);
			this.groupBox4.Controls.Add(this.radioButton6);
			this.groupBox4.Location = new System.Drawing.Point(174, 395);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Size = new System.Drawing.Size(148, 149);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Коррекция";
			// 
			// buttonMod
			// 
			this.buttonMod.Location = new System.Drawing.Point(8, 111);
			this.buttonMod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonMod.Name = "buttonMod";
			this.buttonMod.Size = new System.Drawing.Size(131, 27);
			this.buttonMod.TabIndex = 3;
			this.buttonMod.Text = "Модифицировать";
			this.buttonMod.UseVisualStyleBackColor = true;
			this.buttonMod.Click += new System.EventHandler(this.onModification);
			// 
			// radioButton5
			// 
			this.radioButton5.AutoSize = true;
			this.radioButton5.Location = new System.Drawing.Point(8, 47);
			this.radioButton5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(132, 19);
			this.radioButton5.TabIndex = 1;
			this.radioButton5.TabStop = true;
			this.radioButton5.Text = "система уравнений";
			this.radioButton5.UseVisualStyleBackColor = true;
			// 
			// radioButton6
			// 
			this.radioButton6.AutoSize = true;
			this.radioButton6.Location = new System.Drawing.Point(8, 22);
			this.radioButton6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(129, 19);
			this.radioButton6.TabIndex = 0;
			this.radioButton6.TabStop = true;
			this.radioButton6.Text = "кусочно-линейная";
			this.radioButton6.UseVisualStyleBackColor = true;
			// 
			// histogramPanel
			// 
			this.histogramPanel.Location = new System.Drawing.Point(885, 395);
			this.histogramPanel.Name = "histogramPanel";
			this.histogramPanel.Size = new System.Drawing.Size(307, 192);
			this.histogramPanel.TabIndex = 14;
			// 
			// _threadCountBox
			// 
			this._threadCountBox.Location = new System.Drawing.Point(816, 417);
			this._threadCountBox.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this._threadCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._threadCountBox.Name = "_threadCountBox";
			this._threadCountBox.Size = new System.Drawing.Size(49, 23);
			this._threadCountBox.TabIndex = 15;
			this._threadCountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(749, 395);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 15);
			this.label10.TabIndex = 16;
			this.label10.Text = "Количество потоков";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(746, 442);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(123, 15);
			this.label11.TabIndex = 17;
			this.label11.Text = "Количество попыток";
			// 
			// _testCountBox
			// 
			this._testCountBox.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this._testCountBox.Location = new System.Drawing.Point(816, 460);
			this._testCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._testCountBox.Name = "_testCountBox";
			this._testCountBox.Size = new System.Drawing.Size(49, 23);
			this._testCountBox.TabIndex = 18;
			this._testCountBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// _saveResultsBox
			// 
			this._saveResultsBox.AutoSize = true;
			this._saveResultsBox.Location = new System.Drawing.Point(729, 489);
			this._saveResultsBox.Name = "_saveResultsBox";
			this._saveResultsBox.Size = new System.Drawing.Size(140, 19);
			this._saveResultsBox.TabIndex = 19;
			this._saveResultsBox.Text = "Сохранять результат";
			this._saveResultsBox.UseVisualStyleBackColor = true;
			// 
			// testButton
			// 
			this.testButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.testButton.Location = new System.Drawing.Point(597, 480);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(99, 28);
			this.testButton.TabIndex = 20;
			this.testButton.Text = "Тестировать";
			this.testButton.UseVisualStyleBackColor = false;
			this.testButton.Click += new System.EventHandler(this.OnTest);
			// 
			// Lab1Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 611);
			this.Controls.Add(this.testButton);
			this.Controls.Add(this._saveResultsBox);
			this.Controls.Add(this._testCountBox);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this._threadCountBox);
			this.Controls.Add(this.histogramPanel);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.inputLabel);
			this.Controls.Add(this.outputPictureBox);
			this.Controls.Add(this.inputPictureBox);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "Lab1Form";
			this.Text = "Lab1";
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this._threadCountBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._testCountBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox inputPictureBox;
        private System.Windows.Forms.PictureBox outputPictureBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonHR;
        private System.Windows.Forms.Button buttonRH;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonViz;
        private System.Windows.Forms.Button buttonYUV;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonMod;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
		private Label label5;
		private Label label9;
		private Label label8;
		private Label label7;
		private Label label6;
		private Panel histogramPanel;
		private NumericUpDown _threadCountBox;
		private Label label10;
		private Label label11;
		private NumericUpDown _testCountBox;
		private CheckBox _saveResultsBox;
		private Button testButton;
		private ComboBox _channelBox;
		private RadioButton yuvRadio;
		private RadioButton hlsRadio;
		private RadioButton rgbRadio;
	}
}

