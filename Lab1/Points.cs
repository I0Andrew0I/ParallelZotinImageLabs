using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Points : Form
    {
        private readonly List<Point> _points;
        internal double[] Y = new double[256];
        private Graphics gr;
        private readonly Pen _linePen;
        private readonly Pen _dotsPen;
        private double _canvasHeight;
        private double _canvasWidth;

        public Points()
        {
            InitializeComponent();
            _points = new List<Point>
            {
                new(0, 0)
            };

            _linePen = new Pen(Color.Black);
            _dotsPen = new Pen(Color.DimGray, 6);
        }

        private void Points_Load(object sender, EventArgs e)
        {
            _canvasHeight = pictureBox1.Size.Height;
            _canvasWidth = pictureBox1.Size.Width;
            gr = pictureBox1.CreateGraphics();
            gr.Clear(Color.White);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point nextPoint = NormalizePoint(e.Location);

            DrawPoint(nextPoint);
            _points.Add(nextPoint);
        }

        private void DrawPoint(Point point)
        {
            Point p1 = ToCanvasScale(_points.Last());
            Point p2 = ToCanvasScale(point);

            gr.DrawRectangle(_dotsPen, p2.X, p2.Y, 1, 1);
            gr.DrawLine(_linePen, p1, p2);
            gr.Flush();
        }

        private void onCompleted(object sender, EventArgs e)
        {
            if (_points.Count == 1)
            {
                MessageBox.Show("Отметьте хотя бы одну точку!");
                return;
            }

            if (_points.Last().X != 255)
                _points.Add(new Point(255, 255));

            for (int k = 0; k < _points.Count - 2; k++)
            {
                int x2x1 = _points[k + 1].X - _points[k].X;
                int y2y1 = _points[k + 1].Y - _points[k].Y;

                double contrast = x2x1 == 0 ? 0 : y2y1 / x2x1;
                double brightness = (_points[k].Y * x2x1 - _points[k].X * y2y1) / (double) x2x1;

                for (int a = _points[k].X; a < _points[k + 1].X; a++)
                    Y[a] = contrast * a + brightness;
            }

            Close();
        }

        private Point NormalizePoint(Point point)
        {
            Point delta = pictureBox1.Location;
            return new()
            {
                X = (int) ((point.X - delta.X) / _canvasWidth * 255d),
                Y = (int) ((_canvasHeight - (point.Y - delta.Y)) / _canvasHeight * 255d),
            };
        }

        private Point ToCanvasScale(Point point)
        {
            Point delta = pictureBox1.Location;
            return new()
            {
                X = (int) (point.X / 255d * _canvasWidth) + delta.X,
                Y = (int) (_canvasHeight - point.Y / 255d * _canvasHeight) + delta.Y,
            };
        }
    }
}