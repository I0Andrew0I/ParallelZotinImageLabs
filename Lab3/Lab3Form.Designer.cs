
using System.Windows.Forms;

namespace Lab3
{
    partial class Lab3Form
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
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.widthBox = new System.Windows.Forms.NumericUpDown();
			this.heightBox = new System.Windows.Forms.NumericUpDown();
			this.threadsBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.thresholdTrack = new System.Windows.Forms.TrackBar();
			this.multiplierTrack = new System.Windows.Forms.TrackBar();
			this.label6 = new System.Windows.Forms.Label();
			this.positiveRadio = new System.Windows.Forms.RadioButton();
			this.negativeRadio = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.globalThresholdRadio = new System.Windows.Forms.RadioButton();
			this.localThresholdRadio = new System.Windows.Forms.RadioButton();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.fastShrinkingRadio = new System.Windows.Forms.RadioButton();
			this.shrinkingRadio = new System.Windows.Forms.RadioButton();
			this.fastExpansionRadio = new System.Windows.Forms.RadioButton();
			this.expansionRadio = new System.Windows.Forms.RadioButton();
			this.button5 = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.graphSegmentationRadio = new System.Windows.Forms.RadioButton();
			this.button6 = new System.Windows.Forms.Button();
			this.histogramSegmentationRadio = new System.Windows.Forms.RadioButton();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.inputPictureBox = new System.Windows.Forms.PictureBox();
			this.outputPictureBox = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.inputLabel = new System.Windows.Forms.Label();
			this.contourBox = new System.Windows.Forms.ComboBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.threadsBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.thresholdTrack)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplierTrack)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(195, 598);
			this.buttonSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonSearch.Name = "buttonSearch";
			this.buttonSearch.Size = new System.Drawing.Size(88, 27);
			this.buttonSearch.TabIndex = 13;
			this.buttonSearch.Text = "Обзор...";
			this.buttonSearch.UseVisualStyleBackColor = true;
			this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
			// 
			// searchBox
			// 
			this.searchBox.Location = new System.Drawing.Point(6, 601);
			this.searchBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.searchBox.Name = "searchBox";
			this.searchBox.Size = new System.Drawing.Size(172, 23);
			this.searchBox.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 583);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 15);
			this.label4.TabIndex = 11;
			this.label4.Text = "Выберите файл:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(19, 486);
			this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 27);
			this.button1.TabIndex = 18;
			this.button1.Text = "GrayScale";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnConvertToGrayscale);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.widthBox);
			this.groupBox1.Controls.Add(this.heightBox);
			this.groupBox1.Controls.Add(this.threadsBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.thresholdTrack);
			this.groupBox1.Controls.Add(this.multiplierTrack);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.positiveRadio);
			this.groupBox1.Controls.Add(this.negativeRadio);
			this.groupBox1.Location = new System.Drawing.Point(893, 469);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(296, 165);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(238, 99);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 15);
			this.label5.TabIndex = 55;
			this.label5.Text = "Threads";
			// 
			// widthBox
			// 
			this.widthBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.widthBox.Location = new System.Drawing.Point(173, 101);
			this.widthBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.widthBox.Name = "widthBox";
			this.widthBox.Size = new System.Drawing.Size(53, 23);
			this.widthBox.TabIndex = 26;
			this.widthBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.widthBox.ValueChanged += new System.EventHandler(this.widthBox_ValueChanged);
			// 
			// heightBox
			// 
			this.heightBox.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.heightBox.Location = new System.Drawing.Point(173, 126);
			this.heightBox.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.heightBox.Name = "heightBox";
			this.heightBox.Size = new System.Drawing.Size(53, 23);
			this.heightBox.TabIndex = 27;
			this.heightBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.heightBox.ValueChanged += new System.EventHandler(this.heightBox_ValueChanged);
			// 
			// threadsBox
			// 
			this.threadsBox.Location = new System.Drawing.Point(238, 120);
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
			this.threadsBox.Size = new System.Drawing.Size(46, 23);
			this.threadsBox.TabIndex = 54;
			this.threadsBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(128, 103);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(39, 15);
			this.label3.TabIndex = 28;
			this.label3.Text = "Width";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(89, 64);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(25, 15);
			this.label11.TabIndex = 53;
			this.label11.Text = "255";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(128, 128);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(43, 15);
			this.label7.TabIndex = 29;
			this.label7.Text = "Height";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(17, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(13, 15);
			this.label13.TabIndex = 52;
			this.label13.Text = "1";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(18, 29);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(59, 15);
			this.label14.TabIndex = 51;
			this.label14.Text = "Threshold";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(104, 135);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(13, 15);
			this.label10.TabIndex = 50;
			this.label10.Text = "5";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(23, 135);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(13, 15);
			this.label9.TabIndex = 49;
			this.label9.Text = "1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 94);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 15);
			this.label2.TabIndex = 48;
			this.label2.Text = "Multiplier";
			// 
			// thresholdTrack
			// 
			this.thresholdTrack.Location = new System.Drawing.Point(11, 46);
			this.thresholdTrack.Maximum = 255;
			this.thresholdTrack.Minimum = 1;
			this.thresholdTrack.Name = "thresholdTrack";
			this.thresholdTrack.Size = new System.Drawing.Size(104, 45);
			this.thresholdTrack.TabIndex = 47;
			this.thresholdTrack.Value = 1;
			this.thresholdTrack.Scroll += new System.EventHandler(this.thresholdTrack_Scroll);
			// 
			// multiplierTrack
			// 
			this.multiplierTrack.Location = new System.Drawing.Point(13, 110);
			this.multiplierTrack.Maximum = 20;
			this.multiplierTrack.Name = "multiplierTrack";
			this.multiplierTrack.Size = new System.Drawing.Size(104, 45);
			this.multiplierTrack.TabIndex = 46;
			this.multiplierTrack.Scroll += new System.EventHandler(this.multiplierTrack_Scroll);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(137, 28);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 15);
			this.label6.TabIndex = 6;
			this.label6.Text = "Kernel";
			// 
			// positiveRadio
			// 
			this.positiveRadio.AutoSize = true;
			this.positiveRadio.Location = new System.Drawing.Point(137, 45);
			this.positiveRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.positiveRadio.Name = "positiveRadio";
			this.positiveRadio.Size = new System.Drawing.Size(66, 19);
			this.positiveRadio.TabIndex = 7;
			this.positiveRadio.TabStop = true;
			this.positiveRadio.Text = "positive";
			this.positiveRadio.UseVisualStyleBackColor = true;
			// 
			// negativeRadio
			// 
			this.negativeRadio.AutoSize = true;
			this.negativeRadio.Location = new System.Drawing.Point(137, 64);
			this.negativeRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.negativeRadio.Name = "negativeRadio";
			this.negativeRadio.Size = new System.Drawing.Size(70, 19);
			this.negativeRadio.TabIndex = 8;
			this.negativeRadio.TabStop = true;
			this.negativeRadio.Text = "negative";
			this.negativeRadio.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.globalThresholdRadio);
			this.groupBox2.Controls.Add(this.localThresholdRadio);
			this.groupBox2.Controls.Add(this.button3);
			this.groupBox2.Location = new System.Drawing.Point(749, 471);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(136, 158);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Binarization";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(7, 26);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(59, 15);
			this.label12.TabIndex = 30;
			this.label12.Text = "Threshold";
			// 
			// globalThresholdRadio
			// 
			this.globalThresholdRadio.AutoSize = true;
			this.globalThresholdRadio.Checked = true;
			this.globalThresholdRadio.Location = new System.Drawing.Point(7, 44);
			this.globalThresholdRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.globalThresholdRadio.Name = "globalThresholdRadio";
			this.globalThresholdRadio.Size = new System.Drawing.Size(58, 19);
			this.globalThresholdRadio.TabIndex = 24;
			this.globalThresholdRadio.TabStop = true;
			this.globalThresholdRadio.Text = "global";
			this.globalThresholdRadio.UseVisualStyleBackColor = true;
			// 
			// localThresholdRadio
			// 
			this.localThresholdRadio.AutoSize = true;
			this.localThresholdRadio.Location = new System.Drawing.Point(69, 44);
			this.localThresholdRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.localThresholdRadio.Name = "localThresholdRadio";
			this.localThresholdRadio.Size = new System.Drawing.Size(50, 19);
			this.localThresholdRadio.TabIndex = 25;
			this.localThresholdRadio.Text = "local";
			this.localThresholdRadio.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(10, 123);
			this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(88, 27);
			this.button3.TabIndex = 20;
			this.button3.Text = "Apply";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.OnApplyBinarization);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(6, 55);
			this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(88, 27);
			this.button2.TabIndex = 19;
			this.button2.Text = "Apply";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnCalculateContours);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button4);
			this.groupBox3.Controls.Add(this.fastShrinkingRadio);
			this.groupBox3.Controls.Add(this.shrinkingRadio);
			this.groupBox3.Controls.Add(this.fastExpansionRadio);
			this.groupBox3.Controls.Add(this.expansionRadio);
			this.groupBox3.Location = new System.Drawing.Point(590, 474);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(151, 155);
			this.groupBox3.TabIndex = 21;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Morphological filtering";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(16, 115);
			this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(88, 27);
			this.button4.TabIndex = 21;
			this.button4.Text = "Apply";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.OnApplyMorphologicalFiltering);
			// 
			// fastShrinkingRadio
			// 
			this.fastShrinkingRadio.AutoSize = true;
			this.fastShrinkingRadio.Location = new System.Drawing.Point(16, 87);
			this.fastShrinkingRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fastShrinkingRadio.Name = "fastShrinkingRadio";
			this.fastShrinkingRadio.Size = new System.Drawing.Size(105, 19);
			this.fastShrinkingRadio.TabIndex = 9;
			this.fastShrinkingRadio.TabStop = true;
			this.fastShrinkingRadio.Text = "Shrinking (fast)";
			this.fastShrinkingRadio.UseVisualStyleBackColor = true;
			// 
			// shrinkingRadio
			// 
			this.shrinkingRadio.AutoSize = true;
			this.shrinkingRadio.Location = new System.Drawing.Point(16, 68);
			this.shrinkingRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.shrinkingRadio.Name = "shrinkingRadio";
			this.shrinkingRadio.Size = new System.Drawing.Size(75, 19);
			this.shrinkingRadio.TabIndex = 8;
			this.shrinkingRadio.TabStop = true;
			this.shrinkingRadio.Text = "Shrinking";
			this.shrinkingRadio.UseVisualStyleBackColor = true;
			// 
			// fastExpansionRadio
			// 
			this.fastExpansionRadio.AutoSize = true;
			this.fastExpansionRadio.Location = new System.Drawing.Point(16, 47);
			this.fastExpansionRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.fastExpansionRadio.Name = "fastExpansionRadio";
			this.fastExpansionRadio.Size = new System.Drawing.Size(109, 19);
			this.fastExpansionRadio.TabIndex = 7;
			this.fastExpansionRadio.TabStop = true;
			this.fastExpansionRadio.Text = "Expansion (fast)";
			this.fastExpansionRadio.UseVisualStyleBackColor = true;
			// 
			// expansionRadio
			// 
			this.expansionRadio.AutoSize = true;
			this.expansionRadio.Location = new System.Drawing.Point(16, 28);
			this.expansionRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.expansionRadio.Name = "expansionRadio";
			this.expansionRadio.Size = new System.Drawing.Size(79, 19);
			this.expansionRadio.TabIndex = 6;
			this.expansionRadio.TabStop = true;
			this.expansionRadio.Text = "Expansion";
			this.expansionRadio.UseVisualStyleBackColor = true;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(115, 485);
			this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(147, 27);
			this.button5.TabIndex = 22;
			this.button5.Text = "Сделать исходным";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.OnSaveAsSource);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.graphSegmentationRadio);
			this.groupBox4.Controls.Add(this.button6);
			this.groupBox4.Controls.Add(this.histogramSegmentationRadio);
			this.groupBox4.Location = new System.Drawing.Point(438, 474);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox4.Size = new System.Drawing.Size(144, 114);
			this.groupBox4.TabIndex = 23;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Segmentation";
			// 
			// graphSegmentationRadio
			// 
			this.graphSegmentationRadio.AutoSize = true;
			this.graphSegmentationRadio.Location = new System.Drawing.Point(12, 48);
			this.graphSegmentationRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.graphSegmentationRadio.Name = "graphSegmentationRadio";
			this.graphSegmentationRadio.Size = new System.Drawing.Size(61, 19);
			this.graphSegmentationRadio.TabIndex = 12;
			this.graphSegmentationRadio.TabStop = true;
			this.graphSegmentationRadio.Text = "graphs";
			this.graphSegmentationRadio.UseVisualStyleBackColor = true;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(14, 78);
			this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(86, 27);
			this.button6.TabIndex = 0;
			this.button6.Text = "Apply";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.OnApplySegmentation);
			// 
			// histogramSegmentationRadio
			// 
			this.histogramSegmentationRadio.AutoSize = true;
			this.histogramSegmentationRadio.Location = new System.Drawing.Point(12, 23);
			this.histogramSegmentationRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.histogramSegmentationRadio.Name = "histogramSegmentationRadio";
			this.histogramSegmentationRadio.Size = new System.Drawing.Size(79, 19);
			this.histogramSegmentationRadio.TabIndex = 2;
			this.histogramSegmentationRadio.TabStop = true;
			this.histogramSegmentationRadio.Text = "расширение регионов";
			this.histogramSegmentationRadio.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.inputPictureBox, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.outputPictureBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.inputLabel, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1189, 457);
			this.tableLayoutPanel1.TabIndex = 30;
			// 
			// inputPictureBox
			// 
			this.inputPictureBox.BackColor = System.Drawing.Color.White;
			this.inputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputPictureBox.Location = new System.Drawing.Point(6, 25);
			this.inputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.inputPictureBox.Name = "inputPictureBox";
			this.inputPictureBox.Size = new System.Drawing.Size(584, 427);
			this.inputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.inputPictureBox.TabIndex = 11;
			this.inputPictureBox.TabStop = false;
			// 
			// outputPictureBox
			// 
			this.outputPictureBox.BackColor = System.Drawing.Color.White;
			this.outputPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputPictureBox.Location = new System.Drawing.Point(598, 25);
			this.outputPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.outputPictureBox.Name = "outputPictureBox";
			this.outputPictureBox.Size = new System.Drawing.Size(585, 427);
			this.outputPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.outputPictureBox.TabIndex = 12;
			this.outputPictureBox.TabStop = false;
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
			// contourBox
			// 
			this.contourBox.FormattingEnabled = true;
			this.contourBox.Location = new System.Drawing.Point(6, 22);
			this.contourBox.Name = "contourBox";
			this.contourBox.Size = new System.Drawing.Size(121, 23);
			this.contourBox.TabIndex = 31;
			this.contourBox.SelectedIndexChanged += new System.EventHandler(this.contourBox_SelectedIndexChanged);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.button2);
			this.groupBox5.Controls.Add(this.contourBox);
			this.groupBox5.Location = new System.Drawing.Point(286, 474);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(145, 95);
			this.groupBox5.TabIndex = 32;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Contour";
			// 
			// Lab3Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1194, 637);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonSearch);
			this.Controls.Add(this.searchBox);
			this.Controls.Add(this.label4);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MinimizeBox = false;
			this.Name = "Lab3Form";
			this.Text = "Лабораторная №3";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.threadsBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.thresholdTrack)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.multiplierTrack)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton negativeRadio;
        private System.Windows.Forms.RadioButton positiveRadio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RadioButton fastShrinkingRadio;
        private System.Windows.Forms.RadioButton shrinkingRadio;
        private System.Windows.Forms.RadioButton fastExpansionRadio;
        private System.Windows.Forms.RadioButton expansionRadio;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RadioButton localThresholdRadio;
        private System.Windows.Forms.RadioButton globalThresholdRadio;
        private System.Windows.Forms.RadioButton graphSegmentationRadio;
        private System.Windows.Forms.RadioButton histogramSegmentationRadio;
		private TableLayoutPanel tableLayoutPanel1;
		private PictureBox inputPictureBox;
		private PictureBox outputPictureBox;
		private Label label1;
		private Label inputLabel;
		private ComboBox contourBox;
		private Label label11;
		private Label label13;
		private Label label14;
		private Label label10;
		private Label label9;
		private Label label2;
		private TrackBar thresholdTrack;
		private TrackBar multiplierTrack;
		private GroupBox groupBox5;
		private Label label12;
		private NumericUpDown widthBox;
		private NumericUpDown heightBox;
		private Label label3;
		private Label label7;
		private Label label5;
		private NumericUpDown threadsBox;
	}
}