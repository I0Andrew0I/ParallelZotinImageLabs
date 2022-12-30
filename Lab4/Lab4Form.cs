using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_1
{
    public partial class Lab4Form : Form
    {
        string filePath;
        string curModel = "RGB";
        byte[] input;
        static byte[] buffer;
        byte[] result;

        double[,] M2;
        double[,] U;
        double[,] R;
        double[,] E;

        int height;
        int width;

        public static int[][][,] LAWS_Map = new int[5][][,];

        struct HarrisPoint
        {
            public
            int x;
            public int y;
            public double val;
        }

        static BitmapData sourceData = new BitmapData();
        private static byte[] FromBmpToByte(Bitmap source)
        {
            sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int size = sourceData.Stride * sourceData.Height;
            byte[] bytes = new byte[size];
            Marshal.Copy(sourceData.Scan0, bytes, 0, size);
            source.UnlockBits(sourceData);
            return bytes;
        }

        private static void FromByteToBmp(Bitmap bmp, byte[] bytes)
        {
            BitmapData resultData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(bytes, 0, resultData.Scan0, bytes.Length);
            bmp.UnlockBits(resultData);
        }
        public Lab4Form()
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
                filePath = ofd.FileName;
                textBox1.Text = filePath;
                FileStream fs = new FileStream(filePath, System.IO.FileMode.Open);
                Image img = Image.FromStream(fs);
                fs.Close();
                inputPictureBox.Image = img;
                input = FromBmpToByte(new Bitmap(img));
                buffer = new byte[input.Length];
                buffer = input;
                result = new byte[input.Length];
                height = inputPictureBox.Image.Height;
                width = inputPictureBox.Image.Width;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (radioButton1.Checked)//HSV
            {
                for (int i = 0; i < buffer.Length; i += 4)
                {
                    double h = 0, s = 0, v = 0;

                    // нормализовать значения красного, зеленого, синего
                    double r = (double)buffer[i + 2] / 255.0;
                    double g = (double)buffer[i + 1] / 255.0;
                    double b = (double)buffer[i] / 255.0;

                    double max = Math.Max(r, Math.Max(g, b));
                    double min = Math.Min(r, Math.Min(g, b));

                    v = max;
                    if (max == 0) s = 0;
                    else s = (max - min) / max;
                    if (s == 0) //MessageBox.Show("Насыщенность S = 0!")
                                ;
                    else
                    {
                        //Цвет между желтым и пурпурным
                        if (r == max) h = (g - b) / (max - min);
                        //Цвет между голубым и желтым
                        else if (g == max) h = 2 + (b - r) / (max - min);
                        //Цвет между пурпурным и голубым
                        else if (b == max) b = 4 + (r - g) / (max - min);
                        h = h * 60;
                        if (h < 0) h = h + 360;
                    }
                    
                    buffer[i + 2] = (byte)Math.Round(h / 360.0 * 255.0);
                    buffer[i + 1] = (byte)Math.Round(s * 255.0);
                    buffer[i] = (byte)Math.Round(v * 255.0);
                    buffer[i + 3] = 255;
                }
                filename = "/HSV.png";
                curModel = "HSV";
            }
            else if (radioButton2.Checked)//YUV
            {
                for (int i = 0; i< buffer.Length; i+=4)
                {
                    // нормализует значения красного, зеленого, синего
                    double r = (double)buffer[i + 2];
                    double g = (double)buffer[i + 1];
                    double b = (double)buffer[i];

                    double y = 0.299 * r + 0.587 * g + 0.114 * b;
                    double u = -0.14713 * r - 0.28886 * g + 0.436 * b;
                    double v = 0.615 * r - 0.51499 * g - 0.10001 * b;

                    int rr = (int)(Math.Round(y + 1.14 * v));
                    if (rr > 255) rr = 255; else if (rr < 0) rr = 0;
                    int gg = (int)(Math.Round(y - 0.395 * u - 0.581 * v));
                    if (gg > 255) gg = 255; else if (gg < 0) gg = 0;
                    int bb = (int)(Math.Round(y + 2.032 * u));
                    if (bb > 255) bb = 255; else if (bb < 0) bb = 0;
                    buffer[i + 2] = (byte)rr;
                    buffer[i + 1] = (byte)gg;
                    buffer[i] = (byte)bb;
                    buffer[i + 3] = 255;
                }
                filename = "/YUV.png";
                curModel = "YUV";
            }
            else if (radioButton3.Checked) //HLS
            {
                for (int i = 0; i < buffer.Length; i += 4)
                {
                    double h = 0, s = 0, l = 0;

                    // нормализовать значения красного, зеленого, синего
                    double r = (double)buffer[i + 2] / 255.0;
                    double g = (double)buffer[i + 1] / 255.0;
                    double b = (double)buffer[i] / 255.0;

                    double max = Math.Max(r, Math.Max(g, b));
                    double min = Math.Min(r, Math.Min(g, b));

                    // тон
                    if (max == min)
                    {
                        h = 0; // неопределённый
                    }
                    else if (max == r && g >= b)
                    {
                        h = 60.0 * (g - b) / (max - min);
                    }
                    else if (max == r && g < b)
                    {
                        h = 60.0 * (g - b) / (max - min) + 360.0;
                    }
                    else if (max == g)
                    {
                        h = 60.0 * (b - r) / (max - min) + 120.0;
                    }
                    else if (max == b)
                    {
                        h = 60.0 * (r - g) / (max - min) + 240.0;
                    }

                    // яркость
                    l = (max + min) / 2.0;

                    // насыщенность
                    if (l == 0 || max == min)
                    {
                        s = 0;
                    }
                    else if (0 < l && l <= 0.5)
                    {
                        s = (max - min) / (max + min);
                    }
                    else if (l > 0.5)
                    {
                        s = (max - min) / (2 - (max + min)); //(max-min > 0)?
                    }
                    buffer[i + 2] = (byte)Math.Round(h / 360.0 * 255.0);
                    buffer[i + 1] = (byte)Math.Round(l * 255.0);
                    buffer[i] = (byte)Math.Round(s * 255.0);
                    buffer[i + 3] = 255;
                }
                filename = "/HLS.png";
                curModel = "HLS";
            }

            if (filename == "")
            {
                MessageBox.Show("Не выбрана цветовая модель!");
            }
            else
            {
                //сохраняем там же, где лежит исходный файл
                FileInfo fileInfo = new FileInfo(filePath);
                Bitmap resultBmp = new Bitmap(width, height);
                FromByteToBmp(resultBmp, buffer);
                resultBmp.Save(fileInfo.DirectoryName + filename, ImageFormat.Png);
                outputPictureBox.Image = resultBmp;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buffer = result;
            inputPictureBox.Image = outputPictureBox.Image;
            outputPictureBox.Image = null;
        }

        double[,] GetY()
        {
            //перевод в YUV
            double[,] Y = new double[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int xx = x * 4;
                    int yy = y * sourceData.Stride;
                    // нормализует значения красного, зеленого, синего
                    double r = (double)buffer[xx+yy + 2];
                    double g = (double)buffer[xx + yy + 1];
                    double b = (double)buffer[xx + yy];

                    double y1 = 0.299 * r + 0.587 * g + 0.114 * b;
                    Y[y, x] = y1;
                }
            }
            return Y;
        }

        double[] countHist(int x, int y, int rh, int rw, double[,] Y)
        {
            double[] hist = new double[256];
            for (int i = 0; i < 256; i++)
            {
                hist[i] = 0.0;
            }
            for (int dy = -rh; dy <= rh; dy++)
            {
                int ky = dy + y;
                if (ky < 0) ky = 0;
                if (ky > height - 1) ky = height - 1;
                for (int dx = -rw; dx <= rw; dx++)
                {
                    int kx = x + dx;
                    if (kx < 0) kx = 0;
                    if (kx > width - 1) kx = width - 1;
                    hist[(int)Y[ky, kx]]++;
                }
            }
            return hist;
        }

        void countTexFeatures(int x, int y, int rh, int rw, double[] hist)
        {
            int size = (rh * 2 + 1) * (rw * 2 + 1);
            double m = 0; double m2 = 0; double e = 0; double u = 0;
            for (int i = 0; i<256; i++)
            {
                hist[i] /= (double)size;
            }
            for (int i = 0; i < 256; i++)
            {
                m += hist[i]*i;
            }
            for (int i = 0; i<256; i++)
            {
                m2 += (Math.Pow((i - m), 2) * hist[i]);
                if (hist[i] != 0)
                {
                    e += (hist[i] * Math.Log(hist[i],2));
                }
                u += (Math.Pow(hist[i], 2));
            }

            M2[y,x] = m2;
            U[y,x] = u;
            R[y,x] = 1 - (1 / (1 + m2));
            E[y,x] = -1 * e;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            startHistogramStats((int)threads.Value);
		}

        void startHistogramStats(int threadsCount)
        {
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			double[,] Y = new double[height, width];
			Y = GetY();
			int rh = 2, rw = 2;
			M2 = new double[height, width];
			U = new double[height, width];
			R = new double[height, width];
			E = new double[height, width];

			//for (int y = 0; y < height; y++)
			ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = threadsCount };
			Parallel.For(0, height, po, y =>
			{
				for (int x = 0; x < width; x++)
				{
					double[] curHist = new double[256];
					for (int i = 0; i < 256; i++)
					{
						curHist[i] = 0.0;
					}
					curHist = countHist(x, y, rh, rw, Y);
					countTexFeatures(x, y, rh, rw, curHist);
				}
			});
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;

			PrintCurr(ts, "Гистограммные статистики");
			button7.Visible = true;
		}

        void countMinMax(double[,] arr, out double min, out double max)
        {
           min = arr[0, 0]; max = arr[0, 0];
           for (int i = 0; i< height; i++)
           {
                for (int j= 0; j< width; j++)
                {
                    if (arr[i,j] > max) max = arr[i,j];
                    if (arr[i,j] < min) min = arr[i,j];
                }
            }
        }

        //Карты Лавса
        public static void LUT(Bitmap bmp_orig, int threadsCound)
        {
            int rh = 2;
            int rw = 2;
            int size = (rh * 2 + 1) * (rw * 2 + 1);
            double Pn = 1 / size;
            int height = bmp_orig.Height;
            int width = bmp_orig.Width;
            int[] L5 = new int[5] { 1, 4, 6, 4, 1 };
            int[] E5 = new int[5] { -1, -2, 0, 2, 1 };
            int[] S5 = new int[5] { -1, 0, 2, 0, -1 };
            int[] W5 = new int[5] { -1, 2, 0, -2, 1 };
            int[] R5 = new int[5] { 1, -4, 0, 6, -4 };
            int[][] LV = new int[5][] { L5, E5, S5, W5, R5 };
            int[][,] TMPMap = new int[5][,];
            ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = threadsCound };
            Parallel.For(0, 5, po, i =>
            {
                TMPMap[i] = new int[width, height];
                for (int j = 0; j < height; j++)
                {
                    for (int l = 0; l < width; l++)
                    {
                        TMPMap[i][l, j] = 0;
                    }
                }
            });

            Parallel.For(0, 5, po, i =>
            {
                LAWS_Map[i] = new int[5][,];
                for (int j = 0; j < 5; j++)
                {
                    LAWS_Map[i][j] = new int[height, width];
                    for (int w = 0; w < width; w++)
                    {
                        for (int h = 0; h < height; h++)
                        {
                            LAWS_Map[i][j][h, w] = 0;
                        }
                    }
                }
            });
            unsafe
            {
                Parallel.For(0, height, po, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int im = 0; im < 5; im++)
                        {
                            int LFt = 0;
                            for (int dx = -rw; dx <= rw; dx++)
                            {
                                int kx = x + dx;
                                if (kx < 0) kx = 0;
                                if (kx >= width - 1) kx = width - 1;
                                int xx = kx * 4;
                                int yy = y * sourceData.Stride;
                                double r = (double)buffer[xx + yy + 2];
                                double g = (double)buffer[xx + yy + 1];
                                double b = (double)buffer[xx + yy];
                                double br = 0.299 * r + 0.587 * g + 0.144 * b;
                                LFt += (int)br * LV[im][dx + rw];
                            }
                            TMPMap[im][x, y] = LFt;
                        }
                    }
                });
                Parallel.For(0, height, po, x =>
                {
                    for (int y = 0; y < width; y++)
                    {
                        for (int im = 0; im < 5; im++)
                        {
                            for (int im2 = 0; im2 < 5; im2++)
                            {
                                int LF = 0;
                                for (int dx = -rw; dx <= rw; dx++)
                                {
                                    int kx = x + dx;
                                    if (kx < 0) kx = 0;
                                    if (kx > height - 1) kx = height - 1;
                                    double br = TMPMap[im][y, kx];
                                    LF += (int)br * LV[im2][dx + rw];
                                }
                                LAWS_Map[im][im2][x, y] = LF;
                            }
                        }
                    }
                });
            }
        }

        public Bitmap ShowImg(int x1, int y1)
        {
            int[,] data = LAWS_Map[x1][y1];
            Int32 height = inputPictureBox.Image.Height;
            Int32 width = inputPictureBox.Image.Width;

            unsafe
            {
                Bitmap bitmap = new Bitmap(width, height);
                //BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                int min = LAWS_Map[x1][y1][0, 0];
                int max = LAWS_Map[x1][y1][0, 0];

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (LAWS_Map[x1][y1][i, j] > max) max = LAWS_Map[x1][y1][i, j];
                        if (LAWS_Map[x1][y1][i, j] < min) min = LAWS_Map[x1][y1][i, j];
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    //byte* row = (byte*)bitmapData.Scan0 + bitmapData.Stride * y;
                    for (int x = 0; x < width; x++)
                    {
                        double check = LAWS_Map[x1][y1][y, x];
                        double check2 = check - min;
                        double check3 = max - min;
                        double check4 = check2 / check3;
                        double check5 = check4 * 255;
                        byte grayShade8bit = (byte)check5;

                        int xx = x * 4;
                        int yy = y * sourceData.Stride;
                        result[xx+yy] = grayShade8bit;
                        result[xx + yy + 1] = grayShade8bit;
                        result[xx + yy + 2] = grayShade8bit;
                        result[xx + yy + 3] = 255;
                    }
                }

                FileInfo fileInfo = new FileInfo(filePath);
                FromByteToBmp(bitmap, result);
                bitmap.Save(fileInfo.DirectoryName + "/Laws.png", ImageFormat.Png);
                //bitmap.UnlockBits(bitmapData);
                return bitmap;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp_orig = new Bitmap(inputPictureBox.Image);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            LUT(bmp_orig, (int)threads.Value);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
			PrintCurr(ts, "Карты Лавласа");
			button8.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double min = 0, max = 0;
            string filename = "";
            double res = 0;
            int xx = 0;
            int yy = 0;

            if (radioButton4.Checked)
            {
                countMinMax(M2, out min, out max);
                filename = "/M2.png";
            }
            else if (radioButton5.Checked)
            {
                countMinMax(R, out min, out max);
                filename = "/R.png";
            }
            else if (radioButton6.Checked)
            {
                countMinMax(U, out min, out max);
                filename = "/U.png";
            }
            else if (radioButton7.Checked)
            {
                countMinMax(E, out min, out max);
                filename = "/E.png";
            }
            else
            {
                MessageBox.Show("Вы не выбрали, какой признак отобразить!");
                return;
            }

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    xx = x * 4;
                    yy = y * sourceData.Stride;
                    if (radioButton4.Checked)
                    {
                        res = ((M2[y,x]-min) / (max - min)) *255;
                    }
                    else if (radioButton5.Checked)
                    {
                        res = ((R[y,x] - min) / (max - min)) * 255;
                    }
                    else if (radioButton6.Checked)
                    {
                        res = ((U[y,x] - min) / (max - min)) * 255;
                    }
                    else if (radioButton7.Checked)
                    {
                        res = ((E[y,x] - min) / (max - min)) * 255;
                    }
                    else
                    {
                        MessageBox.Show("Вы не выбрали, какой признак отобразить!");
                    }

                    result[xx + yy + 2] = (byte)res;
                    result[xx + yy + 1] = (byte)res;
                    result[xx + yy] = (byte)res;
                    result[xx + yy + 3] = 255;
                }
            }

            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(filePath);
            Bitmap resultBmp = new Bitmap(width, height);
            FromByteToBmp(resultBmp, result);
            resultBmp.Save(fileInfo.DirectoryName + filename, ImageFormat.Png);
            outputPictureBox.Image = resultBmp;
        }
        double LinearFilter(int rh, int rw, int y, int x, double[,] Y, double[,] linearCoefs)
        {
            double res = 0;
            for (int dy = -rh; dy <= rh; dy++)
            {
                int ky = dy + y;
                if (ky < 0) ky = 0;
                if (ky > height - 1) ky = height - 1;
                for (int dx = -rw; dx <= rw; dx++)
                {
                    int kx = x + dx;
                    if (kx < 0) kx = 0;
                    if (kx > width - 1) kx = width - 1;
                    res += Y[ky, kx] * linearCoefs[dy + rh, dx + rw];
                }
            }
            if (res < 0) res = 0;
            if (res > 255) res = 255;
            return res;
        }

        HarrisPoint[] getBCircle (int x0, int y0, double[,]Y)
        {
            HarrisPoint[] arr = new HarrisPoint[16];
            HarrisPoint curHP = new HarrisPoint();

            curHP.x = x0; curHP.y = y0 - 3;
            arr[0] = curHP;

            curHP.x = x0 + 1; curHP.y = y0 - 3;
            arr[1] = curHP;

            curHP.x = x0 + 2; curHP.y = y0 - 2; 
            arr[2] = curHP;

            curHP.x = x0 + 3; curHP.y = y0 - 1;
            arr[3] = curHP;

            curHP.x = x0 + 3; curHP.y = y0;
            arr[4] = curHP;

            curHP.x = x0 + 3; curHP.y = y0 + 1;
            arr[5] = curHP;

            curHP.x = x0 + 2; curHP.y = y0 + 2;
            arr[6] = curHP;

            curHP.x = x0 + 1; curHP.y = y0 + 3;
            arr[7] = curHP;

            curHP.x = x0; curHP.y = y0 + 3;
            arr[8] = curHP;

            curHP.x = x0 - 1; curHP.y = y0 + 3;
            arr[9] = curHP;

            curHP.x = x0 - 2; curHP.y = y0 + 2;
            arr[10] = curHP;

            curHP.x = x0 - 3; curHP.y = y0 + 1;
            arr[11] = curHP;

            curHP.x = x0 - 3; curHP.y = y0;
            arr[12] = curHP;

            curHP.x = x0 - 3; curHP.y = y0 + 1;
            arr[13] = curHP;

            curHP.x = x0 - 2; curHP.y = y0 + 2;
            arr[14] = curHP;

            curHP.x = x0 - 1; curHP.y = y0 + 3;
            arr[15] = curHP;

            for (int i = 0; i < 16; i++)
            {
                if (arr[i].x < 0 || arr[i].y < 0 || arr[i].x > width - 1 || arr[i].y > height - 1)
                    arr[i].val = -1;
                else
                    arr[i].val = Math.Abs(Y[arr[i].y, arr[i].x] - Y[y0, x0]);
            }

            return arr;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string filename = "";
            int porog = int.Parse(textBox2.Text); 
            int rad = int.Parse(textBox4.Text);
            double[,] Y = new double[height, width];
            Y = GetY();
            ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = (int)threads.Value };
            if (radioButton8.Checked)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                double[,] newY = new double[height, width];
                //коэффициенты для линейного фильтра
                double[,] coefsLF = new double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (i == 1 && j == 1)
                            coefsLF[i, j] = 0.2;
                        else
                            coefsLF[i, j] = 0.1;
                    }
                }
                //сглаживание линейным фильтром
                //for (int y = 0; y < height; y++)
                
                Parallel.For(0, height, po, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        newY[y, x] = LinearFilter(1, 1, y, x, Y, coefsLF);
                    }
                });

                double[,] diffX = new double[height, width];
                double[,] diffY = new double[height, width];
                double[,] diffXX = new double[height, width];
                double[,] diffYY = new double[height, width];
                double[,] diffXY = new double[height, width];
                int RH = 1, RW = 1;

                ////коэффициенты для градиента по горизонтали
                double[,] coefsH = new double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    coefsH[i, 0] = 1;
                }
                for (int i = 0; i < 3; i++)
                {
                    coefsH[i, 1] = 0;
                }
                for (int i = 0; i < 3; i++)
                {
                    coefsH[i, 2] = -1;
                }

                //коэффициенты для градиента по вертикали
                double[,] coefsV = new double[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    coefsV[0, i] = 1;
                }
                for (int i = 0; i < 3; i++)
                {
                    coefsV[1, i] = 0;
                }
                for (int i = 0; i < 3; i++)
                {
                    coefsV[2, i] = -1;
                }


                //for (int y = 0; y < height; y++)
                Parallel.For(0, height, po, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        double linF_ValueX = 0;
                        double linF_ValueY = 0;
                        for (int dy = -RH; dy <= RH; dy++)
                        {
                            int ky = y + dy;
                            if (ky < 0) ky = 0;
                            if (ky > height - 1) ky = height - 1;
                            for (int dx = -RW; dx <= RW; dx++)
                            {
                                int kx = x + dx;
                                if (kx < 0) kx = 0;
                                if (kx > width - 1) kx = width - 1;
                                linF_ValueX += newY[ky, kx] * coefsH[dy + RH, dx + RW];
                                linF_ValueY += newY[ky, kx] * coefsV[dy + RH, dx + RW];
                            }
                        }
                        linF_ValueX = linF_ValueX < 0 ? 0 : linF_ValueX;
                        linF_ValueX = linF_ValueX > 255 ? 255 : linF_ValueX;
                        linF_ValueY = linF_ValueY < 0 ? 0 : linF_ValueY;
                        linF_ValueY = linF_ValueY > 255 ? 255 : linF_ValueY;
                        diffX[y, x] = linF_ValueX;
                        diffY[y, x] = linF_ValueY;
                    }
                });

                Parallel.For(0, height, po, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        double linF_ValueX = 0;
                        double linF_ValueY = 0;
                        double linF_ValueXY = 0;
                        for (int dy = -RH; dy <= RH; dy++)
                        {
                            int ky = y + dy;
                            if (ky < 0) ky = 0;
                            if (ky > height - 1) ky = height - 1;
                            for (int dx = -RW; dx <= RW; dx++)
                            {
                                int kx = x + dx;
                                if (kx < 0) kx = 0;
                                if (kx > width - 1) kx = width - 1;
                                linF_ValueX += diffX[ky, kx] * coefsH[dy + RH, dx + RW];
                                linF_ValueY += diffY[ky, kx] * coefsV[dy + RH, dx + RW];
                                linF_ValueXY += diffX[ky, kx] * coefsV[dy + RH, dx + RW];
                            }
                        }
                        linF_ValueX = linF_ValueX < 0 ? 0 : linF_ValueX;
                        linF_ValueX = linF_ValueX > 255 ? 255 : linF_ValueX;
                        linF_ValueY = linF_ValueY < 0 ? 0 : linF_ValueY;
                        linF_ValueY = linF_ValueY > 255 ? 255 : linF_ValueY;
                        linF_ValueXY = linF_ValueXY < 0 ? 0 : linF_ValueXY;
                        linF_ValueXY = linF_ValueXY > 255 ? 255 : linF_ValueXY;
                        diffXX[y, x] = linF_ValueX;
                        diffYY[y, x] = linF_ValueY;
                        diffXY[y, x] = linF_ValueXY;
                    }
                });

                //вычисление значения функции отклика угла R
                double[,] H = new double[height, width];
                //for (int y = 0; y < height; y++)
                Parallel.For(0, height, po, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        double A = diffX[y, x];
                        double B = diffY[y, x];
                        double C = diffXY[y, x];
                        double res = (A * B - C * C) - (0.04 * ((A + B) * (A + B)));
                        if (res > porog) //порог вводит пользователь
                            H[y, x] = res;
                        else
                            H[y, x] = 0;
                    }
                });

                //поиск особых точек для визуализации
                List<HarrisPoint> HarrisPoints = new List<HarrisPoint>();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (H[y, x] > 0 && H[y,x] > porog)
                        {
                            HarrisPoint curHP = new HarrisPoint();
                            curHP.x = x;
                            curHP.y = y;
                            curHP.val = H[y, x];
                            HarrisPoints.Add(curHP);
                        }
                    }
                }

                //сортировка по убыванию
                for (int i = 0; i < HarrisPoints.Count - 1; i++)
                {
                    if (HarrisPoints[i].val < HarrisPoints[i + 1].val)
                    {
                        HarrisPoint tmp = new HarrisPoint();
                        tmp = HarrisPoints[i];
                        HarrisPoints[i] = HarrisPoints[i + 1];
                        HarrisPoints[i + 1] = tmp;
                    }
                }

                //отсечение по порогу и удаление точек. радиус задаёт пользователь, >=20
                int s = HarrisPoints.Count;
                List<HarrisPoint> arr= new List<HarrisPoint>();
                for (int i = 0; i < HarrisPoints.Count; i++)
                {
                    arr.Add(HarrisPoints[i]);
                }
                HarrisPoints.Clear();
                int a = 0;
                bool flag;
                do
                {
                    flag = true;
                    if (a > 0)
                    {
                        for (int j = 0; j < HarrisPoints.Count; j++)
                        {
                            if (Math.Sqrt(Math.Pow(arr[a].x - HarrisPoints[j].x, 2) + Math.Pow(arr[a].y -
                            HarrisPoints[j].y, 2)) < rad)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        HarrisPoints.Add(arr[a]);
                    }
                    a++;
                } while (a < s);

                //визуализация
                int rh = 1; int rw = 1;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        for (int dy = -rh; dy <= rh; dy++)
                        {
                            int ky = dy + y;
                            if (ky < 0) ky = 0;
                            if (ky > height - 1) ky = height - 1;
                            for (int dx = -rw; dx <= rw; dx++)
                            {
                                int kx = x + dx;
                                if (kx < 0) kx = 0;
                                if (kx > width - 1) kx = width - 1;
                                HarrisPoint curHP = new HarrisPoint();
                                curHP.x = kx;
                                curHP.y = ky;
                                curHP.val = H[ky, kx];
                                int xx = kx * 4;
                                int yy = ky * sourceData.Stride;
                                if (HarrisPoints.Contains(curHP))
                                {
                                    result[xx + yy + 2] = 255;
                                    result[xx + yy + 1] = 0;
                                    result[xx + yy] = 0;
                                    result[xx + yy + 3] = 255;
                                }
                                else
                                {
                                    result[xx + yy + 2] = buffer[xx + yy + 2];
                                    result[xx + yy + 1] = buffer[xx + yy + 1];
                                    result[xx + yy] = buffer[xx + yy];
                                    result[xx + yy + 3] = 255;
                                }
                            }
                        }
                    }
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                PrintCurr(ts, "Харрис");
                filename = "/Harris.png";
            }
            else if(radioButton9.Checked)
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                int w = inputPictureBox.Image.Width;
                int h = inputPictureBox.Image.Height;

                double[,] F = new double[h, w];

                //сглаживание линейным фильтром
                Parallel.For(0, h, po, y =>
                {
                    for (int x = 0; x < w; x++)
                    {
                        F[y, x] = 0;
                    }
                });

                Parallel.For(0, h, po, y =>
                {
                    for (int x = 0; x < w; x++)
                    {
                        HarrisPoint[] circle = new HarrisPoint[16];
                        circle = getBCircle(x, y, Y);
                        double curPorog = porog + Y[y, x];
                        int count = 0;
                        if (circle[0].val >= 0 && (circle[0].val > curPorog || circle[0].val < curPorog))
                            count++;
                        if (circle[4].val >= 0 && (circle[4].val > curPorog || circle[4].val < curPorog))
                            count++;
                        if (circle[8].val >= 0 && (circle[8].val > curPorog || circle[8].val < curPorog))
                            count++;
                        if (circle[12].val >= 0 && (circle[12].val > curPorog || circle[12].val < curPorog))
                            count++;

                        if (count >= 3)
                        {
                            count = 0;
                            for (int k = 0; k < 16; k++)
                            {
                                if (circle[k].val > curPorog)
                                    count++;
                            }
                            F[y, x] = count;
                        }
                    }
                });

                //отсечение по порогу
                //поиск особых точек для визуализации
                List<HarrisPoint> points = new List<HarrisPoint>();

                //Parallel.For(0, h, po, y =>
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        if (F[y, x] != 0 && F[y, x] > porog)
                        {
                            HarrisPoint curHP = new HarrisPoint();
                            curHP.x = x;
                            curHP.y = y;
                            curHP.val = F[y, x];
                            points.Add(curHP);
                        }
                    }
                }
                //);

                //сортировка по убыванию
                for (int i = 0; i < points.Count - 1; i++)
                {
                    if (points[i].val < points[i + 1].val)
                    {
                        HarrisPoint tmp = new HarrisPoint();
                        tmp = points[i];
                        points[i] = points[i + 1];
                        points[i + 1] = tmp;
                    }
                }

                //отсечение по порогу и удаление точек. радиус задаёт пользователь, >=20
                int s = points.Count;
                List<HarrisPoint> arr = new List<HarrisPoint>();
                for (int i = 0; i < points.Count; i++)
                {
                    arr.Add(points[i]);
                }
                points.Clear();
                int a = 0;
                bool flag;
                do
                {
                    flag = true;
                    if (a > 0)
                    {
                        for (int j = 0; j < points.Count; j++)
                        {
                            if (Math.Sqrt(Math.Pow(arr[a].x - points[j].x, 2) + Math.Pow(arr[a].y -
                            points[j].y, 2)) < rad)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                    if (flag)
                    {
                        points.Add(arr[a]);
                    }
                    a++;
                } while (a < s);

                //визуализация
                int rh = 1;
                int rw = 1;
                Parallel.For(0, h, po, y =>
                {
                    for (int x = 0; x < w; x++)
                    {
                        for (int dy = -rh; dy <= rh; dy++)
                        {
                            int ky = dy + y;
                            if (ky < 0) ky = 0;
                            if (ky > h - 1) ky = h - 1;
                            for (int dx = -rw; dx <= rw; dx++)
                            {
                                int kx = x + dx;
                                if (kx < 0) kx = 0;
                                if (kx > w - 1) kx = w - 1;
                                HarrisPoint curHP = new HarrisPoint();
                                curHP.x = kx;
                                curHP.y = ky;
                                curHP.val = F[ky, kx];
                                int xx = kx * 4;
                                int yy = ky * sourceData.Stride;
                                if (points.Contains(curHP))
                                {
                                    result[xx + yy + 2] = 255;
                                    result[xx + yy + 1] = 0;
                                    result[xx + yy] = 0;
                                    result[xx + yy + 3] = 255;
                                }
                                else
                                {
                                    result[xx + yy + 2] = buffer[xx + yy + 2];
                                    result[xx + yy + 1] = buffer[xx + yy + 1];
                                    result[xx + yy] = buffer[xx + yy];
                                    result[xx + yy + 3] = 255;
                                }
                            }
                        }
                    }
                });
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                
                PrintCurr(ts, "FAST");
                filename = "/FAST.png";
            }
            else
            {
                MessageBox.Show("Вы не выбрали метод!");
            }

            if (filename != "")
            {
                //сохраняем там же, где лежит исходный файл
                FileInfo fileInfo = new FileInfo(filePath);
                Bitmap resultBmp = new Bitmap(width, height);
                FromByteToBmp(resultBmp, result);
                resultBmp.Save(fileInfo.DirectoryName + filename, ImageFormat.Png);
                outputPictureBox.Image = resultBmp;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int count1 = Convert.ToInt32(textBox3.Text);
            int count2 = Convert.ToInt32(textBox5.Text);
            outputPictureBox.Image = ShowImg(count1, count2);
        }

        public static double convRad(int x)
        {
            return (x * Math.PI) / 180;
        }
        struct PointsH
        {
            public int R { get; set; }
            public int tet { get; set; }
            public double count { get; set; }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bmp_orig = new Bitmap(inputPictureBox.Image);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Bitmap bmp = new Bitmap(inputPictureBox.Image);
            int col_grad = 181;
            //Порог
            int TS = Convert.ToInt32(textBox6.Text);
            //максимальная длина перпендикуляра (радиус)
            int r = (int)Math.Sqrt((inputPictureBox.Height * inputPictureBox.Height) + (inputPictureBox.Width * inputPictureBox.Width));
            //фазовое пространство
            double[,] FP = new double[col_grad, r];
            //Картинка в чб
            Bitmap bmp_grey = new Bitmap(inputPictureBox.Image);
            int par_col = 2;
            ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = (int)threads.Value };
            //Заполняем нулями фазовое пространство
            Parallel.For(0, col_grad, po, i =>
            //for (int i = 0; i < col_grad; i++)
            {
                for (int j = 0; j < r; j++)
                {
                    FP[i, j] = 0;
                }
            });
            //массив градусов
            int[] grad = new int[col_grad];
            Parallel.For(0, col_grad, po, i =>
            //for (int i = 0; i < col_grad; i++)
            {
                grad[i] = i;
            });


            //углы в радианах
            double[] thetas = new double[grad.Length];
            Parallel.For(0, thetas.Length, po, i =>
            //for (int i = 0; i < thetas.Length; i++)
            {
                thetas[i] = convRad(grad[i]);
            });
            //Высота и ширина новой картинки фазового пространства
            int height = col_grad;
            int width = r;
            //Углы косинусов и синусов
            double[] cos_t = new double[thetas.Length];
            double[] sin_t = new double[thetas.Length];
            int num_thetas = thetas.Length;
            Parallel.For(0, thetas.Length, po, i =>
            //for (int i = 0; i < thetas.Length; i++)
            {
                cos_t[i] = Math.Cos(thetas[i]);
                sin_t[i] = Math.Sin(thetas[i]);
            });
            //Число белых точек
            int white_count = 0;

            //Заполнение массива фазового пространства
            //for (int i = 0; i < bmp_grey.Height; i++)
            int h = bmp_grey.Height;
            int w = bmp_grey.Width;
            int len = thetas.Length;
            Parallel.For(0, h, po, i =>
            {
                for (int j = 0; j < w; j++)
                {
                    int xx = j * 4;
                    int yy = (int)i * sourceData.Stride;
                    double red = (double)buffer[xx + yy + 2];
                    double g = (double)buffer[xx + yy + 1];
                    double b = (double)buffer[xx + yy];
                    if (red > 254 && g > 254 && b > 254)
                    {
                        white_count++;
                        for (int k = 0; k < len; k++)
                        {
                            for (int R = 0; R < r; R++)
                            {
                                if ((Math.Abs(i * cos_t[k] + j * sin_t[k] - R)) < TS)
                                {
                                    FP[k, R]++;
                                }
                            }
                        }
                    }
                }
            });

            //поиск максимума и минимума
            double min = FP[0, 0];
            double max = FP[0, 0];
            Parallel.For(0, col_grad, po, q =>
            //for (int q = 0; q < col_grad; q++)
            {
                for (int R = 0; R < r; R++)
                {
                    if (FP[q, R] > max) max = FP[q, R];
                    if (FP[q, R] < min) min = FP[q, R];
                }
            });

            double check3 = max - min;

            //отображение фазового пространства
            unsafe
            {
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);
                Parallel.For(0, col_grad, po, q =>
                {
                    byte* row = (byte*)bitmapData.Scan0 + bitmapData.Stride * q;
                    for (int R = 0; R < r; R++)
                    {

                        double check = FP[q, R];
                        double check2 = check - min;
                        double check4 = check2 / check3;
                        double check5 = check4 * 255;
                        if (check5 > 255) check5 = 255;
                        row[(R * 4) + 0] = (byte)check5;
                        row[R * 4 + 1] = (byte)check5;
                        row[R * 4 + 2] = (byte)check5;
                    }
                });
                bitmap.UnlockBits(bitmapData);
                //pictureBox2.Image = bitmap;
            }
            
            List<PointsH> numbers = new List<PointsH>();
            for (int q = 0; q < col_grad; q++)
            {
                for (int R = 0; R < r; R++)
                {
                    if (FP[q, R] != 0)
                        numbers.Add(new PointsH() { R = R, tet = q, count = FP[q, R] });
                }
            }

            var sorted = from u in numbers
                         orderby u.count descending
                         select u;

            int count = sorted.Count();
            
            count = 0;
            //Добавляем в новый (сортированный) список 10 точек
            List<PointsH> numbers_sort = new List<PointsH>();
            foreach (PointsH u in sorted)
            {
                count++;
                if (count > 20) break;
                else
                {
                    numbers_sort.Add(u);
                }
            }
            
            Bitmap bitmap_3 = new Bitmap(inputPictureBox.Image);
            Bitmap bitmap_4 = new Bitmap(inputPictureBox.Image);
            byte[] input_im;
            byte[] buffer_im;
            input_im = FromBmpToByte(bitmap_4);
            buffer_im = new byte[input_im.Length];
            buffer_im = input_im;


            //проверяем элементы сортированного списка
            h = bitmap_4.Height;
            w = bitmap_4.Width;
            int sort_count = numbers_sort.Count;
            //for (int y = 0; y < bitmap_4.Height; y++)
            Parallel.For(0, h, po, y =>
            {
                for (int x = 0; x < w; x++)
                {
                    for (int i = 0; i < sort_count; i++)
                    {
                        int xx = x * 4;
                        int yy = y * sourceData.Stride;

                        int Teta = numbers_sort[i].tet;
                        int R = numbers_sort[i].R;
                        int check = (int)Math.Round(y * cos_t[Teta] + x * sin_t[Teta]);
                        if (check == R)
                        {
                            buffer_im[xx + yy + 2] = 255;
                            buffer_im[xx + yy + 2] = 255;
                            buffer_im[xx + yy + 1] = 0;
                            buffer_im[xx + yy] = 0;
                        }
                    }
                }
            });
            Bitmap resultBmp = new Bitmap(inputPictureBox.Image.Width, inputPictureBox.Image.Height);
            FromByteToBmp(resultBmp, buffer_im);
            outputPictureBox.Image = resultBmp;
            FileInfo fileInfo = new FileInfo(filePath);
            resultBmp.Save(fileInfo.DirectoryName + "/Haf.png", ImageFormat.Png);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            
            PrintCurr(ts, "Хаф");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            double p = 10; double c = 50;
            ParallelOptions po = new ParallelOptions { MaxDegreeOfParallelism = 1 };
            Parallel.For(0, height - 1, po, y =>
            {
                for (int x = 0; x < width - 1; x++)
                {

                    int x0 = x * 4;
                    int y0 = y * sourceData.Stride;

                    int x1 = x0 + 4;
                    int y1 = y0 + sourceData.Stride;

                    int Gx = buffer[y0 + x0] - buffer[y1 + x1];
                    int Gy = buffer[y0 + x1] - buffer[y1 + x0];

                    double res = Math.Sqrt(Gx + Gy);
                    if (res < p) res = 0;
                    else
                    {
                        res *= c;
                        if (res > 255) res = 255;
                    }

                    result[y0 + x0 + 2] = (byte)res;
                    result[y0 + x0 + 1] = (byte)res;
                    result[y0 + x0] = (byte)res;
                    result[y0 + x0 + 3] = 255;
                }
            });
            //сохраняем там же, где лежит исходный файл
            FileInfo fileInfo = new FileInfo(filePath);
            Bitmap resultBmp = new Bitmap(width, height);
            FromByteToBmp(resultBmp, result);
            resultBmp.Save(fileInfo.DirectoryName + "/conturRoberts.png", ImageFormat.Png);
            outputPictureBox.Image = resultBmp;
        }

        private void Analyze_Load(object sender, EventArgs e)
        {

        }

        void PrintCurr(TimeSpan totalTime, string methodName)
        {
            textBox7.AppendText($"======\r\n" +
                $"Method: {methodName}\r\n" +
                $"Threads: {(int)threads.Value}\r\n" +
                $"Size: {width}x{height}\r\n" +
                $"Time (seconds){totalTime.TotalSeconds}\r\n");
		}

		private void button10_Click(object sender, EventArgs e)
		{
            int threadsCount = (int)threads.Value;
            for(int i = 1; i <= threadsCount; i++)
            {
                startHistogramStats(i);

			}
		}
	}                         
}                             