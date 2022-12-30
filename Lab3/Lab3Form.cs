using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Labs.Core;
using Labs.Core.Scheme;
using Labs.Core.Segmentation;

namespace Lab3
{
    enum ContourMethod
    {
        Roberts,
        Sobel,
        Laplace
    }

    enum StepDirection
    {
        None,
        Left,
        Up,
        Right,
        Down
    }

    public struct MinPixelCoordinates
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public double Value { get; private set; }

        public MinPixelCoordinates(int x, int y, double value)
        {
            X = x;
            Y = y;
            Value = value;
        }

        public void Set(int x, int y, double value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }

    public partial class Lab3Form : Form
    {
        private readonly Random _rand = new();

        private ContourMethod _methodContur;
        private bool _positiveConv;
        private double _threshold;
        private double _multiplier;
        private int _frameWidth;
        private int _frameHeight;

        private ImageBuffer<ARGB>? _originalSource;
        private ImageBuffer<GrayScale>? _grayScaleSource;
        private ArraySegment<GrayScale>? _temporary;

        private FileInfo _imageFile;

        public Lab3Form()
        {
            InitializeComponent();

            positiveRadio.CheckedChanged += OnKernelSignChanged;
            negativeRadio.CheckedChanged += OnKernelSignChanged;
            inputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            contourBox.DataSource = Enum.GetValues(typeof(ContourMethod));
            _frameWidth = 3;
            _frameHeight = 3;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            if (ofd.ShowDialog() != DialogResult.OK || ofd.FileName == null) return;

            var filePath = ofd.FileName;
            _imageFile = new FileInfo(filePath);
            searchBox.Text = filePath;
            Bitmap image = (Bitmap) UtilityExtensions.ReadImage(filePath);

            inputPictureBox.Image = image;
            outputPictureBox.Image = new Bitmap(image);

            ArraySegment<ARGB> originalPixels = _originalSource?.Pixels ?? ArraySegment<ARGB>.Empty;
            Span<ARGB> pixels = image.LockImage(out var sourceData).Cast<ARGB>();
            originalPixels = UtilityExtensions.PoolCopy(pixels, originalPixels);
            image.UnlockBits(sourceData);
            _originalSource = new ImageBuffer<ARGB>(originalPixels, image.Width, image.Height);
        }

        private void OnKernelSignChanged(object sender, EventArgs e)
        {
            if (sender is not RadioButton {Checked: true}) return;

            _positiveConv = sender == positiveRadio;
        }

        private void OnSaveAsSource(object sender, EventArgs e)
        {
            if (_grayScaleSource is not { } source || _temporary is not { } gray)
            {
                MessageBox.Show("Картинка не преобразована");
                return;
            }

            if (outputPictureBox.Image == null || outputPictureBox.Image == inputPictureBox.Image)
            {
                MessageBox.Show("Не найдены изменения!");
                return;
            }

            Bitmap image = (Bitmap) outputPictureBox.Image;
            Span<ARGB> data = image.LockImage(out var locked).Cast<ARGB>();
            data.CopyTo(_originalSource.Value.Pixels);
            image.UnlockBits(locked);

            inputPictureBox.Image = outputPictureBox.Image;
            outputPictureBox.Image = new Bitmap(inputPictureBox.Image);
            _grayScaleSource = new ImageBuffer<GrayScale>(gray, source.Width, source.Height);
            _temporary = ArraySegment<GrayScale>.Empty;
        }


        private void OnConvertToGrayscale(object sender, EventArgs e)
        {
            if (_originalSource is not { } source)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            var pixels = ArraySegment<GrayScale>.Empty;
            Algorithms.RGBToGrayscale(source.Pixels, ref pixels);
            _grayScaleSource = new ImageBuffer<GrayScale>(pixels, source.Width, source.Height);

            _temporary = pixels;
            SaveImageCopy(pixels, "fromRGBtoGrayScale");
        }

        private void OnCalculateContours(object sender, EventArgs e)
        {
            if (_grayScaleSource is not { } source)
            {
                MessageBox.Show("Картинка не бинаризована");
                return;
            }

            ArraySegment<GrayScale> pixels = UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<GrayScale>.Empty);
            ImageBuffer<GrayScale> tempResult = new(pixels, source.Width, source.Height);

            Frame f = new(0, 0, _frameWidth, _frameHeight);
            TimeSpan time;
            switch (_methodContur)
            {
                case ContourMethod.Roberts:
                {
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ContourMethods.Roberts(source, tempResult, _threshold, _multiplier, threads: 1);
                    stopWatch.Stop();
                    time = stopWatch.Elapsed;
                    break;
                }

                case ContourMethod.Sobel:
                {
                    var (coefGx, coefGy) = ContourMethods.GetGradient(_positiveConv);

                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ContourMethods.Sobel(source, tempResult, f, _threshold, _multiplier, coefGx, coefGy, threads: 1);
                    stopWatch.Stop();
                    time = stopWatch.Elapsed;
                    break;
                }
                case ContourMethod.Laplace:
                {
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    ContourMethods.Laplacian(source, tempResult, new Frame(0, 0, 3, 3),
                        _threshold, _multiplier, ContourMethods.GetLaplacian(_positiveConv), threads: 1);
                    stopWatch.Stop();
                    time = stopWatch.Elapsed;
                    break;
                }
                default:
                    MessageBox.Show("Вы не выбрали метод!");
                    return;
            }

            SaveImageCopy(pixels, _methodContur.ToString());

            MessageBox.Show($"{_methodContur} {time.Seconds:00}:{time.Milliseconds:000}");
        }

        private void OnApplyBinarization(object sender, EventArgs e)
        {
            if (_grayScaleSource is not { } source)
            {
                MessageBox.Show("Картинка не бинаризована");
                return;
            }

            ArraySegment<GrayScale> pixels =
                UtilityExtensions.Pool(source.Pixels.Count, _temporary ?? ArraySegment<GrayScale>.Empty);
            ImageBuffer<GrayScale> tempResult = new(pixels, source.Width, source.Height);
            Frame frame = new(0, 0, _frameWidth, _frameHeight);

            if (localThresholdRadio.Checked)
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                LocalThresholdMethod(source, tempResult, frame);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show($"Локальный {ts.Seconds:00}:{ts.Milliseconds:000}");
            }
            else
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                double threshold = GlobalThreshold(source);
                GlobalThresholdMethod(source, tempResult, threshold);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show("Рассчитано глобальное пороговое значение: " + threshold.ToString());
                MessageBox.Show($"Глобальный {ts.Seconds:00}:{ts.Milliseconds:000}");
            }

            _temporary = pixels;
            SaveImageCopy(pixels, "binarized");
        }

        #region Binarisation methods

        private static void GlobalThresholdMethod(ImageBuffer<GrayScale> sourceImage,
            ImageBuffer<GrayScale> resultImage, double threshold)
        {
            Span<GrayScale> source = sourceImage.Pixels;
            Span<GrayScale> pixels = resultImage.Pixels;
            for (int i = 0; i < pixels.Length; i++)
                pixels[i] = (GrayScale) (source[i].Value > threshold ? 255 : 0);
        }

        private static double GlobalThreshold(ImageBuffer<GrayScale> image)
        {
            double sum = 0;
            int size = image.Pixels.Count;
            Span<GrayScale> pixels = image.Pixels;

            for (int i = 0; i < size; i++)
                sum += pixels[i].Value;

            return sum / size;
        }

        private static void LocalThresholdMethod(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage,
            in Frame frame)
        {
            double size = frame.Square;

            ArraySegment<GrayScale> source = sourceImage.Pixels;
            Span<GrayScale> pixels = resultImage.Pixels;

            for (int y = 0; y < sourceImage.Height - 1; y++)
            for (int x = 0; x < sourceImage.Width - 1; x++)
            {
                double sum = 0;
                frame.X = x;
                frame.Y = y;
                (int yfrom, int yto) = frame.IterateY(x);
                for (int yk = yfrom; yk <= yto; yk++)
                {
                    (int xfrom, int xto) = frame.IterateX(yk);
                    for (int xk = xfrom; xk <= xto; xk++)
                    {
                        int y0 = Math.Clamp(yk, 0, sourceImage.Height - 1);
                        int x0 = Math.Clamp(xk, 0, sourceImage.Width - 1);

                        sum += source[x0 + y0 * sourceImage.Width].Value;
                    }

                    double localThreshold = sum / size;
                    int pid = x + y * sourceImage.Width;
                    pixels[pid] = (GrayScale) (source[pid].Value > localThreshold ? 255 : 0);
                }
            }
        }

        #endregion

        private void OnApplyMorphologicalFiltering(object sender, EventArgs e)
        {
            if (_grayScaleSource is not { } source)
            {
                MessageBox.Show("Картинка не бинаризована");
                return;
            }

            bool[,] mask = new bool[3, 3]
            {
                {false, true, false},
                {true, true, true},
                {false, true, false}
            };

            ArraySegment<GrayScale> pixels = UtilityExtensions.Pool(source.Pixels.Count, _temporary ?? ArraySegment<GrayScale>.Empty);
            ImageBuffer<GrayScale> tempResult = new(pixels, source.Width, source.Height);

            string filename = "";
            if (toboganningRadio.Checked) //классическое расширение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                ToboganningFilteringMethod(source, tempResult, mask);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show($"Toboganning {ts.Seconds:00}:{ts.Milliseconds:000}");
                filename = "toboganning";
            }
            else if (fastExpansionRadio.Checked) //ускоренное расширение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                SpeedyExpansionFilteringMethod(source, tempResult, mask);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show($"Fast expansion {ts.Seconds:00}:{ts.Milliseconds:000}");
                filename = "fast_expansion";
            }
            else if (shrinkingRadio.Checked) //классическое сужение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                ClassicalShrinkingFilteringMethod(source, tempResult, mask);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show($"Classical shrinking {ts.Seconds:00}:{ts.Milliseconds:000}");
                filename = "classic_shrinking";
            }
            else if (fastShrinkingRadio.Checked) //ускоренное сужение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                SpeedyShrinkingFilteringMethod(source, tempResult, mask);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                MessageBox.Show($"Fast shrinking {ts.Seconds:00}:{ts.Milliseconds:000}");
                filename = "fast_shrinking";
            }
            else
            {
                MessageBox.Show("Не выбран способ морфологической обработки!");
            }

            _temporary = pixels;
            if (filename != "")
                SaveImageCopy(pixels, filename);
        }

        #region Morphological filtering


        
        private void ToboganningFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result,
            bool[,] mask)
        {
            Span<GrayScale> sourcePixels = source.Pixels;
            List<MinPixelCoordinates> sectionPixels = new List<MinPixelCoordinates>();
            Span<GrayScale> resultPixels = result.Pixels;
            //int mWidth = mask.GetLength(0);
            //int mHeight = mask.GetLength(1);
            //int RW = (mWidth - 1) / 2;
            //int RH = (mHeight - 1) / 2;

            //var maxY = source.Height - RH - 1;
            //var maxX = source.Width - RW - 1;
            //
            //for (int y = RH; y < maxY; y++)
            //for (int x = RW; x < maxX; x++)
            //{
            //    double max = 0;
            //
            //    int pid = x + y * source.Width;
            //    for (int j = -RH; j <= RH; j++)
            //    {
            //        int y1 = Math.Clamp(y + j, 0, source.Height - 1);
            //        for (int i = -RW; i <= RW; i++)
            //        {
            //            int x1 = Math.Clamp(x + i, 0, source.Width - 1);
            //            int fid = x1 + y1 * source.Width;
            //
            //            if (sourcePixels[fid].Value >= max && mask[i + RW, j + RH])
            //                max = sourcePixels[fid].Value;
            //        }
            //    }
            //
            //    resultPixels[pid] = new GrayScale(max);
            //}

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    var resultMinPixel = FindMinPixelFromImage(source, x, y);
                    resultPixels[x + y * source.Width] = new GrayScale(resultMinPixel.Value);
                }
            }
        }

        private MinPixelCoordinates FindMinPixelFromImage(ImageBuffer<GrayScale> source, int x, int y)
        {
            if (source.Width - 1 < x || source.Height - 1 < y || x < 0 || y < 0)
                return new MinPixelCoordinates(x, y, 1000);

            double currentValue = source.Pixels[x + y * source.Width].Value;
            int currentX = x;
            int currentY = y;
            double[] localValues = new double[4] { 1000, 1000, 1000, 1000 };
            List<double> list;
            double minValue;

            while (true)
            {
                localValues = new double[4] { 1000, 1000, 1000, 1000 };

                if (currentX + 1 < source.Width - 1)
                    localValues[0] = source.Pixels[currentX + 1 + currentY * source.Width].Value;

                if (currentX - 1 > 0)
                    localValues[1] = source.Pixels[currentX - 1 + currentY * source.Width].Value;

                if (currentY + 1 < source.Height - 1)
                    localValues[2] = source.Pixels[currentX + (currentY + 1) * source.Width].Value;

                if (currentY - 1 > 0)
                    localValues[3] = source.Pixels[currentX + (currentY - 1) * source.Width].Value;

                list = localValues.ToList();
                minValue = list.Min();

                if (currentValue > minValue)
                {
                    switch (list.IndexOf(minValue))
                    {
                        case 0:
                            currentX++; break;
                        case 1:
                            currentX--; break;
                        case 2:
                            currentY++; break;
                        default:
                            currentY--; break;
                    }

                    currentValue = source.Pixels[currentX + currentY * source.Width].Value;
                }
                else
                    return new MinPixelCoordinates(x, y, currentValue);
            }
        }

        private void ClassicalShrinkingFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result,
            bool[,] mask)
        {
            Span<GrayScale> sourcePixels = source.Pixels;
            Span<GrayScale> resultPixels = result.Pixels;
            int mWidth = mask.GetLength(0);
            int mHeight = mask.GetLength(1);
            int RW = (mWidth - 1) / 2;
            int RH = (mHeight - 1) / 2;

            var maxY = source.Height - RH - 1;
            var maxX = source.Width - RW - 1;

            for (int y = RH; y < maxY; y++)
            for (int x = RW; x < maxX; x++)
            {
                int pid = x + y * source.Width;
                double min = sourcePixels[pid].Value;

                for (int j = -RH; j <= RH; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 1);
                    for (int i = -RW; i <= RW; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 1);
                        int fid = x1 + y1 * source.Width;

                        if (sourcePixels[fid].Value <= min && mask[i + RW, j + RH])
                            min = sourcePixels[fid].Value;
                    }
                }

                resultPixels[pid] = new GrayScale(min);
            }
        }

        private void SpeedyExpansionFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result,
            bool[,] mask)
        {
            Span<GrayScale> sourcePixels = source.Pixels;
            Span<GrayScale> resultPixels = result.Pixels;
            int mWidth = mask.GetLength(0);
            int mHeight = mask.GetLength(1);
            int RW = (mWidth - 1) / 2;
            int RH = (mHeight - 1) / 2;

            var maxY = source.Height - RH - 1;
            var maxX = source.Width - RW - 1;

            for (int y = RH; y < maxY; y++)
            for (int x = RW; x < maxX; x++)
            {
                int pid = x + y * source.Width;
                double max = 0;

                bool flag = false;
                for (int k = -2; k <= 2; k++)
                {
                    int y2 = Math.Clamp(y + k, 0, source.Height - 2);
                    int x2 = Math.Clamp(x + 2, 0, source.Width - 2);

                    if (Math.Abs(sourcePixels[x2 + y2 * source.Width].Value - 255.0) < 0.1)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    resultPixels[pid] = sourcePixels[pid];
                    continue;
                }

                for (int j = -RH; j <= RH; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 1);
                    for (int i = -RW; i <= RW; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 1);
                        int fid = x1 + y1 * source.Width;

                        if (sourcePixels[fid].Value >= max && mask[i + RW, j + RH])
                            max = sourcePixels[fid].Value;
                    }
                }

                resultPixels[pid] = (GrayScale) max;
            }
        }

        private void SpeedyShrinkingFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result,
            bool[,] mask)
        {
            Span<GrayScale> sourcePixels = source.Pixels;
            Span<GrayScale> resultPixels = result.Pixels;
            int mWidth = mask.GetLength(0);
            int mHeight = mask.GetLength(1);
            int RW = (mWidth - 1) / 2;
            int RH = (mHeight - 1) / 2;

            var maxY = source.Height - RH - 1;
            var maxX = source.Width - RW - 1;

            for (int y = RH; y < maxY; y++)
            for (int x = RW; x < maxX; x++)
            {
                int pid = x + y * source.Width;

                if (Math.Abs(sourcePixels[pid].Value - 255.0) < 0.1)
                {
                    resultPixels[pid] = sourcePixels[pid];
                    continue;
                }

                double min = source.Pixels[pid].Value;
                for (int j = -RH; j <= RH; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 1);
                    for (int i = -RW; i <= RW; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 1);
                        int fid = x1 + y1 * source.Width;

                        if (sourcePixels[fid].Value <= min && mask[i + RW, j + RH])
                            min = sourcePixels[fid].Value;
                    }
                }

                resultPixels[pid] = (GrayScale) min;
            }
        }

        #endregion

        private void OnApplySegmentation(object sender, EventArgs e)
        {
            if (_originalSource == null)
            {
                MessageBox.Show("Не выбрана картинка");
                return;
            }

            if (_grayScaleSource is not { } source)
            {
                MessageBox.Show("Картинка не бинаризована");
                return;
            }

            TimeSpan time;
            if (histogramSegmentationRadio.Checked) //разбиение
            {
                ArraySegment<ARGB> pixels = UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<ARGB>.Empty);
                ImageBuffer<ARGB> rgbResult = new(pixels, source.Width, source.Height);

                Stopwatch stopWatch = Stopwatch.StartNew();
                HistogramSegmentationMethod(source, rgbResult, _rand);
                stopWatch.Stop();

                time = stopWatch.Elapsed;

                SaveImageCopy(pixels, "segmentHist");
                MessageBox.Show($"Histogram segmentation: {time.Seconds:00}:{time.Milliseconds:000}");
            }
            else if (graphSegmentationRadio.Checked) //графы
            {
                ArraySegment<GrayScale> pixels =
                    UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<GrayScale>.Empty);
                ImageBuffer<GrayScale> grayResult = new(pixels, source.Width, source.Height);

                Stopwatch stopWatch = Stopwatch.StartNew();
                GraphSegmentationMethod(source, grayResult, _threshold);
                stopWatch.Stop();

                time = stopWatch.Elapsed;

                SaveImageCopy(pixels, "segmentGraph");
                MessageBox.Show($"Graph segmentation: {time.Seconds:00}:{time.Milliseconds:000}");
            }
            else
            {
                MessageBox.Show("Не выбран способ морфологической обработки!");
            }
        }

        private static void HistogramSegmentationMethod(ImageBuffer<GrayScale> source, ImageBuffer<ARGB> result,
            Random random)
        {
            int[] histY = new int[256];

            for (var i = 0; i < source.Pixels.Count; i++)
                histY[(int) source.Pixels[i].Value]++;

            // second derivative
            int[] histY2 = new int[256];
            for (int i = 1; i < 256 - 1; i++)
            {
                histY2[i] = (histY[i - 1] + histY[i] + histY[i + 1]) / 3;
            }

            List<int> maximumIds = new();
            List<int> minimumIds = new();
            int count = 0;
            //поиск локальных максимумов
            for (int i = 0; i < 256 - 1; i++)
            {
                if (histY2[i] > histY2[i + 1])
                {
                    count++;
                    if (count == 5) maximumIds.Add(i - count);
                }
                else
                {
                    count = 0;
                }
            }

            //поиск локальных минимумов
            for (int i = 0; i < maximumIds.Count - 1; i++)
            {
                int id = maximumIds[i];
                for (int j = maximumIds[i]; j < maximumIds[i + 1]; j++)
                    if (histY2[j] < histY2[id])
                        id = j;

                minimumIds.Add(id);
            }

            int[] histMas = new int[256];
            int k = 0;
            for (int i = 0; i < minimumIds.Count; i++)
            {
                for (int a = k; a < minimumIds[i]; a++)
                    histMas[a] = i;

                k = minimumIds[i];
            }

            for (int b = k; b < 256; b++)
                histMas[b] = minimumIds.Count;


            ARGB[] colors = new ARGB[minimumIds.Count + 1];
            Span<byte> argb = stackalloc byte[4] {0, 0, 0, 255};
            Span<byte> rgb = argb.Slice(0, 3);

            // TODO: test generation
            for (int i = 0; i < minimumIds.Count + 1; i++)
            {
                random.NextBytes(rgb);
                colors[i] = new ARGB(argb);
            }

            Span<ARGB> resultPixels = result.Pixels;
            for (int i = 0; i < source.Pixels.Count; i++)
            {
                // int l = histMas[buffer[i + 2]];
                int l = histMas[(int) source.Pixels[i].Value]; //???
                resultPixels[i] = colors[l];
            }
        }

        private static void GraphSegmentationMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result,
            double threshold)
        {
            byte[,] matrSv = new byte[source.Width - 1, source.Height - 1];
            for (int x = 0; x < source.Width - 2; x++)
            for (int y = 0; y < source.Height - 2; y++)
            {
                int id1 = x + y * source.Width;
                int id2 = x + 1 + (y + 1) * source.Width;

                double value = 255 - Math.Abs(source.Pixels[id1].Value - source.Pixels[id2].Value);
                matrSv[x, y] = (byte) (value < threshold ? 0 : value);
            }

            Span<GrayScale> resultPixels = result.Pixels;
            for (int x = 0; x < source.Width - 1; x++)
            for (int y = 0; y < source.Height - 1; y++)
            {
                int id = x + y * source.Width;
                resultPixels[id] = (GrayScale) matrSv[x, y];
            }
        }

        private void thresholdTrack_Scroll(object sender, EventArgs e) =>
            _threshold = thresholdTrack.Value;

        private void multiplierTrack_Scroll(object sender, EventArgs e) =>
            _multiplier = multiplierTrack.Value / 4.0;

        private void widthBox_ValueChanged(object sender, EventArgs e) =>
            _frameWidth = (int) widthBox.Value;

        private void heightBox_ValueChanged(object sender, EventArgs e) =>
            _frameHeight = (int) heightBox.Value;


        private Bitmap ShowResult(ArraySegment<ARGB> result)
        {
            Bitmap image = (Bitmap) outputPictureBox.Image;
            image.CopyFrom(result);
            UtilityExtensions.Reuse(result);
            outputPictureBox.Image = image;
            return image;
        }

        private Bitmap ShowResult(ArraySegment<GrayScale> result)
        {
            Bitmap image = (Bitmap) outputPictureBox.Image;
            Span<ARGB> pixels = image.LockImage(out var locked).Cast<ARGB>();
            for (int i = 0; i < pixels.Length; i++)
            {
                var value = (byte) result[i].Value;
                pixels[i].R = value;
                pixels[i].G = value;
                pixels[i].B = value;
            }

            image.UnlockBits(locked);
            outputPictureBox.Image = image;
            return image;
        }

        private void SaveImageCopy(ArraySegment<ARGB> imageBuffer, string fileName)
        {
            Bitmap resultBmp = ShowResult(imageBuffer);
            resultBmp.Save(_imageFile.DirectoryName + "/" + fileName + ".png", ImageFormat.Png);
        }

        private void SaveImageCopy(ArraySegment<GrayScale> imageBuffer, string fileName)
        {
            Bitmap resultBmp = ShowResult(imageBuffer);
            resultBmp.Save(_imageFile.DirectoryName + "/" + fileName + ".png", ImageFormat.Png);
        }

        private void contourBox_SelectedIndexChanged(object sender, EventArgs e) =>
            _methodContur = (ContourMethod) contourBox.SelectedItem;
    }
}