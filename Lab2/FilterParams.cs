using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Lab2
{
    public partial class FilterParams : Form
    {
        internal (int width, int height) MatrixSize;
        internal double[,]? Matrix;

        internal bool RoundFrame;
        internal double SliderValue;
        internal readonly Filter Filter;

        public FilterParams(Filter filter)
        {
            InitializeComponent();
            Filter = filter;
            MatrixSize = (3, 3);
            RoundFrame = false;
        }

        private void FilterParams_Load(object sender, EventArgs e)
        {
            matrixPanel.Visible = false;
            shapePanel.Visible = false;
            sliderPanel.Visible = false;
            sizePanel.Visible = false;

            if (Filter == Filter.Kasaburi)
            {
                sliderPanel.Visible = true;
                sliderLabel.Text = "Threshold";
                minSliderLabel.Text = "1";
                maxSliderLabel.Text = "100";
                sliderBar.Maximum = 100;
            }

            sizePanel.Visible = true;
            shapePanel.Visible = true;

            if (Filter == Filter.Linear)
            {
                matrixPanel.Visible = true;
                OnSizeUpdated(null, null);
            }


            rectangleShapeRadio.Checked = true;
            widthBox.ValueChanged += OnSizeUpdated;
            heightBox.ValueChanged += OnSizeUpdated;
        }

        private void onSubmit(object sender, EventArgs e)
        {
            if (Filter == Filter.Linear)
            {
                if (!SaveMatrix(out var matrix))
                {
                    MessageBox.Show("Коэффициент должен быть целым числом!");
                    return;
                }

                Matrix = matrix;
            }

            Close();
        }

        private bool SaveMatrix(out double[,] matrix)
        {
            double del = 0;
            matrix = new double[MatrixSize.height, MatrixSize.width];
            for (int y = 0; y < MatrixSize.height; y++)
            for (int x = 0; x < MatrixSize.width; x++)
            {
                if (!double.TryParse(dataGridView1[y, x].Value?.ToString() ?? "0", out double value))
                    return false;

                matrix[y, x] = value;
                del += value;
            }

            for (int y = 0; y < MatrixSize.height; y++)
            for (int x = 0; x < MatrixSize.width; x++)
                matrix[y, x] /= del;

            return true;
        }

        private void OnSizeUpdated(object? sender, EventArgs e)
        {
            (int w1, int h1) = (dataGridView1.ColumnCount, dataGridView1.RowCount);
            int w2 = (int) widthBox.Value;
            int h2 = (int) heightBox.Value;

            MatrixSize = (w2, h2);
            int dw = w2 - w1;
            int dh = h2 - h1;
            for (int i = 0; i < Math.Abs(dw); i++)
            {
                if (dw > 0)
                    dataGridView1.Columns.Add("Column", "Column");
                else if (dw < 0)
                    dataGridView1.Columns.RemoveAt(dataGridView1.ColumnCount - 1);
            }

            for (int i = 0; i < Math.Abs(dh); i++)
            {
                if (dh > 0)
                    dataGridView1.Rows.Add();
                else if (dh < 0)
                    dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 1);
            }
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void OnRectangleShape(object sender, EventArgs e)
        {
            RoundFrame = false;
        }

        private void OnRoundShape(object sender, EventArgs e)
        {
            RoundFrame = true;
        }

        private void sliderBar_Scroll(object sender, EventArgs e)
        {
            SliderValue = sliderBar.Value;
        }
    }
}