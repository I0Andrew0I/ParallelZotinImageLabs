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

        private FileInfo _imageFile;

        public Lab3Form()
        {
            InitializeComponent();

            positiveRadio.CheckedChanged += OnKernelSignChanged;
            negativeRadio.CheckedChanged += OnKernelSignChanged;
            inputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            outputPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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
                    ContourMethods.Laplacian(source, tempResult, f, _threshold, _multiplier, ContourMethods.GetLaplacian(_positiveConv), threads: 1);
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

            ArraySegment<GrayScale> pixels = UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<GrayScale>.Empty);
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

            SaveImageCopy(pixels, "binarized");
        }

        #region Binarisation methods

        private static void GlobalThresholdMethod(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage, double threshold)
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

        private static void LocalThresholdMethod(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage, in Frame frame)
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

            ArraySegment<GrayScale> pixels = UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<GrayScale>.Empty);
            ImageBuffer<GrayScale> tempResult = new(pixels, source.Width, source.Height);

            string filename = "";
            if (expansionRadio.Checked) //классическое расширение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                ClassicExpansionFilteringMethod(source, tempResult);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = $"{ts.Seconds:00}:{ts.Milliseconds:000}";
                MessageBox.Show("Расш класс " + elapsedTime);
                filename = "fastR";
            }
            else if (fastExpansionRadio.Checked) //ускоренное расширение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                SpeedyExpansionFilteringMethod(source, tempResult);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = $"{ts.Seconds:00}:{ts.Milliseconds:000}";
                MessageBox.Show("Расш ускор " + elapsedTime);
                filename = "fastR";
            }
            else if (shrinkingRadio.Checked) //классическое сужение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                ClassicalShrinkingFilteringMethod(source, tempResult);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = $"{ts.Seconds:00}:{ts.Milliseconds:000}";
                MessageBox.Show("Суж класс " + elapsedTime);
                filename = "classicS";
            }
            else if (fastShrinkingRadio.Checked) //ускоренное сужение
            {
                Stopwatch stopWatch = Stopwatch.StartNew();
                SpeedyShrinkingFilteringMethod(source, tempResult);
                stopWatch.Stop();

                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = $"{ts.Seconds:00}:{ts.Milliseconds:000}";
                MessageBox.Show("Суж ускор " + elapsedTime);
                filename = "fastS";
            }
            else
            {
                MessageBox.Show("Не выбран способ морфологической обработки!");
            }

            if (filename != "")
                SaveImageCopy(pixels, filename);
        }

        #region Morphological filtering

        private void ClassicExpansionFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result)
        {
            Span<GrayScale> resultPixels = result.Pixels;

            for (int y = 2; y < source.Height - 2; y++)
            for (int x = 2; x < source.Width - 2; x++)
            {
                double max = 0;

                int pid = x + y * source.Width;
                for (int j = -2; j <= 2; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 2);
                    for (int i = -2; i <= 2; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 2);
                        int fid = x1 + y1 * source.Width;

                        if (source.Pixels[fid].Value >= max)
                            max = source.Pixels[fid].Value;
                    }
                }

                resultPixels[pid] = new GrayScale(max);
            }
        }

        private void ClassicalShrinkingFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result)
        {
            Span<GrayScale> resultPixels = result.Pixels;

            for (int y = 2; y < source.Height - 2; y++)
            for (int x = 2; x < source.Width - 2; x++)
            {
                double min = 255;
                int pid = x + y * source.Width;

                for (int j = -2; j <= 2; j++)
                {
                    for (int i = -2; i <= 2; i++)
                    {
                        int y1 = Math.Clamp(y + j, 0, source.Height - 2);
                        int x1 = Math.Clamp(x + i, 0, source.Width - 2);
                        int fid = x1 + y1 * source.Width;

                        if (source.Pixels[fid].Value <= min)
                            min = resultPixels[fid].Value;
                    }
                }

                resultPixels[pid] = new GrayScale(min);
            }
        }

        private void SpeedyExpansionFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result)
        {
            Span<GrayScale> resultPixels = result.Pixels;
            Span<GrayScale> sourcePixels = source.Pixels;

            for (int y = 2; y < source.Height - 2; y++)
            for (int x = 2; x < source.Width - 2; x++)
            {
                int pid = x + y * source.Width;
                double max = 0;

                bool flag = false;
                for (int k = -2; k <= 2; k++)
                {
                    int y2 = y + k;
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

                for (int j = -2; j <= 2; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 2);
                    for (int i = -2; i <= 2; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 2);

                        if (sourcePixels[x1 + y1 * source.Width].Value >= max)
                            max = sourcePixels[x1 + y1 * source.Width].Value;
                    }
                }

                resultPixels[pid] = (GrayScale) max;
            }
        }

        private void SpeedyShrinkingFilteringMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result)
        {
            Span<GrayScale> resultPixels = result.Pixels;
            Span<GrayScale> sourcePixels = source.Pixels;

            for (int y = 2; y < source.Height - 2; y++)
            for (int x = 2; x < source.Width - 2; x++)
            {
                int pid = x + y * source.Width;

                if (Math.Abs(sourcePixels[pid].Value - 255.0) < 0.1)
                {
                    resultPixels[pid] = sourcePixels[pid];
                    continue;
                }

                double min = 255;
                for (int j = -2; j <= 2; j++)
                {
                    int y1 = Math.Clamp(y + j, 0, source.Height - 2);
                    for (int i = -2; i <= 2; i++)
                    {
                        int x1 = Math.Clamp(x + i, 0, source.Width - 2);

                        if (sourcePixels[x1 + y1 * source.Width].Value <= min)
                            min = sourcePixels[x1 + y1 * source.Width].Value;
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
                ArraySegment<GrayScale> pixels = UtilityExtensions.Pool(source.Pixels.Count, ArraySegment<GrayScale>.Empty);
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

        private static void HistogramSegmentationMethod(ImageBuffer<GrayScale> source, ImageBuffer<ARGB> result, Random random)
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
            for (int i = 0; i < source.Pixels.Count; i += 4)
            {
                // int l = histMas[buffer[i + 2]];
                int l = histMas[(int) source.Pixels[i].Value]; //???
                resultPixels[i] = colors[l];
            }
        }

        private static void GraphSegmentationMethod(ImageBuffer<GrayScale> source, ImageBuffer<GrayScale> result, double threshold)
        {
            int[,] matrSv = new int[source.Width - 1, source.Height - 1];
            for (int x = 0; x < source.Width - 1; x++)
            for (int y = 0; y < source.Height - 1; y++)
            {
                int id1 = x + y * source.Width;
                int id2 = x + 1 + (y + 1) * source.Width;

                var value = (int) (255 - Math.Abs(source.Pixels[id1].Value - source.Pixels[id2].Value));
                matrSv[x, y] = value < threshold ? 0 : value;
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
    }
}