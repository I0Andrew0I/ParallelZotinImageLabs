using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using C1.Win.Chart;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

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
        private ArraySegment<byte>? _targetBuffer;
        private ArraySegment<byte>? _sourceBuffer;

        public Lab1Form()
        {
            InitializeComponent();
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
                Image img = Algorithms.ReadImage(_filePath);

                inputPictureBox.Image = img;

                if (_sourceBuffer is {Count: >0, Array: { }} memory)
                    ArrayPool<byte>.Shared.Return(memory.Array);

                _sourceBuffer = Algorithms.BmpToByte(new Bitmap(inputPictureBox.Image));
            }
        }

        private void buttonRH_Click(object sender, EventArgs e)
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


            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(_filePath);
            Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            Algorithms.ByteToBmp(resultBmp, targetMemory);

            resultBmp.Save(fileInfo.DirectoryName + "/fromRGBtoHLS.png", ImageFormat.Png);
            outputPictureBox.Image = resultBmp;

            _curModel = "HLS";
            radioButton1.Text = "H (тон)";
            radioButton2.Text = "L (яркость)";
            radioButton3.Text = "S (насыщенность)";
        }

        private void buttonHR_Click(object sender, EventArgs e)
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

            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(_filePath);
            Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);

            Algorithms.ByteToBmp(resultBmp, targetMemory);

            resultBmp.Save(fileInfo.DirectoryName + "/fromHLStoRGB.png", ImageFormat.Png);
            outputPictureBox.Image = resultBmp;

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

        FlexChart CreateChart((uint[] R, uint[] G, uint[] B) hist)
        {
            var chart = new FlexChart()
            {
                ChartType = C1.Chart.ChartType.LineSymbols,
                Dock = DockStyle.Fill
            };

            chart.Series.Add(new Series()
            {
                Name = "Red",
                Style = {StrokeColor = Color.Red},
                DataSource = hist.R
            });
            chart.Series.Add(new Series()
            {
                Name = "Blue",
                Style = {StrokeColor = Color.Blue},
                DataSource = hist.B
            });
            chart.Series.Add(new Series
            {
                Name = "Green",
                Style = {StrokeColor = Color.Green},
                DataSource = hist.B
            });

            return chart;
        }


        private void onImageVisualize(object sender, EventArgs e)
        {
            var source = (Bitmap) inputPictureBox.Image;
            var hist = Algorithms.Histogram(source);

            FlexChart chart = CreateChart(hist);
            var popup = new Form()
            {
                Width = 400,
                Height = 400,
            };
            popup.Controls.Add(chart);
            popup.Show(this);

            // if (radioButton1.Checked)
            // {
            //     for (int i = 0; i < 256; i++)
            //     {
            //         chart1.Series[0].Points.AddXY(i, hist[i].R);
            //     }
            //
            //     chart1.Series[0].LegendText = radioButton1.Text;
            //     chart1.Series[0].Color = Color.Red;
            // }
            // else if (radioButton2.Checked)
            // {
            //     for (int i = 0; i < 256; i++)
            //     {
            //         chart1.Series[0].Points.AddXY(i, hist[i].G);
            //     }
            //
            //     chart1.Series[0].LegendText = radioButton2.Text;
            //     chart1.Series[0].Color = Color.Green;
            // }
            // else if (radioButton3.Checked)
            // {
            //     for (int i = 0; i < 256; i++)
            //     {
            //         chart1.Series[0].Points.AddXY(i, hist[i].B);
            //     }
            //
            //     chart1.Series[0].LegendText = radioButton3.Text;
            //     chart1.Series[0].Color = Color.Blue;
            // }


            Bitmap result = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);

            //перерисовать картинку
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    //получить цвет пикселя
                    Color pix = source.GetPixel(i, j);
                    if (radioButton1.Checked)
                        result.SetPixel(i, j, Color.FromArgb(255, pix.R, 0, 0));
                    else if (radioButton2.Checked)
                        result.SetPixel(i, j, Color.FromArgb(255, 0, pix.G, 0));
                    else if (radioButton3.Checked)
                        result.SetPixel(i, j, Color.FromArgb(255, 0, 0, pix.B));
                }
            }

            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(_filePath);
            //Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            //FromByteToBmp(resultBmp, buffer);
            result.Save(fileInfo.DirectoryName + "/vizColor" + _fileNameCounter + ".png", ImageFormat.Png);
            outputPictureBox.Image = result;
            _fileNameCounter++;
        }

        private void onTransformImage(object sender, EventArgs e)
        {
            if (_sourceBuffer == null)
            {
                MessageBox.Show("Картинка не выбрана");
                return;
            }

            ArraySegment<byte> targetMemory = CopyFromSource(_sourceBuffer.Value);


            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Algorithms.TransformImage(targetMemory, _coefY, _coefC);
            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
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
            if (radioButton6.Checked)
            {
                Points getPoints = new Points();
                if (getPoints.ShowDialog() == DialogResult.OK)
                {
                    _correctionCurve = getPoints.Y;
                }
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ColorCorrection(_correctionCurve);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds / 10);
            MessageBox.Show("Яркость/контрастность " + elapsedTime);
            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(_filePath);
            Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            Algorithms.ByteToBmp(resultBmp, targetMemory);
            resultBmp.Save(fileInfo.DirectoryName + "/change" + _fileNameCounter + ".png", ImageFormat.Png);
            outputPictureBox.Image = resultBmp;
            _fileNameCounter++;
        }

        /// <code>
        /// y = (9+y)^2
        /// y = 5y + 8
        /// y = 3y
        /// y = sin(y)
        /// y = y^2
        /// </code>
        /// <param name="y"></param>
        /// <returns></returns>
        double EquationSystem(double y)
        {
            double Ynew = y switch
            {
                >= 0 and <= 50 => Math.Pow(9.0 + y, 2),
                > 50 and <= 100 => 5 * y + 8,
                > 100 and <= 150 => 3 * y,
                > 150 and <= 200 => Math.Sin(y),
                > 200 and <= 255 => Math.Pow(y, 2),
                _ => 0
            };

            return Ynew;
        }


        private void ColorCorrection(double[] curve)
        {
            if (_targetBuffer is not { } buffer) return;
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = 4};

            Parallel.For(0, buffer.Count / 4, po, i =>
            {
                int pix_i = i * 4;
                double new_y = 0;

                //перевод в YUV
                // нормализует значения красного, зеленого, синего
                double r = (double) buffer[pix_i + 2];
                double g = (double) buffer[pix_i + 1];
                double b = (double) buffer[pix_i];

                double y = 0.299 * r + 0.587 * g + 0.114 * b;
                double u = -0.14713 * r - 0.28886 * g + 0.436 * b;
                double v = 0.615 * r - 0.51499 * g - 0.10001 * b;

                // коррекция системой уравнений
                if (radioButton5.Checked)
                {
                    new_y = EquationSystem(y);
                }
                // коррекция по кривой
                else if (radioButton6.Checked)
                {
                    new_y = curve[(int) Math.Round(y)];
                }

                int rr = (int) Math.Round(new_y + 1.14 * v);
                if (rr > 255) rr = 255;
                else if (rr < 0) rr = 0;
                int gg = (int) (Math.Round(new_y - 0.395 * u - 0.581 * v));
                if (gg > 255) gg = 255;
                else if (gg < 0) gg = 0;
                int bb = (int) (Math.Round(new_y + 2.032 * u));
                if (bb > 255) bb = 255;
                else if (bb < 0) bb = 0;

                buffer[pix_i + 2] = (byte) rr;
                buffer[pix_i + 1] = (byte) gg;
                buffer[pix_i] = (byte) bb;
                buffer[pix_i + 3] = 255;
            });
        }

        private void SaveImageCopy(ArraySegment<byte> imageBuffer)
        {
            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(_filePath);
            Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            Algorithms.ByteToBmp(resultBmp, imageBuffer);
            resultBmp.Save(fileInfo.DirectoryName + "/change" + _fileNameCounter + ".png", ImageFormat.Png);
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
    }
}