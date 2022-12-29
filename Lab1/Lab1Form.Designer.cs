
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
			this.testButton = new System.Windows.Forms.Button();
			this.inputPictureBox = new System.Windows.Forms.PictureBox();
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this._saveResultsBox = new System.Windows.Forms.CheckBox();
			this._threadCountBox = new System.Windows.Forms.NumericUpDown();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this._testCountBox = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._threadCountBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._testCountBox)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
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
			this.groupBox1.Location = new System.Drawing.Point(875, 149);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(224, 226);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Изменение яркости/контраста";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(50, 143);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 15);
			this.label9.TabIndex = 14;
			this.label9.Text = "1";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(197, 136);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(13, 15);
			this.label8.TabIndex = 13;
			this.label8.Text = "5";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 136);
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
			this.buttonYUV.Location = new System.Drawing.Point(12, 171);
			this.buttonYUV.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonYUV.Name = "buttonYUV";
			this.buttonYUV.Size = new System.Drawing.Size(198, 27);
			this.buttonYUV.TabIndex = 9;
			this.buttonYUV.Text = "Изменить";
			this.buttonYUV.UseVisualStyleBackColor = true;
			this.buttonYUV.Click += new System.EventHandler(this.onTransformImage);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 98);
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
			this.contrastTrackBar.Location = new System.Drawing.Point(8, 120);
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
			// openFileDialog
			// 
			this.openFileDialog.FileName = "ofd";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(600, 18);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(83, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Изображение";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(600, 36);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(267, 23);
			this.textBox1.TabIndex = 9;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(875, 36);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(88, 23);
			this.buttonSearch.TabIndex = 10;
			this.buttonSearch.Text = "Обзор";
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
			this.groupBox3.Location = new System.Drawing.Point(600, 398);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(158, 222);
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
			this.groupBox4.Location = new System.Drawing.Point(600, 149);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Size = new System.Drawing.Size(248, 113);
			this.groupBox4.TabIndex = 13;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Коррекция";
			// 
			// buttonMod
			// 
			this.buttonMod.Location = new System.Drawing.Point(8, 72);
			this.buttonMod.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonMod.Name = "buttonMod";
			this.buttonMod.Size = new System.Drawing.Size(150, 27);
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
			this.radioButton5.Size = new System.Drawing.Size(73, 19);
			this.radioButton5.TabIndex = 1;
			this.radioButton5.TabStop = true;
			this.radioButton5.Text = "autolevel";
			this.radioButton5.UseVisualStyleBackColor = true;
			// 
			// radioButton6
			// 
			this.radioButton6.AutoSize = true;
			this.radioButton6.Location = new System.Drawing.Point(8, 22);
			this.radioButton6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton6.Name = "radioButton6";
			this.radioButton6.Size = new System.Drawing.Size(150, 19);
			this.radioButton6.TabIndex = 0;
			this.radioButton6.TabStop = true;
			this.radioButton6.Text = "нелинейная степенная";
			this.radioButton6.UseVisualStyleBackColor = true;
			// 
			// histogramPanel
			// 
			this.histogramPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.histogramPanel.Location = new System.Drawing.Point(790, 398);
			this.histogramPanel.Name = "histogramPanel";
			this.histogramPanel.Size = new System.Drawing.Size(309, 222);
			this.histogramPanel.TabIndex = 14;
			// 
			// testButton
			// 
			this.testButton.BackColor = System.Drawing.Color.White;
			this.testButton.Location = new System.Drawing.Point(991, 79);
			this.testButton.Name = "testButton";
			this.testButton.Size = new System.Drawing.Size(117, 45);
			this.testButton.TabIndex = 20;
			this.testButton.Text = "Тестировать";
			this.testButton.UseVisualStyleBackColor = false;
			this.testButton.Click += new System.EventHandler(this.OnTest);
			// 
			// inputPictureBox
			// 
			this.inputPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.inputPictureBox.BackColor = System.Drawing.Color.White;
			this.inputPictureBox.Location = new System.Drawing.Point(9, 9);
			this.inputPictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.inputPictureBox.Name = "inputPictureBox";
			this.inputPictureBox.Size = new System.Drawing.Size(576, 324);
			this.inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.inputPictureBox.TabIndex = 11;
			this.inputPictureBox.TabStop = false;
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.outputPictureBox.BackColor = System.Drawing.Color.White;
			this.outputPictureBox.Location = new System.Drawing.Point(9, 344);
			this.outputPictureBox.Margin = new System.Windows.Forms.Padding(0);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(576, 324);
			this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.outputPictureBox.TabIndex = 12;
			this.outputPictureBox.TabStop = false;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(184, 71);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(13, 15);
			this.label14.TabIndex = 22;
			this.label14.Text = "2";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(16, 72);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(13, 15);
			this.label13.TabIndex = 15;
			this.label13.Text = "0";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(8, 50);
			this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.trackBar1.Maximum = 20;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(202, 45);
			this.trackBar1.TabIndex = 15;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll_1);
			// 
			// _saveResultsBox
			// 
			this._saveResultsBox.AutoSize = true;
			this._saveResultsBox.Location = new System.Drawing.Point(970, 38);
			this._saveResultsBox.Name = "_saveResultsBox";
			this._saveResultsBox.Size = new System.Drawing.Size(140, 19);
			this._saveResultsBox.TabIndex = 19;
			this._saveResultsBox.Text = "Сохранять результат";
			this._saveResultsBox.UseVisualStyleBackColor = true;
			// 
			// _threadCountBox
			// 
			this._threadCountBox.Location = new System.Drawing.Point(799, 101);
			this._threadCountBox.Maximum = new decimal(new int[] {
            16,
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
            4,
            0,
            0,
            0});
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(733, 80);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 15);
			this.label10.TabIndex = 16;
			this.label10.Text = "Количество потоков";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(600, 80);
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
			this._testCountBox.Location = new System.Drawing.Point(671, 101);
			this._testCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this._testCountBox.Name = "_testCountBox";
			this._testCountBox.Size = new System.Drawing.Size(49, 23);
			this._testCountBox.TabIndex = 18;
			this._testCountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.trackBar1);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Location = new System.Drawing.Point(600, 273);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(248, 102);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Изменение гаммы для нелинойной степенной коррекции";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 71);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(13, 15);
			this.label1.TabIndex = 23;
			this.label1.Text = "0";
			// 
			// Lab1Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1120, 677);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.histogramPanel);
			this.Controls.Add(this.inputPictureBox);
			this.Controls.Add(this.outputPictureBox);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this._saveResultsBox);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this._testCountBox);
			this.Controls.Add(this.testButton);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this._threadCountBox);
			this.Controls.Add(this.label4);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "Lab1Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lab1";
			this.Load += new System.EventHandler(this.Lab1Form_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._threadCountBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._testCountBox)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.Timer timer;
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
		private Button testButton;
		private ComboBox _channelBox;
		private RadioButton yuvRadio;
		private RadioButton hlsRadio;
		private RadioButton rgbRadio;
		private PictureBox inputPictureBox;
		private PictureBox outputPictureBox;
        private TrackBar trackBar1;
        private Label label14;
        private Label label13;
        private CheckBox _saveResultsBox;
        private NumericUpDown _threadCountBox;
        private Label label10;
        private Label label11;
        private NumericUpDown _testCountBox;
        private GroupBox groupBox2;
        private Label label1;
    }
}

