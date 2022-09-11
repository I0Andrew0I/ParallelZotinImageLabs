using System;
using System.Buffers;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Labs.Core;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Lab1
{
    public partial class Lab1Form : Form
    {
        private string _filePath;
        private string _curModel;
        private int _coefY;
        private int _coefC;
        private int _fileNameCounter = 0;
        private double[] _correctionCurve;
        private bool IsRGB = false;

        private readonly PlotView _histogram;

        private ArraySegment<byte>? _targetBuffer;
        private ArraySegment<byte>? _sourceBuffer;

        public Lab1Form()
        {
            InitializeComponent();
            _histogram = new PlotView() {Dock = DockStyle.Fill};
            histogramPanel.Controls.Add(_histogram);

            inputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            outputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _filePath = ofd.FileName;
                textBox1.Text = _filePath;

                Bitmap image = (Bitmap) UtilityExtensions.ReadImage(_filePath);

                IsRGB = true;
                inputPictureBox.Image = image;
                outputPictureBox.Image = new Bitmap(image);

                if (_sourceBuffer is {Count: >0, Array: { }} memory)
                    ArrayPool<byte>.Shared.Return(memory.Array);

                _sourceBuffer = image.CopyBytes();

                UpdateHistogram(IsRGB);
            }
        }

        private void OnRGBtoHLS(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            if (_curModel == "HLS")
            {
                MessageBox.Show("Изображение уже представлено в цветовой модели " + _curModel);
                return;
            }

            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);
            Algorithms.RGBToHLS(targetMemory);
            
            SaveImageCopy(targetMemory, "fromRGBtoHLS");
            UpdateHistogram(false);

            _curModel = "HLS";
            radioButton1.Text = "H (тон)";
            radioButton2.Text = "L (яркость)";
            radioButton3.Text = "S (насыщенность)";
        }

        private void OnHLStoRGB(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            if (_curModel == "RGB")
            {
                MessageBox.Show("Изображение уже представлено в цветовой модели " + _curModel);
                return;
            }

            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);
            Algorithms.HLStoRGB(targetMemory);
            
            SaveImageCopy(targetMemory, "fromHLStoRGB");
            
            UpdateHistogram(true);

            _curModel = "RGB";
            radioButton1.Text = "R (красный)";
            radioButton2.Text = "G (зелёный)";
            radioButton3.Text = "B (синий)";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _coefY = brightnessTrackBar.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            _coefC = contrastTrackBar.Value / 10;
        }


        private void onImageVisualize(object sender, EventArgs e)
        {
            if (_sourceBuffer is not { } sourceMemory)
                return;

            var image = (Bitmap) outputPictureBox.Image;
            Span<byte> bytes = image.LockImage(out var locked);

            Span<ARGB> inputPixel = sourceMemory.Cast<ARGB>();
            Span<ARGB> outputPixel = bytes.Cast<ARGB>();

            for (int i = 0; i < outputPixel.Length; i++)
            {
                byte r = 0, g = 0, b = 0;
                //получить цвет пикселя
                var pix = inputPixel[i];

                if (radioButton1.Checked)
                    r = pix.R;
                if (radioButton2.Checked)
                    g = pix.G;
                if (radioButton3.Checked)
                    b = pix.B;

                outputPixel[i].R = r;
                outputPixel[i].G = g;
                outputPixel[i].B = b;
                outputPixel[i].A = 255;
            }

            image.UnlockBits(locked);

            outputPictureBox.Image = image;
        }

        private void onTransformImage(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);


            var stopWatch = Stopwatch.StartNew();
            Algorithms.TransformImage(targetMemory, _coefY, _coefC);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}.{1:00}", ts.TotalSeconds, ts.Milliseconds / 10);
            MessageBox.Show("Яркость/контрастность " + elapsedTime);

            SaveImageCopy(_targetBuffer.Value);
        }

        private void onModification(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);

            double[] correctionCurve = Array.Empty<double>();
            if (radioButton6.Checked)
            {
                Points getPoints = new Points();
                if (getPoints.ShowDialog() == DialogResult.OK)
                    correctionCurve = getPoints.Y;
                else
                    return;
            }

            var stopWatch = Stopwatch.StartNew();
            Algorithms.ColorCorrection(targetMemory, correctionCurve, radioButton6.Checked);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            MessageBox.Show("Яркость/контрастность " + elapsedTime);
            SaveImageCopy(targetMemory);
        }

        private void UpdateHistogram(bool isRgb)
        {
            var target = (Bitmap) outputPictureBox.Image;
            var hist = Algorithms.Histogram(target);
            var labels = isRgb ? ("Red", "Green", "Blue") : ("Hue", "Lightness", "Saturation");
            DrawChart(_histogram, hist, labels);
        }


        private void SaveImageCopy(ArraySegment<byte> imageBuffer) =>
            SaveImageCopy(imageBuffer, "change" + _fileNameCounter);

        private void SaveImageCopy(ArraySegment<byte> imageBuffer, string fileName)
        {
            FileInfo fileInfo = new(_filePath);
            Bitmap resultBmp = new(inputPictureBox.Image.Width, inputPictureBox.Image.Height);

            resultBmp.CopyFrom(imageBuffer);
            resultBmp.Save(fileInfo.DirectoryName + "/" + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            outputPictureBox.Image = resultBmp;
            _fileNameCounter++;
        }

        private ArraySegment<byte> CopyFromSource(ArraySegment<byte> sourceMemory)
        {
            if (_targetBuffer is not { } targetMemory)
            {
                byte[] rent = ArrayPool<byte>.Shared.Rent(sourceMemory.Count);
                targetMemory = new ArraySegment<byte>(rent, 0, sourceMemory.Count);
                _targetBuffer = targetMemory;
            }

            if (targetMemory.Count < sourceMemory.Count)
            {
                ArrayPool<byte>.Shared.Return(targetMemory.Array);
                byte[] rent = ArrayPool<byte>.Shared.Rent(sourceMemory.Count);
                targetMemory = new ArraySegment<byte>(rent, 0, sourceMemory.Count);
                _targetBuffer = targetMemory;
            }

            sourceMemory.CopyTo(targetMemory);
            return targetMemory;
        }

        void DrawChart(PlotView plotView, (uint[] R, uint[] G, uint[] B) hist, (string, string, string) Titles)
        {
            var first = new LineSeries() {Title = Titles.Item1, Color = OxyColor.FromRgb(255, 0, 0)};
            first.Points.AddRange(hist.R.Select((p, i) => new DataPoint(i, p)));

            var second = new LineSeries() {Title = Titles.Item2, Color = OxyColor.FromRgb(10, 255, 0)};
            second.Points.AddRange(hist.G.Select((p, i) => new DataPoint(i, p)));

            var third = new LineSeries() {Title = Titles.Item3, Color = OxyColor.FromRgb(0, 10, 255)};
            third.Points.AddRange(hist.B.Select((p, i) => new DataPoint(i, p)));

            var model = new PlotModel()
            {
                Series = {first, second, third},
                Axes =
                {
                    new LinearAxis() {Position = AxisPosition.Bottom, Minimum = 0, Maximum = 255},
                    new LinearAxis() {Position = AxisPosition.Left, Minimum = 0},
                }
            };
            plotView.Model = model;
        }
    }
}