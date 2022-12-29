using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labs.Core;

namespace Lab1
{
    enum CorrectionMethod
    {
        Brightness,
        AutoLevels,
        NonLinearPowerCorrection
    }

    public partial class Benchmark : Form
    {
        private Bitmap? pic1;
        private Bitmap? pic2;
        private Bitmap? pic3;
        private Bitmap? pic4;
        private string? _savePath;
        private double _contast;
        private int _brightness;


        public Benchmark()
        {
            InitializeComponent();
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

            Bitmap?[] pictures = new[] {pic1, pic2, pic3, pic4};
            int testsCount = (int) testsBox.Value;


            int pictureCount = pictures.Count(p => p != null);


            TimeSpan[,] times = new TimeSpan[4, pictureCount];
            var methods = new[]
            {
                CorrectionMethod.Brightness,
                CorrectionMethod.NonLinearPowerCorrection,
                CorrectionMethod.AutoLevels
            };
            localProgress.Value = 1;
            localProgress.Maximum = 4 * testsCount * pictureCount;
            localProgress.Minimum = 1;
            localProgress.Step = 1;

            overallProgress.Value = 1;
            overallProgress.Maximum = methods.Length;
            overallProgress.Minimum = 1;
            overallProgress.Step = 1;
            startButton.Enabled = false;

            float gamma = (float)trackBar1.Value / 10;

            foreach (var method in methods)
            {
                localProgress.Value = 1;
                file.WriteLine(method);
                file.Write("Threads;");
                int picId = 0;

                try
                {
                    foreach (Bitmap? picture in pictures)
                    {
                        if (picture == null) continue;
                        file.Write($"{picture.Width}x{picture.Height};;");
                        picId++;

                        ArraySegment<byte> source = CopyImage(picture);

                        for (int threads = 1; threads <= 4; threads++)
                        {
                            // run number of tests
                            TimeSpan methodTime =
                                await Task.Factory.StartNew(() =>
                                    RunTests(source, method, gamma, _brightness, _contast, testsCount, threads));

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


        private void contrastTrackBar_Scroll(object sender, EventArgs e)
        {
            _contast = contrastTrackBar.Value / 10.0;
        }

        private void brightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            _brightness = brightnessTrackBar.Value;
        }

        private TimeSpan RunTests(
            ArraySegment<byte> source, CorrectionMethod method,
            float gamma, int brightness, double contrast,
            int numTests, int numThreads)
        {
            ArraySegment<byte> targetBuffer = ArraySegment<byte>.Empty;
            List<TimeSpan> tests = new();

            Trace.WriteLine("Starting tests...");
            for (int count = 0; count < numTests; count++)
            {
                targetBuffer = UtilityExtensions.PoolCopy(source, targetBuffer);

                var watch = Stopwatch.StartNew();
                switch (method)
                {
                    case CorrectionMethod.Brightness:
                        Algorithms.TransformImage(targetBuffer, brightness, contrast, numThreads);
                        break;
                    case CorrectionMethod.AutoLevels:
                        Algorithms.ColorCorrection(targetBuffer, gamma, false, numThreads);
                        break;
                    case CorrectionMethod.NonLinearPowerCorrection:
                        Algorithms.ColorCorrection(targetBuffer, gamma, true, numThreads);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(method), method, null);
                }

                watch.Stop();
                tests.Add(watch.Elapsed);
                UtilityExtensions.Reuse(targetBuffer);

                Trace.WriteLine($"Test {count} finished...");
                localProgress.Invoke(() => localProgress.PerformStep());
            }

            Trace.WriteLine("Calculating time...");

            return UtilityExtensions.CalculateTime(tests);
        }


        private Bitmap OpenImage(out string filename)
        {
            filename = "";
            using var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() != DialogResult.OK) return null;

            filename = dialog.FileName;
            return (Bitmap) Bitmap.FromFile(filename);
        }

        private static ArraySegment<byte> CopyImage(Bitmap picture)
        {
            var data = picture.LockImage(out var locked);
            ArraySegment<byte> source = UtilityExtensions.PoolCopy(data);
            picture.UnlockBits(locked);
            return source;
        }

        private void testsBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}