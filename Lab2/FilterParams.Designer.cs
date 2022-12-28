
using System.Windows.Forms;

namespace Lab2
{
    partial class FilterParams
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.label2 = new System.Windows.Forms.Label();
			this.submitButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.rectangleShapeRadio = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.widthBox = new System.Windows.Forms.NumericUpDown();
			this.heightBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.sharpnessBar = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.sharpnessPanel = new System.Windows.Forms.Panel();
			this.shapePanel = new System.Windows.Forms.Panel();
			this.matrixPanel = new System.Windows.Forms.Panel();
			this.sizePanel = new System.Windows.Forms.Panel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sharpnessBar)).BeginInit();
			this.sharpnessPanel.SuspendLayout();
			this.shapePanel.SuspendLayout();
			this.matrixPanel.SuspendLayout();
			this.sizePanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ColumnHeadersVisible = false;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dataGridView1.Location = new System.Drawing.Point(10, 22);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.Size = new System.Drawing.Size(403, 207);
			this.dataGridView1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(10, 10);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 15);
			this.label2.TabIndex = 3;
			this.label2.Text = "Элементы ядра";
			// 
			// submitButton
			// 
			this.submitButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.submitButton.Location = new System.Drawing.Point(288, 326);
			this.submitButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(88, 27);
			this.submitButton.TabIndex = 4;
			this.submitButton.Text = "Готово";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.onSubmit);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(58, 326);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(88, 27);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.OnCancel);
			// 
			// radioButton1
			// 
			this.rectangleShapeRadio.AutoSize = true;
			this.rectangleShapeRadio.Location = new System.Drawing.Point(11, 11);
			this.rectangleShapeRadio.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.rectangleShapeRadio.Name = "rectangleShapeRadio";
			this.rectangleShapeRadio.Size = new System.Drawing.Size(111, 19);
			this.rectangleShapeRadio.TabIndex = 6;
			this.rectangleShapeRadio.TabStop = true;
			this.rectangleShapeRadio.Text = "прямоугольная";
			this.rectangleShapeRadio.UseVisualStyleBackColor = true;
			this.rectangleShapeRadio.CheckedChanged += new System.EventHandler(this.OnRectangleShape);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(11, 36);
			this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(74, 19);
			this.radioButton2.TabIndex = 7;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "круговая";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.OnRoundShape);
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
            1,
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
            1,
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
			// sharpnessBar
			// 
			this.sharpnessBar.Location = new System.Drawing.Point(2, 28);
			this.sharpnessBar.Maximum = 50;
			this.sharpnessBar.Minimum = 1;
			this.sharpnessBar.Name = "sharpnessBar";
			this.sharpnessBar.Size = new System.Drawing.Size(120, 45);
			this.sharpnessBar.TabIndex = 12;
			this.sharpnessBar.Value = 1;
			this.sharpnessBar.Scroll += new System.EventHandler(this.sharpnessBar_Scroll);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 15);
			this.label1.TabIndex = 13;
			this.label1.Text = "Резкость";
			// 
			// sharpnessPanel
			// 
			this.sharpnessPanel.Controls.Add(this.label1);
			this.sharpnessPanel.Controls.Add(this.sharpnessBar);
			this.sharpnessPanel.Location = new System.Drawing.Point(136, 3);
			this.sharpnessPanel.Name = "sharpnessPanel";
			this.sharpnessPanel.Size = new System.Drawing.Size(125, 75);
			this.sharpnessPanel.TabIndex = 14;
			// 
			// shapePanel
			// 
			this.shapePanel.Controls.Add(this.rectangleShapeRadio);
			this.shapePanel.Controls.Add(this.radioButton2);
			this.shapePanel.Location = new System.Drawing.Point(267, 3);
			this.shapePanel.Name = "shapePanel";
			this.shapePanel.Size = new System.Drawing.Size(146, 75);
			this.shapePanel.TabIndex = 15;
			// 
			// matrixPanel
			// 
			this.matrixPanel.Controls.Add(this.label2);
			this.matrixPanel.Controls.Add(this.dataGridView1);
			this.matrixPanel.Location = new System.Drawing.Point(3, 84);
			this.matrixPanel.Name = "matrixPanel";
			this.matrixPanel.Padding = new System.Windows.Forms.Padding(10);
			this.matrixPanel.Size = new System.Drawing.Size(423, 239);
			this.matrixPanel.TabIndex = 16;
			// 
			// sizePanel
			// 
			this.sizePanel.Controls.Add(this.widthBox);
			this.sizePanel.Controls.Add(this.heightBox);
			this.sizePanel.Controls.Add(this.label3);
			this.sizePanel.Controls.Add(this.label4);
			this.sizePanel.Location = new System.Drawing.Point(3, 3);
			this.sizePanel.Name = "sizePanel";
			this.sizePanel.Size = new System.Drawing.Size(127, 75);
			this.sizePanel.TabIndex = 17;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flowLayoutPanel1.Controls.Add(this.sizePanel);
			this.flowLayoutPanel1.Controls.Add(this.sharpnessPanel);
			this.flowLayoutPanel1.Controls.Add(this.shapePanel);
			this.flowLayoutPanel1.Controls.Add(this.matrixPanel);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(439, 326);
			this.flowLayoutPanel1.TabIndex = 18;
			// 
			// FilterParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 358);
			this.Controls.Add(this.flowLayoutPanel1);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.submitButton);
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.Name = "FilterParams";
			this.Text = "Параметры фильтрации";
			this.Load += new System.EventHandler(this.FilterParams_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sharpnessBar)).EndInit();
			this.sharpnessPanel.ResumeLayout(false);
			this.sharpnessPanel.PerformLayout();
			this.shapePanel.ResumeLayout(false);
			this.shapePanel.PerformLayout();
			this.matrixPanel.ResumeLayout(false);
			this.matrixPanel.PerformLayout();
			this.sizePanel.ResumeLayout(false);
			this.sizePanel.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.RadioButton rectangleShapeRadio;
        private System.Windows.Forms.RadioButton radioButton2;
		private NumericUpDown widthBox;
		private NumericUpDown heightBox;
		private Label label3;
		private Label label4;
		private TrackBar sharpnessBar;
		private Label label1;
		private Panel sharpnessPanel;
		private Panel shapePanel;
		private Panel matrixPanel;
		private Panel sizePanel;
		private FlowLayoutPanel flowLayoutPanel1;
	}
}