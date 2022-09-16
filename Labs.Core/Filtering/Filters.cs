using System;
using System.Threading.Tasks;

namespace Labs.Core.Filtering
{
    public static class Filters
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="RH">frame height</param>
        /// <param name="RW">frame width</param>
        /// <param name="py">image y</param>
        /// <param name="px">image x</param>
        /// <param name="imageHeight"></param>
        /// <param name="imageWidth"></param>
        /// <param name="buffer">image buffer</param>
        /// <param name="matrix">filtration matrix</param>
        /// <param name="channel">output channel</param>
        /// <returns></returns>
        [Obsolete]
        public static double LinearFilterFrame(int RH, int RW, int py, int px, int imageHeight, int imageWidth, byte[] buffer, double[,] matrix, string channel)
        {
            double res = 0;
            for (int dy = -RH; dy <= RH; dy++)
            {
                int y = dy + py;
                if (y < 0) y = 0;
                if (y > imageHeight - 1) y = imageHeight - 1;
                for (int dx = -RW; dx <= RW; dx++)
                {
                    int x = px + dx;
                    if (x < 0) x = 0;
                    if (x > imageWidth - 1) x = imageWidth - 1;

                    switch (channel)
                    {
                        case "r":
                            res += buffer[y + x + 2] * matrix[dy + RH, dx + RW]; //R
                            break;
                        case "g":
                            res += buffer[y + x + 1] * matrix[dy + RH, dx + RW]; //G
                            break;
                        case "b":
                            res += buffer[y + x] * matrix[dy + RH, dx + RW]; //B
                            break;
                        case "all":
                            //???
                            break;
                    }
                }
            }

            if (res < 0) res = 0;
            if (res > 255) res = 255;
            return res;
        }

        [Obsolete]
        public static byte[] LinearFiltering(int frameSize, byte[] imageBuffer, string channel, int imageWidth, int imageHeight, double[,] matrix)
        {
            int rh, rw;
            rw = (frameSize - 1) / 2;
            rh = (frameSize - 1) / 2;
            var result = new byte[imageBuffer.Length];

            //for (int y = 0; y < height; y++)
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = 1};
            Parallel.For(0, imageHeight, po, y =>
            {
                for (int x = 0; x < imageWidth; x++)
                {
                    double linFValue = LinearFilterFrame(rh, rw, (int) y, x, imageHeight, imageWidth, imageBuffer, matrix, channel);
                    int xx = x * 4;
                    int yy = y * imageWidth * 4;
                    switch (channel)
                    {
                        case "r":
                            result[xx + yy + 2] = (byte) linFValue; //R
                            result[xx + yy + 1] = imageBuffer[xx + yy + 1];
                            result[xx + yy + 0] = imageBuffer[xx + yy + 0];
                            result[xx + yy + 3] = imageBuffer[xx + yy + 3];
                            break;
                        case "g":
                            result[xx + yy + 1] = (byte) linFValue; //G
                            result[xx + yy + 2] = imageBuffer[xx + yy + 2];
                            result[xx + yy + 0] = imageBuffer[xx + yy + 0];
                            result[xx + yy + 3] = imageBuffer[xx + yy + 3];
                            break;
                        case "b":
                            result[xx + yy] = (byte) linFValue; //B
                            result[xx + yy + 1] = imageBuffer[xx + yy + 1];
                            result[xx + yy + 2] = imageBuffer[xx + yy + 2];
                            result[xx + yy + 3] = imageBuffer[xx + yy + 3];
                            break;
                        case "all":
                            //???
                            break;
                    }
                }
            });
            return result;
        }


        // minmax, Median 
        public static void Convolution<TPixel>(ImageBuffer<TPixel> image, Frame size, ImageBuffer<TPixel> result, FrameReducer<TPixel> reducer, int threads = 1) where TPixel : struct
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            Parallel.For(0, image.Height, po, (int y) =>
            {
                TPixel[] pixels = new TPixel[size.Heigth * size.Width];
                for (int x = 0; x < image.Width; x++)
                {
                    var frame = new Frame(x, y, size.Width, size.Heigth);
                    CopyPixels(image, frame, pixels);
                    result.Pixels[x + y * image.Width] = reducer(pixels, frame);
                }
            });
        }

        // Linear, Mean, Laplacian
        public static void KernelFiltering<TPixel>(ImageBuffer<TPixel> image, double[,] kernel, PixelTransformer<TPixel> transformer, ImageBuffer<TPixel> result, int threads = 1) where TPixel : struct
        {
            int mWidth = kernel.GetLength(1);
            int mHeight = kernel.GetLength(0);

            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            Parallel.For(0, image.Height, po, (int y) =>
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var frame = new Frame(x, y, mWidth, mHeight);
                    result.Pixels[x + y * image.Width] = ApplyKernel(image.Pixels, image.Height, image.Width, frame, kernel, transformer);
                }
            });
        }

        private static TPixel ApplyKernel<TPixel>(ArraySegment<TPixel> image, int imageHeight, int imageWidth, Frame f, double[,] kernel, PixelTransformer<TPixel> transformer) where TPixel : struct
        {
            TPixel result = default;

            for (int dy = -f.Heigth; dy <= f.Heigth; dy++)
            {
                int y = dy + f.Y;
                if (y < 0) y = 0;
                if (y > imageHeight - 1) y = imageHeight - 1;

                for (int dx = -f.Width; dx <= f.Width; dx++)
                {
                    int x = f.X + dx;
                    if (x < 0) x = 0;
                    if (x > imageWidth - 1) x = imageWidth - 1;

                    int pixelId = y * imageHeight + x;
                    int matrixY = dy + f.Heigth;
                    int matrixX = dx + f.Width;

                    result = transformer(image[pixelId], kernel[matrixY, matrixX], result);
                }
            }

            return result;
        }

        private static void CopyPixels<TPixel>(ImageBuffer<TPixel> image, Frame frame, TPixel[] result) where TPixel : struct
        {
            int i = 0;
            for (int dy = -frame.Heigth; dy <= frame.Heigth; dy++)
            {
                int y = dy + frame.Y;
                if (y < 0) y = 0;
                if (y > image.Height - 1) y = image.Height - 1;

                for (int dx = -frame.Width; dx <= frame.Width; dx++)
                {
                    int x = frame.X + dx;
                    if (x < 0) x = 0;
                    if (x > image.Width - 1) x = image.Width - 1;

                    int pixelId = y * image.Height + x;
                    result[i] = image.Pixels[pixelId];
                    i++;
                }
            }
        }

        public static double[,] CalculateLaplacian()
        {
            var matrix = new double[3, 3];
            for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (i == 1 && j == 1)
                    matrix[i, j] = 8;
                else
                    matrix[i, j] = -1;

            return matrix;
        }

        public static double[,] CalculateMean(Frame size)
        {
            var matrix = new double[size.Heigth, size.Width];
            double value = 1.0 / size.Heigth * size.Width;
            
            for (int i = 0; i < size.Heigth; i++)
            for (int j = 0; j < size.Width; j++)
                matrix[i, j] = value;

            return matrix;
        }
        
    }
}