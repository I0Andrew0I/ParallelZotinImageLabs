
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
			this.buttonViz = new System.Windows.Forms.Button();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.buttonMod = new System.Windows.Forms.Button();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.radioButton6 = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.inputPictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.outputPictureBox)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
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
			this.groupBox1.Controls.Add(this.buttonYUV);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.contrastTrackBar);
			this.groupBox1.Controls.Add(this.brightnessTrackBar);
			this.groupBox1.Location = new System.Drawing.Point(330, 482);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox1.Size = new System.Drawing.Size(588, 103);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Изменение яркости/контраста";
			// 
			// buttonYUV
			// 
			this.buttonYUV.Location = new System.Drawing.Point(484, 50);
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
			this.label3.Location = new System.Drawing.Point(284, 33);
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
			this.contrastTrackBar.Location = new System.Drawing.Point(275, 52);
			this.contrastTrackBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.contrastTrackBar.Maximum = 50;
			this.contrastTrackBar.Name = "contrastTrackBar";
			this.contrastTrackBar.Size = new System.Drawing.Size(202, 45);
			this.contrastTrackBar.TabIndex = 6;
			this.contrastTrackBar.Value = 5;
			this.contrastTrackBar.Scroll += new System.EventHandler(this.trackBar2_Scroll);
			// 
			// brightnessTrackBar
			// 
			this.brightnessTrackBar.Location = new System.Drawing.Point(29, 52);
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
			this.groupBox2.Location = new System.Drawing.Point(330, 408);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox2.Size = new System.Drawing.Size(412, 68);
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
			this.buttonHR.Click += new System.EventHandler(this.OnHLStoRGB);
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
			this.buttonRH.Click += new System.EventHandler(this.OnRGBtoHLS);
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "ofd";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(765, 405);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 15);
			this.label4.TabIndex = 8;
			this.label4.Text = "Путь к файлу";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(765, 423);
			this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(224, 23);
			this.textBox1.TabIndex = 9;
			// 
			// buttonSearch
			// 
			this.buttonSearch.Location = new System.Drawing.Point(997, 420);
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
			this.groupBox3.Controls.Add(this.buttonViz);
			this.groupBox3.Controls.Add(this.radioButton3);
			this.groupBox3.Controls.Add(this.radioButton2);
			this.groupBox3.Controls.Add(this.radioButton1);
			this.groupBox3.Location = new System.Drawing.Point(13, 395);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.groupBox3.Size = new System.Drawing.Size(118, 149);
			this.groupBox3.TabIndex = 11;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Канал";
			// 
			// buttonViz
			// 
			this.buttonViz.Location = new System.Drawing.Point(8, 111);
			this.buttonViz.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.buttonViz.Name = "buttonViz";
			this.buttonViz.Size = new System.Drawing.Size(98, 27);
			this.buttonViz.TabIndex = 3;
			this.buttonViz.Text = "Show";
			this.buttonViz.UseVisualStyleBackColor = true;
			this.buttonViz.Click += new System.EventHandler(this.onImageVisualize);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(8, 74);
			this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(77, 19);
			this.radioButton3.TabIndex = 2;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "B (синий)";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(8, 47);
			this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(91, 19);
			this.radioButton2.TabIndex = 1;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "G (зелёный)";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Location = new System.Drawing.Point(8, 22);
			this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(91, 19);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "R (красный)";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.buttonMod);
			this.groupBox4.Controls.Add(this.radioButton5);
			this.groupBox4.Controls.Add(this.radioButton6);
			this.groupBox4.Location = new System.Drawing.Point(157, 395);
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
			// Lab1Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1204, 599);
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
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button buttonYUV;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonMod;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton6;
    }
}

