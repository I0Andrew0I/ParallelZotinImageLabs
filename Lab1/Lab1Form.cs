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
using Labs.Core.Scheme;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
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

        private Enum Channels;

        private readonly PlotView _histogram;

        private ArraySegment<byte>? _targetBuffer;
        private ArraySegment<byte>? _sourceBuffer;
        private ArraySegment<HLSA>? _hlsaPixels;
        private ArraySegment<YUV>? _yuvPixels;
        private Benchmark _benchmarkForm;

        public Lab1Form()
        {
            InitializeComponent();
            _benchmarkForm = new Benchmark();
            _histogram = new PlotView() {Dock = DockStyle.Fill};
            histogramPanel.Controls.Add(_histogram);

            _contrast = contrastTrackBar.Value / 10.0;

            rgbRadio.CheckedChanged += ConvertColorModel;
            hlsRadio.CheckedChanged += ConvertColorModel;
            yuvRadio.CheckedChanged += ConvertColorModel;

            var rgb = ARGB.Channel.All;
            _channelBox.DataSource = Enum.GetValues(rgb.GetType());
            _channelBox.SelectionChangeCommitted += SetChannels;
            Channels = rgb;
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

                Channels = ARGB.Channel.All;
                inputPictureBox.Image = image;
                outputPictureBox.Image = new Bitmap(image);

                if (_sourceBuffer is {Count: >0, Array: { }} memory)
                    ArrayPool<byte>.Shared.Return(memory.Array);

                _sourceBuffer = image.CopyBytes();

                UpdateHistogram(_sourceBuffer.Value.Cast<ARGB>());
            }
        }

        private void SetChannels(object? sender, EventArgs e)
        {
            Channels = (Enum) _channelBox.SelectedItem;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            _brightness = brightnessTrackBar.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            _contrast = contrastTrackBar.Value / 10.0;
        }

        private void ConvertColorModel(object? sender, EventArgs e)
        {
            if (sender is not RadioButton {Checked: true}) return;

            object channels;
            if (sender == rgbRadio)
                channels = ARGB.Channel.All;
            else if (sender == hlsRadio)
                channels = HLSA.Channel.All;
            else if (sender == yuvRadio)
                channels = YUV.Channel.All;
            else return;

            if (channels.GetType() == Channels.GetType())
            {
                MessageBox.Show("Изображение уже представлено в этой цветовой модели");
                return;
            }

            _channelBox.DataSource = Enum.GetValues(channels.GetType());
            Channels = (Enum) channels;

            if (channels.Equals(ARGB.Channel.All) && _sourceBuffer != null)
            {
                UpdateHistogram(_sourceBuffer.Value.Cast<ARGB>());
            }

            if (channels.Equals(HLSA.Channel.All) && _sourceBuffer != null)
            {
                ArraySegment<HLSA> result = _hlsaPixels ?? ArraySegment<HLSA>.Empty;
                Algorithms.RGBToHLS(_sourceBuffer.Value.Cast<ARGB>(), ref result);
                UpdateHistogram(result.AsSpan());
                _hlsaPixels = result;
            }

            if (channels.Equals(YUV.Channel.All) && _sourceBuffer != null)
            {
                ArraySegment<YUV> result = _yuvPixels ?? ArraySegment<YUV>.Empty;
                Algorithms.RGBToYUV(_sourceBuffer.Value.Cast<ARGB>(), ref result);
                UpdateHistogram(result.AsSpan());
                _yuvPixels = result;
            }
        }

        private void onFilterChannels(object sender, EventArgs e)
        {
            buttonViz.Enabled = false;
            var image = (Bitmap) outputPictureBox.Image;
            Span<byte> bytes = image.LockImage(out var locked);
            Span<ARGB> outputPixel = bytes.Cast<ARGB>();

            ApplyFiltration(outputPixel);

            image.UnlockBits(locked);
            outputPictureBox.Image = image;
            buttonViz.Enabled = true;
        }

        private void ApplyFiltration(Span<ARGB> outputPixel)
        {
            switch (Channels)
            {
                case ARGB.Channel channels:
                {
                    if (_sourceBuffer is not { } rgbArray)
                        return;

                    _targetBuffer ??= CopyFromSource(rgbArray);
                    Span<ARGB> inputPixel = _targetBuffer.Value.Cast<ARGB>();

                    FilterChannels(inputPixel, outputPixel, channels);
                    break;
                }
                case HLSA.Channel channels:
                {
                    if (_hlsaPixels is not { } hlsaArray)
                        return;

                    FilterChannels(hlsaArray.AsSpan(), outputPixel, channels);
                    break;
                }
                case YUV.Channel channels:
                {
                    if (_yuvPixels is not { } yuvArray)
                        return;

                    FilterChannels(yuvArray.AsSpan(), outputPixel, channels);
                    break;
                }
            }
        }

        private void FilterChannels(Span<HLSA> input, Span<ARGB> output, HLSA.Channel channels)
        {
            ARGB value = default(ARGB);

            bool HUE = channels.HasFlag(HLSA.Channel.Hue);
            bool LIG = channels.HasFlag(HLSA.Channel.Lightness);
            bool SAT = channels.HasFlag(HLSA.Channel.Saturation);

            for (int i = 0; i < output.Length; i++)
            {
                HLSA pixel = input[i];
                if (HUE)
                {
                    pixel.L = 0.5;
                    pixel.S = 1;
                    value = pixel.ToARGB();
                }
                else if (LIG)
                {
                    var v = (byte) Math.Round(pixel.L * 255.0);
                    value = new ARGB(v, v, v, 255);
                }
                else if (SAT)
                {
                    var v = (byte) Math.Round(pixel.S * 255.0);
                    value = new ARGB(v, v, v, 255);
                }

                output[i] = value;
            }
        }

        private void FilterChannels(Span<ARGB> input, Span<ARGB> output, ARGB.Channel channels)
        {
            bool RED = channels.HasFlag(ARGB.Channel.Red);
            bool BLUE = channels.HasFlag(ARGB.Channel.Blue);
            bool GREEN = channels.HasFlag(ARGB.Channel.Green);

            for (int i = 0; i < output.Length; i++)
            {
                byte r = 0, g = 0, b = 0;
                //получить цвет пикселя
                var pix = input[i];

                if (RED)
                    r = pix.R;
                if (GREEN)
                    g = pix.G;
                if (BLUE)
                    b = pix.B;

                output[i].R = r;
                output[i].G = g;
                output[i].B = b;
                output[i].A = 255;
            }
        }


        private void FilterChannels(Span<YUV> input, Span<ARGB> output, YUV.Channel channels)
        {
            ARGB value = default(ARGB);

            bool Y = channels.HasFlag(YUV.Channel.Y);
            bool U = channels.HasFlag(YUV.Channel.U);
            bool V = channels.HasFlag(YUV.Channel.V);

            for (int i = 0; i < output.Length; i++)
            {
                YUV pixel = input[i];
                if (Y)
                {
                    pixel.U = 0;
                    pixel.V = 0;
                    value = pixel.ToARGB();
                }
                else if (U)
                {
                    pixel.V = 0;
                    value = pixel.ToARGB();
                }
                else if (V)
                {
                    pixel.U = 0;
                    value = pixel.ToARGB();
                }

                output[i] = value;
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

        private void UpdateHistogram(Span<YUV> pixels)
        {
            var hist = Algorithms.Histogram(pixels);
            DrawChart(_histogram, hist, ("Y", "U", "V"));
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
            model.IsLegendVisible = true;
            model.Legends.Add(new Legend(){LegendPlacement = LegendPlacement.Inside, IsLegendVisible = true});
            plotView.Model = model;
        }

        private void OnTest(object sender, EventArgs e)
        {
            _benchmarkForm.Show();
        }
    }
}