using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        private int _brightness;
        private double _contrast;
        private int _fileNameCounter = 0;
        private double[] _correctionCurve;
        private bool IsRGB = false;

        private readonly PlotView _histogram;

        private ArraySegment<byte>? _targetBuffer;
        private ArraySegment<byte>? _sourceBuffer;
        private ArraySegment<HLSA>? _hlsaPixels;

        public Lab1Form()
        {
            InitializeComponent();
            _histogram = new PlotView() {Dock = DockStyle.Fill};
            histogramPanel.Controls.Add(_histogram);

            inputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            outputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            _contrast = contrastTrackBar.Value / 10.0;
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

                UpdateHistogram(_sourceBuffer.Value.Cast<ARGB>());
            }
        }

        private void OnRGBtoHLS(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            if (IsRGB == false)
            {
                MessageBox.Show("Изображение уже представлено в этой цветовой модели");
                return;
            }

            IsRGB = false;
            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);
            _hlsaPixels = Algorithms.RGBToHLS(targetMemory);

            ShowResult(targetMemory);
            UpdateHistogram(_hlsaPixels.Value.AsSpan());

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

            if (IsRGB == true)
            {
                MessageBox.Show("Изображение уже представлено в этой цветовой модели");
                return;
            }

            IsRGB = true;
            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);
            Algorithms.HLStoRGB(_hlsaPixels.Value, ref targetMemory);

            ShowResult(targetMemory);

            UpdateHistogram(targetMemory.Cast<ARGB>());

            radioButton1.Text = "R (красный)";
            radioButton2.Text = "G (зелёный)";
            radioButton3.Text = "B (синий)";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _brightness = brightnessTrackBar.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            _contrast = contrastTrackBar.Value / 10.0;
        }


        private void onFilterChannels(object sender, EventArgs e)
        {
            var image = (Bitmap) outputPictureBox.Image;
            Span<byte> bytes = image.LockImage(out var locked);
            Span<ARGB> outputPixel = bytes.Cast<ARGB>();

            if (IsRGB)
            {
                if (_sourceBuffer is not { } sourceMemory)
                    return;

                _targetBuffer ??= CopyFromSource(sourceMemory);
                Span<ARGB> inputPixel = _targetBuffer.Value.Cast<ARGB>();
                FilterRGBChannel(inputPixel, outputPixel);
            }
            else
            {
                if (_hlsaPixels is not { } hlsaArray)
                    return;

                FilterHLSChannel(hlsaArray.AsSpan(), outputPixel);
            }

            image.UnlockBits(locked);
            outputPictureBox.Image = image;
        }

        private void FilterHLSChannel(Span<HLSA> input, Span<ARGB> output)
        {
            ARGB value = default(ARGB);
            for (int i = 0; i < output.Length; i++)
            {
                HLSA pixel = input[i];
                if (radioButton1.Checked)
                {
                    pixel.L = 0.5;
                    pixel.S = 1;
                    value = pixel.ToARGB();
                }

                if (radioButton2.Checked)
                {
                    var v = (byte) Math.Round(pixel.L * 255.0);
                    value = new ARGB(v, v, v, 255);
                }

                if (radioButton3.Checked)
                {
                    var v = (byte) Math.Round(pixel.S * 255.0);
                    value = new ARGB(v, v, v, 255);
                }

                output[i] = value;
            }
        }

        private void FilterRGBChannel(Span<ARGB> input, Span<ARGB> output)
        {
            for (int i = 0; i < output.Length; i++)
            {
                byte r = 0, g = 0, b = 0;
                //получить цвет пикселя
                var pix = input[i];

                if (radioButton1.Checked)
                    r = pix.R;
                if (radioButton2.Checked)
                    g = pix.G;
                if (radioButton3.Checked)
                    b = pix.B;

                output[i].R = r;
                output[i].G = g;
                output[i].B = b;
                output[i].A = 255;
            }
        }

        private void onTransformImage(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }


            List<TimeSpan> tests = new();
            for (int count = 0; count < _testCountBox.Value; count++)
            {
                ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);

                var stopWatch = Stopwatch.StartNew();
                Algorithms.TransformImage(targetMemory, _brightness, _contrast, (int) _threadCountBox.Value);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                tests.Add(ts);
                _targetBuffer = targetMemory;
            }

            var time = UtilityExtensions.CalculateTime(tests);

            string elapsedTime = String.Format("{0:00}.{1:00}", time.TotalSeconds, time.Milliseconds / 10);
            MessageBox.Show("Яркость/контрастность " + elapsedTime);

            if (_saveResultsBox.Checked)
                SaveImageCopy(_targetBuffer.Value);
            else
                ShowResult(_targetBuffer.Value);
        }

        private void onModification(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }


            double[] correctionCurve = Array.Empty<double>();
            if (radioButton6.Checked)
            {
                Points getPoints = new Points();
                if (getPoints.ShowDialog() == DialogResult.OK)
                    correctionCurve = getPoints.Y;
                else
                    return;
            }

            List<TimeSpan> tests = new();
            for (int count = 0; count < _testCountBox.Value; count++)
            {
                ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);
                var stopWatch = Stopwatch.StartNew();
                Algorithms.ColorCorrection(targetMemory, correctionCurve, radioButton6.Checked, (int) _threadCountBox.Value);
                stopWatch.Stop();
                tests.Add(stopWatch.Elapsed);
                _targetBuffer = targetMemory;
            }

            TimeSpan time = UtilityExtensions.CalculateTime(tests);

            string elapsedTime = String.Format("{0:00}:{1:00}", time.TotalSeconds, time.Milliseconds / 10);
            MessageBox.Show("Яркость/контрастность " + elapsedTime);

            if (_saveResultsBox.Checked)
                SaveImageCopy(_targetBuffer.Value);
            else
                ShowResult(_targetBuffer.Value);
        }

        private void UpdateHistogram(Span<ARGB> pixels)
        {
            var hist = Algorithms.Histogram(pixels);
            DrawChart(_histogram, hist, ("Red", "Green", "Blue"));
        }

        private void UpdateHistogram(Span<HLSA> pixels)
        {
            var hist = Algorithms.Histogram(pixels);
            var labels = ("Hue", "Lightness", "Saturation");
            DrawChart(_histogram, hist, labels);
        }


        private void SaveImageCopy(ArraySegment<byte> imageBuffer) =>
            SaveImageCopy(imageBuffer, "change" + _fileNameCounter);

        private void SaveImageCopy(ArraySegment<byte> imageBuffer, string fileName)
        {
            FileInfo fileInfo = new(_filePath);
            Bitmap resultBmp = ShowResult(imageBuffer);

            resultBmp.Save(fileInfo.DirectoryName + "/" + fileName + ".png", System.Drawing.Imaging.ImageFormat.Png);
            _fileNameCounter++;
        }

        private Bitmap ShowResult(ArraySegment<byte> imageBuffer)
        {
            Bitmap resultBmp;
            if (outputPictureBox.Image == null
                || outputPictureBox.Image.Width != inputPictureBox.Image.Width
                || outputPictureBox.Image.Height != inputPictureBox.Image.Height)
            {
                resultBmp = new(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            }
            else
            {
                resultBmp = (Bitmap) outputPictureBox.Image;
            }

            resultBmp.CopyFrom(imageBuffer);
            outputPictureBox.Image = resultBmp;
            return resultBmp;
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