
using System.Windows.Forms;

namespace Lab2
{
    partial class Lab2Form
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
			this.buttonSearch = new System.Windows.Forms.Button();
			this.searchBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.channelGroup = new System.Windows.Forms.GroupBox();
			this._channelBox = new System.Windows.Forms.ComboBox();
			this.radioButton14 = new System.Windows.Forms.RadioButton();
			this.radioButton13 = new System.Windows.Forms.RadioButton();
			this.radioButton12 = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.inputLabel = new System.Windows.Forms.Label();
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			this.inputPictureBox = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.button1 = new System.Windows.Forms.Button();
			this.filteringGroup = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.threadsBox = new System.Windows.Forms.NumericUpDown();
			this.testsBox = new System.Windows.Forms.NumericUpDown();
			this.filteringProgress = new System.Windows.Forms.ProgressBar();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.fMedianRadioButton = new System.Windows.Forms.RadioButton();
			this.fMeanRadioButton = new System.Windows.Forms.RadioButton();
			this.fMinMaxRadioButton = new System.Windows.Forms.RadioButton();
			this.fLaplacianRadioButton = new System.Windows.Forms.RadioButton();
			this.fLinearRadioButton = new System.Windows.Forms.RadioButton();
			this.button4 = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.channelGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.filteringGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.threadsBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(226, 749);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(88, 27);
			this.buttonSearch.TabIndex = 18;
			this.buttonSearch.Text = "Обзор...";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.OnImageLoaded);
			// 
			// searchBox
			// 
			this.searchBox.Location = new System.Drawing.Point(5, 749);
			this.searchBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(213, 23);
			this.searchBox.TabIndex = 17;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(4, 734);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 15);
			this.label4.TabIndex = 16;
			this.label4.Text = "Путь к файлу";
			// 
			// channelGroup
			// 
			this.channelGroup.Controls.Add(this._channelBox);
			this.channelGroup.Controls.Add(this.radioButton14);
			this.channelGroup.Controls.Add(this.radioButton13);
			this.channelGroup.Controls.Add(this.radioButton12);
			this.channelGroup.Location = new System.Drawing.Point(548, 479);
			this.channelGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.channelGroup.Name = "channelGroup";
			this.channelGroup.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.channelGroup.Size = new System.Drawing.Size(321, 68);
			this.channelGroup.TabIndex = 15;
			this.channelGroup.TabStop = false;
			this.channelGroup.Text = "Выбор модели/канала";
			// 
			// _channelBox
			// 
			this._channelBox.FormattingEnabled = true;
			this._channelBox.Location = new System.Drawing.Point(185, 30);
			this._channelBox.Name = "_channelBox";
			this._channelBox.Size = new System.Drawing.Size(121, 23);
			this._channelBox.TabIndex = 4;
			// 
			// radioButton14
			// 
			this.radioButton14.AutoSize = true;
			this.radioButton14.Location = new System.Drawing.Point(118, 31);
			this.radioButton14.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton14.Name = "radioButton14";
			this.radioButton14.Size = new System.Drawing.Size(47, 19);
			this.radioButton14.TabIndex = 2;
			this.radioButton14.TabStop = true;
			this.radioButton14.Text = "YUV";
			this.radioButton14.UseVisualStyleBackColor = true;
			this.radioButton14.CheckedChanged += new System.EventHandler(this.OnYUVSelected);
			// 
			// radioButton13
			// 
			this.radioButton13.AutoSize = true;
			this.radioButton13.Location = new System.Drawing.Point(64, 31);
			this.radioButton13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton13.Name = "radioButton13";
			this.radioButton13.Size = new System.Drawing.Size(46, 19);
			this.radioButton13.TabIndex = 1;
			this.radioButton13.TabStop = true;
			this.radioButton13.Text = "HLS";
			this.radioButton13.UseVisualStyleBackColor = true;
			this.radioButton13.CheckedChanged += new System.EventHandler(this.OnHLSSelected);
			// 
			// radioButton12
			// 
			this.radioButton12.AutoSize = true;
			this.radioButton12.Checked = true;
			this.radioButton12.Location = new System.Drawing.Point(9, 31);
			this.radioButton12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton12.Name = "radioButton12";
			this.radioButton12.Size = new System.Drawing.Size(47, 19);
			this.radioButton12.TabIndex = 0;
			this.radioButton12.TabStop = true;
			this.radioButton12.Text = "RGB";
			this.radioButton12.UseVisualStyleBackColor = true;
			this.radioButton12.CheckedChanged += new System.EventHandler(this.OnRGBSelected);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(598, 2);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 15);
			this.label1.TabIndex = 14;
			this.label1.Text = "Результат:";
			// 
			// inputLabel
			// 
			this.inputLabel.AutoSize = true;
			this.inputLabel.Location = new System.Drawing.Point(6, 2);
			this.inputLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.inputLabel.Name = "inputLabel";
			this.inputLabel.Size = new System.Drawing.Size(141, 15);
			this.inputLabel.TabIndex = 13;
			this.inputLabel.Text = "Исходное изображение:";
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.BackColor = System.Drawing.Color.White;
			this.outputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputPictureBox.Location = new System.Drawing.Point(598, 25);
			this.outputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(585, 340);
			this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.outputPictureBox.TabIndex = 12;
			this.outputPictureBox.TabStop = false;
			// 
			// inputPictureBox
			// 
			this.inputPictureBox.BackColor = System.Drawing.Color.White;
			this.inputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputPictureBox.Location = new System.Drawing.Point(6, 25);
			this.inputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.inputPictureBox.Name = "inputPictureBox";
			this.inputPictureBox.Size = new System.Drawing.Size(584, 340);
			this.inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.inputPictureBox.TabIndex = 11;
			this.inputPictureBox.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButton3);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(23, 534);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(160, 180);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Выбор вида шума";
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(7, 132);
			this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(139, 19);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "мультипликативный";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(7, 83);
			this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(91, 19);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "аддитивный";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(7, 38);
			this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(96, 19);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "импульсный";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.textBox3);
			this.groupBox3.Controls.Add(this.textBox2);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.trackBar1);
			this.groupBox3.Location = new System.Drawing.Point(203, 534);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(316, 180);
			this.groupBox3.TabIndex = 20;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Настройка параметров";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(223, 32);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(10, 15);
			this.label7.TabIndex = 8;
			this.label7.Text = " ";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(150, 145);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 15);
			this.label6.TabIndex = 7;
			this.label6.Text = "% \"черных\"";
			this.label6.Visible = false;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 145);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 15);
			this.label5.TabIndex = 6;
			this.label5.Text = "% \"белых\"";
			this.label5.Visible = false;
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(226, 142);
			this.textBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(41, 23);
			this.textBox3.TabIndex = 5;
			this.textBox3.Visible = false;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(84, 142);
			this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(44, 23);
			this.textBox2.TabIndex = 4;
			this.textBox2.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 105);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(235, 15);
			this.label3.TabIndex = 3;
			this.label3.Text = "Коэффициент распределения импульсов\r\n";
			this.label3.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(167, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Уровень зашумленности в %";
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(20, 61);
			this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(271, 45);
			this.trackBar1.TabIndex = 0;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(392, 727);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(127, 27);
			this.button1.TabIndex = 21;
			this.button1.Text = "Наложить шум";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnNoiseApply);
			// 
			// filteringGroup
			// 
			this.filteringGroup.Controls.Add(this.label12);
			this.filteringGroup.Controls.Add(this.label13);
			this.filteringGroup.Controls.Add(this.threadsBox);
			this.filteringGroup.Controls.Add(this.testsBox);
			this.filteringGroup.Controls.Add(this.filteringProgress);
			this.filteringGroup.Controls.Add(this.button3);
			this.filteringGroup.Controls.Add(this.button2);
			this.filteringGroup.Controls.Add(this.fMedianRadioButton);
			this.filteringGroup.Controls.Add(this.fMeanRadioButton);
			this.filteringGroup.Controls.Add(this.fMinMaxRadioButton);
			this.filteringGroup.Controls.Add(this.fLaplacianRadioButton);
			this.filteringGroup.Controls.Add(this.fLinearRadioButton);
			this.filteringGroup.Location = new System.Drawing.Point(548, 553);
			this.filteringGroup.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.filteringGroup.Name = "filteringGroup";
			this.filteringGroup.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.filteringGroup.Size = new System.Drawing.Size(481, 210);
			this.filteringGroup.TabIndex = 22;
			this.filteringGroup.TabStop = false;
			this.filteringGroup.Text = "Фильтрация";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(332, 111);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(123, 15);
			this.label12.TabIndex = 33;
			this.label12.Text = "Количество попыток";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(335, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(120, 15);
			this.label13.TabIndex = 32;
			this.label13.Text = "Количество потоков";
			// 
			// threadsBox
			// 
			this.threadsBox.Location = new System.Drawing.Point(391, 86);
			this.threadsBox.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
			this.threadsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.threadsBox.Name = "threadsBox";
			this.threadsBox.Size = new System.Drawing.Size(64, 23);
			this.threadsBox.TabIndex = 31;
			this.threadsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// testsBox
			// 
			this.testsBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.testsBox.Location = new System.Drawing.Point(391, 129);
			this.testsBox.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.testsBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.testsBox.Name = "testsBox";
			this.testsBox.Size = new System.Drawing.Size(64, 23);
			this.testsBox.TabIndex = 30;
			this.testsBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// filteringProgress
			// 
			this.filteringProgress.Location = new System.Drawing.Point(160, 170);
			this.filteringProgress.Name = "filteringProgress";
			this.filteringProgress.Size = new System.Drawing.Size(200, 27);
			this.filteringProgress.TabIndex = 29;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(367, 170);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 27);
			this.button3.TabIndex = 23;
			this.button3.Text = "Готово";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.OnApplyFiltering);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 170);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(124, 27);
			this.button2.TabIndex = 22;
			this.button2.Text = "Задать параметры";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnConfigure);
			// 
			// fMedianRadioButton
			// 
			this.fMedianRadioButton.AutoSize = true;
			this.fMedianRadioButton.Location = new System.Drawing.Point(16, 48);
			this.fMedianRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fMedianRadioButton.Name = "fMedianRadioButton";
			this.fMedianRadioButton.Size = new System.Drawing.Size(142, 19);
			this.fMedianRadioButton.TabIndex = 4;
			this.fMedianRadioButton.Text = "быстрый медианный";
			this.fMedianRadioButton.UseVisualStyleBackColor = true;
			// 
			// fMeanRadioButton
			// 
			this.fMeanRadioButton.AutoSize = true;
			this.fMeanRadioButton.Location = new System.Drawing.Point(16, 98);
			this.fMeanRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fMeanRadioButton.Name = "fMeanRadioButton";
			this.fMeanRadioButton.Size = new System.Drawing.Size(279, 19);
			this.fMeanRadioButton.TabIndex = 3;
			this.fMeanRadioButton.Text = "рекурсивный среднеарифметический фильтр";
			this.fMeanRadioButton.UseVisualStyleBackColor = true;
			// 
			// fMinMaxRadioButton
			// 
			this.fMinMaxRadioButton.AutoSize = true;
			this.fMinMaxRadioButton.Location = new System.Drawing.Point(16, 73);
			this.fMinMaxRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fMinMaxRadioButton.Name = "fMinMaxRadioButton";
			this.fMinMaxRadioButton.Size = new System.Drawing.Size(231, 19);
			this.fMinMaxRadioButton.TabIndex = 2;
			this.fMinMaxRadioButton.Text = "максимума/минимума, (max+min)/2";
			this.fMinMaxRadioButton.UseVisualStyleBackColor = true;
			// 
			// fLaplacianRadioButton
			// 
			this.fLaplacianRadioButton.AutoSize = true;
			this.fLaplacianRadioButton.Location = new System.Drawing.Point(16, 122);
			this.fLaplacianRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fLaplacianRadioButton.Name = "fLaplacianRadioButton";
			this.fLaplacianRadioButton.Size = new System.Drawing.Size(278, 19);
			this.fLaplacianRadioButton.TabIndex = 1;
			this.fLaplacianRadioButton.Text = "повышение резкости (на основе Лапласиана)";
			this.fLaplacianRadioButton.UseVisualStyleBackColor = true;
			// 
			// fLinearRadioButton
			// 
			this.fLinearRadioButton.AutoSize = true;
			this.fLinearRadioButton.Location = new System.Drawing.Point(16, 25);
			this.fLinearRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fLinearRadioButton.Name = "fLinearRadioButton";
			this.fLinearRadioButton.Size = new System.Drawing.Size(78, 19);
			this.fLinearRadioButton.TabIndex = 0;
			this.fLinearRadioButton.Text = "линейная";
			this.fLinearRadioButton.UseVisualStyleBackColor = true;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(1037, 377);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(149, 27);
			this.button4.TabIndex = 24;
			this.button4.Text = "Сделать исходным";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.OnSaveAsSource);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(1062, 567);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(59, 30);
			this.label8.TabIndex = 25;
			this.label8.Text = "Метрики:\r\n\r\n";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(1046, 607);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(43, 15);
			this.label9.TabIndex = 26;
			this.label9.Text = "MSAD:";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(1046, 639);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(39, 15);
			this.label10.TabIndex = 27;
			this.label10.Text = "PSNR:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(1046, 668);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(36, 15);
			this.label11.TabIndex = 28;
			this.label11.Text = "SSIM:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.inputPictureBox, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.outputPictureBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.inputLabel, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1189, 370);
			this.tableLayoutPanel1.TabIndex = 29;
			// 
			// Lab2Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1189, 777);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.filteringGroup);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.searchBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.channelGroup);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "Lab2Form";
			this.Text = "Lab2Form";
			this.channelGroup.ResumeLayout(false);
			this.channelGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.filteringGroup.ResumeLayout(false);
			this.filteringGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.threadsBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.testsBox)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox channelGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.PictureBox outputPictureBox;
        private System.Windows.Forms.PictureBox inputPictureBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox filteringGroup;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton fMedianRadioButton;
        private System.Windows.Forms.RadioButton fMeanRadioButton;
        private System.Windows.Forms.RadioButton fMinMaxRadioButton;
        private System.Windows.Forms.RadioButton fLaplacianRadioButton;
        private System.Windows.Forms.RadioButton fLinearRadioButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton radioButton14;
        private System.Windows.Forms.RadioButton radioButton13;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
		private ComboBox _channelBox;
		private ProgressBar filteringProgress;
		private NumericUpDown testsBox;
		private NumericUpDown threadsBox;
		private Label label12;
		private Label label13;
		private TableLayoutPanel tableLayoutPanel1;
	}
}