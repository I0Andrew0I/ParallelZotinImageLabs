using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labs.Core;
using Labs.Core.Filtering;
using Labs.Core.Scheme;
using Statistics = Labs.Core.Statistics;

namespace Lab2
{
    public enum Filter
    {
        Linear,
        Laplacian,
        Median,
        Mean,
        MinMax
    }

    public delegate (TimeSpan time, ImageBuffer<ARGB> result) TestRunner(int numTests, int numThreads);

    public delegate TestRunner MethodResolver(Filter filter, double[,] kernel, Frame frame);


    public partial class Lab2Form : Form
    {
        private Filter SelectedFilter { get; set; }

        private Enum _selectedChannels;
        private bool _roundFrame;

        private ArraySegment<ARGB> _sourceARGB;
        private ArraySegment<HLSA> _sourceHLSA;
        private ArraySegment<YUV> _sourceYUV;

        private double _noiseLevel = 0.0;
        private double _coef1 = 0.0;
        private double _coef2 = 0.0;
        private double[,] _kernelMatrix;
        private Frame _frameSize;

        double _sharpness;

        private int _height;
        private int _width;


        public Lab2Form()
        {
            InitializeComponent();

            fLaplacianRadioButton.CheckedChanged += OnFilterChanged;
            fLinearRadioButton.CheckedChanged += OnFilterChanged;
            fMinMaxRadioButton.CheckedChanged += OnFilterChanged;
            fMedianRadioButton.CheckedChanged += OnFilterChanged;
            fMeanRadioButton.CheckedChanged += OnFilterChanged;

            _channelBox.DataSource = Enum.GetValues(typeof(ARGB.Channel));
            _channelBox.SelectionChangeCommitted += ChannelBoxOnSelectionChangeCommitted;
        }

        private void OnFilterChanged(object? sender, EventArgs e)
        {
            if (fLaplacianRadioButton.Checked)
                SelectedFilter = Filter.Laplacian;
            if (fLinearRadioButton.Checked)
                SelectedFilter = Filter.Linear;
            if (fMinMaxRadioButton.Checked)
                SelectedFilter = Filter.MinMax;
            if (fMedianRadioButton.Checked)
                SelectedFilter = Filter.Median;
            if (fMeanRadioButton.Checked)
                SelectedFilter = Filter.Mean;
        }

        private void ChannelBoxOnSelectionChangeCommitted(object? sender, EventArgs e)
        {
            _selectedChannels = (Enum) _channelBox.SelectedItem;
        }

        private void OnImageLoaded(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var filePath = ofd.FileName;
            searchBox.Text = filePath;
            Bitmap image = (Bitmap) UtilityExtensions.ReadImage(filePath);

            inputPictureBox.Image = image;
            outputPictureBox.Image = new Bitmap(image);
            _height = image.Height;
            _width = image.Width;

            Span<ARGB> pixels = image.LockImage(out var sourceData).Cast<ARGB>();
            _sourceARGB = UtilityExtensions.PoolCopy(pixels, _sourceARGB);
            image.UnlockBits(sourceData);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label3.Text = "Коэффициент распределения импульсов";
                label3.Visible = true;
                label5.Text = "% 'белых'";
                label5.Visible = true;
                label6.Text = "% 'черных'";
                label6.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label3.Text = "Максимальное отклонение в +/- сторону";
                label3.Visible = true;
                label3.Visible = true;
                label5.Text = "от ";
                label5.Visible = true;
                label6.Text = "до ";
                label6.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                label3.Text = "Коэффициент умножения \n (принадлежит отрезку [0.001; 5])";
                label3.Visible = true;
                label3.Visible = true;
                label5.Text = "от ";
                label5.Visible = true;
                label6.Text = "до";
                label6.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void OnNoiseApply(object sender, EventArgs e)
        {
            if (_sourceARGB.Count == 0)
            {
                MessageBox.Show("Не выбрана картинка!");
                return;
            }

            if (isChannelUndefined())
            {
                MessageBox.Show("Канал не выбран");
                return;
            }

            _coef1 = double.Parse(textBox2.Text);
            _coef2 = double.Parse(textBox3.Text);

            string filename = "";
            var result = UtilityExtensions.PoolCopy<ARGB>(_sourceARGB);

            if (radioButton1.Checked) //импульсный
            {
                Noising.ImpulseNoise(result, (ARGB.Channel) _selectedChannels, _noiseLevel, _coef1, _coef2);
                filename = "/impulseNoise.png";
            }
            else if (radioButton2.Checked) //аддитивный
            {
                filename = "/additNoise.png";
                Noising.AdditiveNoise(result, (ARGB.Channel) _selectedChannels, _noiseLevel, (byte) _coef1, (byte) _coef2);
            }
            else if (radioButton3.Checked) //мультипликативный
            {
                Noising.MultiplicativeNoise(result, (ARGB.Channel) _selectedChannels, _noiseLevel, (byte) _coef1, (byte) _coef2);
                filename = "/multiNoise.png";
            }
            else
            {
                MessageBox.Show("Вы не выбрали вид шума!");
            }

            if (filename != "")
            {
                Bitmap image = ShowResult(result);

                FileInfo fileInfo = new(searchBox.Text);
                image.Save(fileInfo.DirectoryName + filename, ImageFormat.Png);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar1.Value.ToString();
            _noiseLevel = trackBar1.Value / 100.0;
        }

        private void OnConfigure(object sender, EventArgs e)
        {
            FilterParams fp = new FilterParams(SelectedFilter);
            if (fp.ShowDialog() != DialogResult.OK) return;

            SelectedFilter = fp.Filter;
            _roundFrame = fp.RoundFrame;
            _frameSize = _roundFrame
                ? new EllipsoidsFrame(0, 0, fp.MatrixSize.width, fp.MatrixSize.height)
                : new Frame(0, 0, fp.MatrixSize.width, fp.MatrixSize.height);

            if (SelectedFilter == Filter.Linear)
            {
                _kernelMatrix = fp.Matrix;
            }
            else if (SelectedFilter == Filter.Laplacian)
            {
                _sharpness = fp.Sharpness;
            }
        }


        private async void OnApplyFiltering(object sender, EventArgs e)
        {
            if (_sourceARGB.Count == 0)
            {
                MessageBox.Show("Не выбрана картинка!");
                return;
            }

            if (isChannelUndefined())
            {
                MessageBox.Show("Канал не выбран");
                return;
            }

            if (SelectedFilter == Filter.Mean)
            {
                if (_frameSize == null)
                {
                    MessageBox.Show("Не указаны параметры");
                    return;
                }

                _kernelMatrix = Filters.CalculateMean(_frameSize);
            }
            else if (SelectedFilter == Filter.Laplacian)
            {
                _kernelMatrix = Filters.CalculateLaplacian();
            }

            filteringGroup.Enabled = false;
            channelGroup.Enabled = false;

            // prepare non-generic test runner
            var testRunner = GetTestRunner(_selectedChannels)(SelectedFilter, _kernelMatrix, _frameSize);


            // run number of tests
            (TimeSpan methodTime, ImageBuffer<ARGB> result) =
                testRunner((int) testsBox.Value, (int) threadsBox.Value);
            // await Task.Factory.StartNew(() => testRunner((int) testsBox.Value, (int) threadsBox.Value));

            string elapsedTime = String.Format("{0:00}:{1:0000}", methodTime.TotalSeconds, methodTime.Milliseconds);

            Trace.WriteLine("Displaying result...");
            // show image
            FileInfo fileInfo = new(searchBox.Text);
            Bitmap bitmap = ShowResult(result.Pixels);

            string suffix = GetChannelSuffix(_selectedChannels);

            bitmap.Save($"{fileInfo.DirectoryName}\\{SelectedFilter}[{suffix}].png");

            Trace.WriteLine("Calculating metrics...");
            // calc metrics
            var MSAD = Statistics.MSAD(_sourceARGB, result.Pixels);
            var PSNR = Statistics.PSNR(_sourceARGB, result);
            var SSIM = Statistics.SSIM(_sourceARGB, result.Pixels);

            label9.Text = "MSAD: " + Math.Round(MSAD, 5);
            label10.Text = "PSNR: " + Math.Round(PSNR, 5);
            label11.Text = "SSIM: " + Math.Round(SSIM, 5);

            Trace.WriteLine("Testing finished...");
            MessageBox.Show($"Time {SelectedFilter} [{_selectedChannels.GetType()}] {elapsedTime}");
            filteringGroup.Enabled = true;
            channelGroup.Enabled = true;
        }

        private MethodResolver GetTestRunner(Enum channel) => (filter, kernel, frame) =>
        {
            var result = ArraySegment<ARGB>.Empty;
            var method = new MethodParameters(filter, frame, kernel);

            return channel switch
            {
                ARGB.Channel channels => RunTestsRGB(channels),
                HLSA.Channel channels => RunTestsHLSA(channels),
                YUV.Channel channels => RunTestsYUV(channels),

                _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, null)
            };

            TestRunner RunTestsRGB(ARGB.Channel channels) => (tests, threads) =>
            {
                var imageBuffer = new ImageBuffer<ARGB>(_sourceARGB, _width, _height);
                (TimeSpan time, ImageBuffer<ARGB> argb) = RunTests(imageBuffer, filter, frame,
                    new(tests, threads),
                    ConvolutionMethods.ARGB(imageBuffer, channels, _kernelMatrix, _sharpness)
                );

                return (time, argb);
            };

            TestRunner RunTestsHLSA(HLSA.Channel channels) => (tests, threads) =>
            {
                var imageBuffer = new ImageBuffer<HLSA>(_sourceHLSA, _width, _height);
                (TimeSpan time, ImageBuffer<HLSA> hlsa) = RunTests(imageBuffer, filter, frame,
                    new(tests, threads),
                    ConvolutionMethods.HLSA(imageBuffer, channels, _kernelMatrix, _sharpness)
                );

                Trace.WriteLine("Converting result...");
                Algorithms.HLSToRGB(hlsa.Pixels, ref result);
                return (time, new(result, _width, _height));
            };

            TestRunner RunTestsYUV(YUV.Channel channels) => (tests, threads) =>
            {
                var imageBuffer = new ImageBuffer<YUV>(_sourceYUV, _width, _height);
                (TimeSpan time, ImageBuffer<YUV> yuv) = RunTests(imageBuffer, filter, frame,
                    new(tests, threads),
                    ConvolutionMethods.YUV(imageBuffer, channels, _kernelMatrix, _sharpness)
                );

                Trace.WriteLine("Converting result...");
                Algorithms.YUVToRGB(yuv.Pixels, ref result);
                return (time, new(result, _width, _height));
            };
        };

        private (TimeSpan time, ImageBuffer<TPixel> result) RunTests<TPixel, TChannel>(
            ImageBuffer<TPixel> source,
            Filter filter,
            Frame frame,
            TestProperties test,
            ConvolutionModule<TPixel, TChannel> module)
            where TPixel : struct, IColor<TPixel, TChannel>
        {
            (int numTests, int numThreads) = test;

            ImageBuffer<TPixel> result = null;
            ArraySegment<TPixel> targetBuffer = ArraySegment<TPixel>.Empty;
            List<TimeSpan> tests = new();

            filteringProgress.Invoke(() =>
            {
                filteringProgress.Step = 1;
                filteringProgress.Value = 0;
                filteringProgress.Minimum = 0;
                filteringProgress.Maximum = numTests;
            });

            Trace.WriteLine("Starting tests...");
            for (int count = 0; count < numTests; count++)
            {
                targetBuffer = UtilityExtensions.PoolCopy(source.Pixels, targetBuffer);
                result = new(targetBuffer, source.Width, source.Height);

                ConvolutionMethod<TPixel, TChannel> testFunction = filter switch
                {
                    Filter.Linear => module.Linear,
                    Filter.Laplacian => module.Laplacian,
                    Filter.Median => module.Median,
                    Filter.Mean => module.MeanRecursive,
                    Filter.MinMax => module.MinMax,
                    _ => throw new ArgumentOutOfRangeException()
                };

                var watch = Stopwatch.StartNew();
                testFunction.Apply(frame, result, numThreads);
                watch.Stop();
                tests.Add(watch.Elapsed);

                Trace.WriteLine($"Test {count} finished...");
                filteringProgress.Invoke(() => filteringProgress.PerformStep());
            }

            Trace.WriteLine("Calculating time...");

            TimeSpan time = UtilityExtensions.CalculateTime(tests);
            return (time, result!);
        }

        
        private Bitmap ShowResult(ArraySegment<ARGB> result)
        {
            Bitmap image = (Bitmap) outputPictureBox.Image;
            image.CopyFrom(result);
            UtilityExtensions.Reuse(result);
            outputPictureBox.Image = image;
            return image;
        }

        private void OnSaveAsSource(object sender, EventArgs e)
        {
            if (outputPictureBox.Image == null || outputPictureBox.Image == inputPictureBox.Image)
            {
                MessageBox.Show("Не найдены изменения!");
                return;
            }

            Bitmap image = (Bitmap) outputPictureBox.Image;
            var data = image.LockImage(out var locked).Cast<ARGB>();
            data.CopyTo(_sourceARGB);
            image.UnlockBits(locked);

            _sourceYUV = UtilityExtensions.Reuse(_sourceYUV);
            _sourceHLSA = UtilityExtensions.Reuse(_sourceHLSA);

            inputPictureBox.Image = outputPictureBox.Image;
            outputPictureBox.Image = null;
        }

        private void OnRGBSelected(object sender, EventArgs e)
        {
            if (sender is not RadioButton {Checked: true}) return;
            _channelBox.DataSource = Enum.GetValues(typeof(ARGB.Channel));
        }

        private void OnHLSSelected(object sender, EventArgs e)
        {
            if (sender is not RadioButton {Checked: true}) return;

            _channelBox.DataSource = Enum.GetValues(typeof(HLSA.Channel));
            Algorithms.RGBToHLS(_sourceARGB, ref _sourceHLSA);
        }

        private void OnYUVSelected(object sender, EventArgs e)
        {
            if (sender is not RadioButton {Checked: true}) return;

            _channelBox.DataSource = Enum.GetValues(typeof(YUV.Channel));
            Algorithms.RGBToYUV(_sourceARGB, ref _sourceYUV);
        }

        private string GetChannelSuffix(Enum value)
        {
            var argb = new[] {ARGB.Channel.Alpha, ARGB.Channel.Red, ARGB.Channel.Blue, ARGB.Channel.Green};
            var hlsa = new[] {HLSA.Channel.Alpha, HLSA.Channel.Hue, HLSA.Channel.Saturation, HLSA.Channel.Lightness};
            var yuv = new[] {YUV.Channel.Y, YUV.Channel.U, YUV.Channel.V};

            var flags = value switch
            {
                ARGB.Channel channels => channels.GetFlags().IntersectBy(argb, c => c).Select(c => c.ToString()),
                HLSA.Channel channels => channels.GetFlags().IntersectBy(hlsa, c => c).Select(c => c.ToString()),
                YUV.Channel channels => channels.GetFlags().IntersectBy(yuv, c => c).Select(c => c.ToString()),
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
            return String.Join("", flags.Select(f => f.Substring(0, 1)));
        }

        private bool isChannelUndefined()
        {
            return _selectedChannels == null
                   || _selectedChannels.Equals(ARGB.Channel.Undefined)
                   || _selectedChannels.Equals(HLSA.Channel.Undefined)
                   || _selectedChannels.Equals(YUV.Channel.Undefined);
        }
    }
}