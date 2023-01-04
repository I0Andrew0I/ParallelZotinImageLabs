using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Labs.Core;
using Labs.Core.Scheme;
using Labs.Core.Segmentation;

namespace Lab3
{
	delegate Func<int, int, (TimeSpan time, ImageBuffer<GrayScale> argb)> ContourResolver(ContourMethod filter, Frame frame, double threshold, double multiplier, bool positive);

	public partial class Benchmark : Form
	{
		private Dictionary<ContourMethod, bool> TestingMethods { get; set; } = new();
		private Frame FrameShape { get; set; }
		private bool RoundFrame { get; set; }

		private Bitmap? pic1;
		private Bitmap? pic2;
		private Bitmap? pic3;
		private Bitmap? pic4;
		private string? _savePath;
		private bool _positiveConv = true;
		private double _threshold;
		private double _multiplier;

		public Benchmark()
		{
			InitializeComponent();

			laplaceBox.CheckedChanged += UpdateTestingMethods;
			roberstBox.CheckedChanged += UpdateTestingMethods;
			sobelBox.CheckedChanged += UpdateTestingMethods;

			TestingMethods.Add(ContourMethod.Laplace, true);
			TestingMethods.Add(ContourMethod.Roberts, true);
			TestingMethods.Add(ContourMethod.Sobel, true);
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

			int mWidth = (int)widthBox.Value;
			int mHeight = (int)heightBox.Value;
			FrameShape = RoundFrame ? new EllipsisFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);
			Bitmap?[] pictures = new[] { pic1, pic2, pic3, pic4 };
			int testsCount = (int)testsBox.Value;

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
			foreach ((ContourMethod method, bool enabled) in TestingMethods)
			{
				if (!enabled) continue;
				localProgress.Value = 1;
				file.WriteLine(method);
				file.Write("Threads;");
				int picId = 0;

				if (method == ContourMethod.Sobel)
					var (coefGx, coefGy) = ContourMethods.GetGradient(_positiveConv);

				try
				{
					foreach (Bitmap? picture in pictures)
					{
						if (picture == null) continue;
						file.Write($"{picture.Width}x{picture.Height};;");
						picId++;

						ArraySegment<GrayScale> source = CopyImage(picture);

						// prepare non-generic test runner
						var testRunner = GetTestRunner(source, picture.Width, picture.Height)(method, FrameShape, _threshold, _multiplier, _positiveConv);

						for (int threads = 1; threads <= 4; threads++)
						{
							// run number of tests
							(TimeSpan methodTime, ImageBuffer<GrayScale> _) =
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

			ContourMethod updated = ContourMethod.Laplace;
			if (sender == laplaceBox)
				updated = ContourMethod.Laplace;
			else if (sender == roberstBox)
				updated = ContourMethod.Roberts;
			else if (sender == sobelBox)
				updated = ContourMethod.Sobel;

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


		private ContourResolver GetTestRunner(ArraySegment<GrayScale> input, int width, int height) =>
			(filter, frame, threshold, multiplier, positiveConv) =>
			{
				return (tests, threads) =>
				{
					var imageBuffer = new ImageBuffer<GrayScale>(input, width, height);
					(TimeSpan time, ImageBuffer<GrayScale> argb) = RunTests(imageBuffer, filter, frame, threshold,
						multiplier, positiveConv, new(tests, threads));

					return (time, argb);
				};
			};

		private (TimeSpan time, ImageBuffer<GrayScale> result) RunTests(
			ImageBuffer<GrayScale> source, ContourMethod method,
			Frame frame, double threshold, double multiplier,
			bool positiveConv, TestProperties test)
		{
			(int numTests, int numThreads) = test;

			ImageBuffer<GrayScale> tempResult = new ImageBuffer<GrayScale>(ArraySegment<GrayScale>.Empty, source.Width, source.Height);
			ArraySegment<GrayScale> targetBuffer = ArraySegment<GrayScale>.Empty;
			List<TimeSpan> tests = new();

			var (coefGx, coefGy) = ContourMethods.GetGradient(positiveConv);
			double[,] laplacian = ContourMethods.GetLaplacian(positiveConv);

			Trace.WriteLine("Starting tests...");
			for (int count = 0; count < numTests; count++)
			{
				targetBuffer = UtilityExtensions.PoolCopy(source.Pixels, targetBuffer);
				tempResult = tempResult with { Pixels = targetBuffer };

				var watch = Stopwatch.StartNew();
				switch (method)
				{
					case ContourMethod.Roberts:
						{
							ContourMethods.Roberts(source, tempResult, threshold, multiplier, numThreads);
							break;
						}
					case ContourMethod.Sobel:
						{
							ContourMethods.Sobel(source, tempResult, frame, threshold, multiplier, coefGx, coefGy, numThreads);
							break;
						}
					case ContourMethod.Laplace:
						{
							ContourMethods.Laplacian(source, tempResult, frame, threshold, multiplier, laplacian, numThreads);
							break;
						}
				}

				watch.Stop();
				tests.Add(watch.Elapsed);

				Trace.WriteLine($"Test {count} finished...");
				localProgress.Invoke(() => localProgress.PerformStep());
			}

			Trace.WriteLine("Calculating time...");

			TimeSpan time = UtilityExtensions.CalculateTime(tests);
			return (time, tempResult);
		}


		private Bitmap OpenImage(out string filename)
		{
			filename = "";
			using var dialog = new OpenFileDialog();
			if (dialog.ShowDialog() != DialogResult.OK) return null;

			filename = dialog.FileName;
			return (Bitmap)Bitmap.FromFile(filename);
		}

		private static ArraySegment<GrayScale> CopyImage(Bitmap picture)
		{
			Span<ARGB> data = picture.LockImage(out var locked).Cast<ARGB>();
			ArraySegment<GrayScale> result = ArraySegment<GrayScale>.Empty;
			Algorithms.RGBToGrayscale(data, ref result);
			picture.UnlockBits(locked);
			return result;
		}

		private void multiplierTrack_Scroll(object sender, EventArgs e)
		{
			_multiplier = multiplierTrack.Value;
		}

		private void thresholdTrack_Scroll(object sender, EventArgs e)
		{
			_threshold = thresholdTrack.Value;
		}
	}
}