using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Segmentation
{
    public static class ContourMethods
    {
        public static void Roberts(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage, double threshold, double multiplier, int threads)
        {
            int imageWidth = sourceImage.Width - 1;
            int imageHeight = sourceImage.Height - 1;

            int step = (int) Math.Truncate(imageHeight / 16.0);

            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                Span<GrayScale> sourcePixels = sourceImage.Pixels;
                Span<GrayScale> resultPixels = resultImage.Pixels;

                for (int y = from; y < to; y++)
                {
                    for (int x0 = 0; x0 < imageWidth; x0++)
                    {
                        {
                            int x1 = x0 + 1;
                            int y0 = y * imageWidth;
                            int y1 = (y + 1) * imageWidth;

                            double Gx = sourcePixels[x0 + y0].Value - sourcePixels[x1 + y1].Value;
                            double Gy = sourcePixels[x1 + y0].Value - sourcePixels[x0 + y1].Value;

                            double res = Math.Abs(Gx) + Math.Abs(Gy);
                            res = res >= threshold ? res * multiplier : 0;

                            resultPixels[x0 + y0] = (GrayScale) res;
                        }
                    }
                }
            });
        }

        public static void Sobel(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage, Frame f, double threshold, double multiplier, int[] coefGx, int[] coefGy, int threads)
        {
            int imageWidth = sourceImage.Width;
            int imageHeight = sourceImage.Height;

            int step = (int) Math.Truncate(imageHeight / 16.0);

            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                Span<GrayScale> sourcePixels = sourceImage.Pixels;
                Span<GrayScale> resultPixels = resultImage.Pixels;

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        double Gx = 0;
                        double Gy = 0;

                        f.X = x;
                        f.Y = y;
                        int picid = x + y * imageWidth;
                        (int yfrom, int yto) = f.IterateY(f.X);

                        for (int y0 = yfrom; y0 <= yto; y0++)
                        {
                            int mY = y0 - yfrom;
                            (int xfrom, int xto) = f.IterateX(y0);

                            for (int x0 = xfrom; x0 <= xto; x0++)
                            {
                                int mX = x0 - xfrom;
                                int fy = Math.Clamp(y0, 0, sourceImage.Height - 1);
                                int fx = Math.Clamp(x0, 0, imageWidth - 1);
                                int frameId = fx + fy * imageWidth;

                                Gx += sourcePixels[frameId].Value * coefGx[mX + mY];
                                Gy += sourcePixels[frameId].Value * coefGy[mX + mY];
                            }
                        }

                        double res = Math.Abs(Gx) + Math.Abs(Gy);
                        res = res >= threshold ? res * multiplier : 0;

                        resultPixels[picid] = (GrayScale) res;
                    }
                }
            });
        }


        public static void Laplacian(ImageBuffer<GrayScale> sourceImage, ImageBuffer<GrayScale> resultImage, Frame f, double threshold, double multiplier, double[,] laplacian, int threads)
        {
            int imageWidth = sourceImage.Width;
            int imageHeight = sourceImage.Height;

            int step = (int) Math.Truncate(imageHeight / 16.0);

            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                Span<GrayScale> sourcePixels = sourceImage.Pixels;
                Span<GrayScale> resultPixels = resultImage.Pixels;

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        double res = 0;
                        f.X = x;
                        f.Y = y;
                        int picid = x + y * imageWidth;
                        (int yfrom, int yto) = f.IterateY(f.X);

                        for (int y0 = yfrom; y0 <= yto; y0++)
                        {
                            int mY = y0 - yfrom;
                            (int xfrom, int xto) = f.IterateX(y0);

                            for (int x0 = xfrom; x0 <= xto; x0++)
                            {
                                int mX = x0 - xfrom;

                                int fy = Math.Clamp(y0, 0, imageHeight - 1);
                                int fx = Math.Clamp(x0, 0, imageWidth - 1);
                                int frameId = fx + fy * imageWidth;

                                res += sourcePixels[frameId].Value * laplacian[mY, mX];
                            }
                        }

                        res = res >= threshold ? res * multiplier : 0;

                        resultPixels[picid] = (GrayScale) res;
                    }
                }
            });
        }

        public static double[,] GetLaplacian(bool positive)
        {
            double[,] coefs = new double[3, 3];
            int K = positive ? 1 : -1;

            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (i == 1 && j == 1)
                    coefs[i, j] = 8 * K;
                else
                    coefs[i, j] = -1 * K;

            return coefs;
        }

        public static (int[] coefGx, int[] coefGy) GetGradient(bool positive)
        {
            int[] coefGx;
            int[] coefGy;
            if (positive)
            {
                coefGx = new[]
                {
                    1, 0, -1,
                    2, 0, -2,
                    1, 0, -1
                };
                coefGy = new[]
                {
                    1, 2, 1,
                    0, 0, 0,
                    -1, -2, -1
                };
            }
            else
            {
                coefGx = new[]
                {
                    2, 1, 0,
                    1, 0, -1,
                    0, -1, -2
                };
                coefGy = new[]
                {
                    0, 1, 2,
                    -1, 0, 1,
                    -2, -1, 0
                };
            }

            return (coefGx, coefGy);
        }
    }
}