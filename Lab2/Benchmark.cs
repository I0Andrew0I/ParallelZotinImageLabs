using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labs.Core;
using Labs.Core.Filtering;
using Labs.Core.Scheme;

namespace Lab2
{
    public partial class Benchmark : Form
    {
        private Dictionary<Filter, bool> TestingMethods { get; set; } = new();
        private Frame FrameShape { get; set; }
        private bool RoundFrame { get; set; }

        private Bitmap? pic1;
        private Bitmap? pic2;
        private Bitmap? pic3;
        private Bitmap? pic4;
        private string? _savePath;

        public Benchmark()
        {
            InitializeComponent();

            laplacianBox.CheckedChanged += UpdateTestingMethods;
            minMaxBox.CheckedChanged += UpdateTestingMethods;
            medianBox.CheckedChanged += UpdateTestingMethods;
            meanBox.CheckedChanged += UpdateTestingMethods;
            linearBox.CheckedChanged += UpdateTestingMethods;

            TestingMethods.Add(Filter.Laplacian, true);
            TestingMethods.Add(Filter.Linear, true);
            TestingMethods.Add(Filter.Mean, true);
            TestingMethods.Add(Filter.Median, true);
            TestingMethods.Add(Filter.MinMax, true);
        }

        double Speed(double sync, double parallel)
        {
            return sync / parallel;
        }

        private async void OnStart(object sender, EventArgs _)
        {
            if (_savePath == null)
            {
                MessageBox.Show("Выберите куда сохранить файл");
                return;
            }

            TextWriter file;
            try
            {
                file = new StreamWriter(_savePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Файл занят другой программой");
                return;
            }

            int mWidth = (int) widthBox.Value;
            int mHeight = (int) heightBox.Value;
            FrameShape = RoundFrame ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);
            Bitmap?[] pictures = new[] {pic1, pic2, pic3, pic4};
            int testsCount = (int) testsBox.Value;
            double[,] meanKernel = Kernel.CalculateMean(FrameShape);
            double[,] laplacian = Kernel.CalculateLaplacian();
            double[,] linear = CalculateLinear(FrameShape);


            int pictureCount = pictures.Count(p => p != null);

            localProgress.Value = 1;
            localProgress.Maximum = 4 * testsCount * pictureCount;
            localProgress.Minimum = 1;
            localProgress.Step = 1;

            overallProgress.Value = 1;
            overallProgress.Maximum = TestingMethods.Values.Count(v => v);
            overallProgress.Minimum = 1;
            overallProgress.Step = 1;
            startButton.Enabled = false;

            TimeSpan[,] times = new TimeSpan[4, pictureCount];
            foreach ((Filter method, bool enabled) in TestingMethods)
            {
                if (!enabled) continue;
                localProgress.Value = 1;
                file.WriteLine(method);
                file.Write("Threads;");
                int picId = 0;


                var shape = method == Filter.Laplacian
                    ? new Frame(0, 0, 3, 3)
                    : FrameShape;

                var matrix = method switch
                {
                    Filter.Laplacian => laplacian,
                    Filter.Mean => meanKernel,
                    _ => linear
                };

                try
                {
                    foreach (Bitmap? picture in pictures)
                    {
                        if (picture == null) continue;
                        file.Write($"{picture.Width}x{picture.Height};;");
                        picId++;

                        ArraySegment<ARGB> source = CopyImage(picture);

                        // prepare non-generic test runner
                        var testRunner = GetTestRunner(source, picture.Width, picture.Height)(method, matrix, shape);

                        for (int threads = 1; threads <= 4; threads++)
                        {
                            // run number of tests
                            (TimeSpan methodTime, ImageBuffer<ARGB> _) =
                                await Task.Factory.StartNew(() => testRunner(testsCount, threads));

                            times[threads - 1, picId - 1] = methodTime;
                        }


                        UtilityExtensions.Reuse(source);
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceWarning(e.Message);
                }
                finally
                {
                    if (overallProgress.InvokeRequired)
                        overallProgress.Invoke(() => overallProgress.PerformStep());
                    else overallProgress.PerformStep();

                    WriteMetrics(file, pictureCount, times);
                }
            }

            file.Close();
            MessageBox.Show("Finished!");
            startButton.Enabled = true;
        }

        private void WriteMetrics(TextWriter file, int pictureCount, TimeSpan[,] times)
        {
            file.WriteLine(";;");

            file.Write(";");
            for (int j = 0; j < pictureCount; j++)
                file.Write("Time;Speed;");

            file.WriteLine(";;");

            for (int i = 0; i < 4; i++)
            {
                file.Write(i + 1);
                file.Write(";");
                for (int j = 0; j < pictureCount; j++)
                {
                    TimeSpan time = times[i, j];
                    file.Write($"{Math.Round(time.TotalSeconds)},{time.Milliseconds:0000}");
                    file.Write(";");
                    file.Write(Speed(times[0, j].TotalMilliseconds, time.TotalMilliseconds));
                    file.Write(";");
                }

                file.WriteLine(";");
            }
        }


        private void OnOutputPath(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _savePath = saveFileDialog1.FileName;
                textBox5.Text = _savePath;
            }
        }

        private void UpdateTestingMethods(object? sender, EventArgs e)
        {
            if (sender == null) return;

            Filter updated = Filter.Linear;
            if (sender == laplacianBox)
                updated = Filter.Laplacian;
            else if (sender == minMaxBox)
                updated = Filter.MinMax;
            else if (sender == medianBox)
                updated = Filter.Median;
            else if (sender == meanBox)
                updated = Filter.Mean;
            else if (sender == linearBox)
                updated = Filter.Linear;

            TestingMethods[updated] = !TestingMethods[updated];
        }

        private void OnPic1Select(object sender, EventArgs e)
        {
            pic1 = OpenImage(out var name);
            textBox1.Text = name;
        }

        private void OnPic2Select(object sender, EventArgs e)
        {
            pic2 = OpenImage(out var name);
            textBox2.Text = name;
        }

        private void OnPic3Select(object sender, EventArgs e)
        {
            pic3 = OpenImage(out var name);
            textBox3.Text = name;
        }

        private void OnPic4Select(object sender, EventArgs e)
        {
            pic4 = OpenImage(out var name);
            textBox4.Text = name;
        }

        private double[,] CalculateLinear(Frame f)
        {
            double[,] matrix = new double[f.Height, f.Width];
            for (int y = 0; y < f.Height; y++)
            for (int x = 0; y < f.Width; y++)
                matrix[y, x] = x % 3 + y % 2;

            return matrix;
        }

        private MethodResolver GetTestRunner(ArraySegment<ARGB> input, int width, int height) => (filter, kernel, frame) =>
        {
            return (tests, threads) =>
            {
                var imageBuffer = new ImageBuffer<ARGB>(input, width, height);
                (TimeSpan time, ImageBuffer<ARGB> argb) = RunTests(imageBuffer, filter, frame,
                    new(tests, threads),
                    ConvolutionMethods.ARGB(imageBuffer, ARGB.Channel.RGB, kernel, 1.5)
                );

                return (time, argb);
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

            ConvolutionMethod<TPixel, TChannel> testFunction = filter switch
            {
                Filter.Linear => module.Linear,
                Filter.Laplacian => module.Laplacian,
                Filter.Median => module.Median,
                Filter.Mean => module.MeanRecursive,
                Filter.MinMax => module.MinMax,
                _ => throw new ArgumentOutOfRangeException()
            };

            Trace.WriteLine("Starting tests...");
            for (int count = 0; count < numTests; count++)
            {
                targetBuffer = UtilityExtensions.PoolCopy(source.Pixels, targetBuffer);
                result = new(targetBuffer, source.Width, source.Height);

                var watch = Stopwatch.StartNew();
                testFunction.Apply(frame, result, numThreads);
                watch.Stop();
                tests.Add(watch.Elapsed);

                Trace.WriteLine($"Test {count} finished...");
                localProgress.Invoke(() => localProgress.PerformStep());
            }

            Trace.WriteLine("Calculating time...");

            TimeSpan time = UtilityExtensions.CalculateTime(tests);
            return (time, result!);
        }


        private Bitmap OpenImage(out string filename)
        {
            filename = "";
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return null;

            filename = dialog.FileName;
            return (Bitmap) Bitmap.FromFile(filename);
        }

        private static ArraySegment<ARGB> CopyImage(Bitmap picture)
        {
            var data = picture.LockImage(out var locked).Cast<ARGB>();
            ArraySegment<ARGB> source = UtilityExtensions.PoolCopy(data);
            picture.UnlockBits(locked);
            return source;
        }
    }
}