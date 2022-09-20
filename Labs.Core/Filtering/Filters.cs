using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public static class Filters
    {
        // minmax [круговой и прямоугольный], Median
        public static void Convolution<TPixel, TChannel>(ImageBuffer<TPixel> image, Frame size, ImageBuffer<TPixel> result, FrameReducer<TPixel, TChannel> reducer, bool round = false, int threads = 1)
            where TPixel : struct, IColor<TPixel, TChannel>
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            int imageWidth = image.Width;
            int imageHeight = image.Height;

            int mHeight = size.Height;
            int mWidth = size.Width;
            int frameSize = mHeight * mWidth;
            int step = (int) Math.Truncate(imageHeight / 16.0);

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;

                var frameBuffer = new TPixel[frameSize].AsSpan();
                var output = result.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;

                        CopyPixels(image, frame, frameBuffer);
                        TPixel pixel = reducer(frameBuffer, frame);
                        output[pixelId] = pixel;
                    }
                }
            });
        }

        public static void RecursiveKernelFiltering<TPixel, TChannel>(ImageBuffer<TPixel> image, double[,] kernel, PixelTransformer<TPixel, TChannel> transformer, ImageBuffer<TPixel> result, bool round = false, int threads = 1)
            where TPixel : struct, IColor<TPixel, TChannel>
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            int imageWidth = image.Width;
            int imageHeight = image.Height;
            int mWidth = kernel.GetLength(1);
            int mHeight = kernel.GetLength(0);
            int step = (int) Math.Truncate(imageHeight / 16.0);

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = result.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;

                        TPixel pixel = transformer(image, frame, kernel);
                        output[pixelId] = pixel;
                    }
                }
            });
        }

        // Linear, Mean, Laplacian
        public static void KernelFiltering<TPixel, TChannel>(ImageBuffer<TPixel> image, double[,] kernel, PixelTransformer<TPixel, TChannel> transformer, ImageBuffer<TPixel> result, bool round = false, int threads = 1)
            where TPixel : struct, IColor<TPixel, TChannel>
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = threads};
            int imageWidth = image.Width;
            int imageHeight = image.Height;
            int mWidth = kernel.GetLength(1);
            int mHeight = kernel.GetLength(0);
            int step = (int) Math.Truncate(imageHeight / 16.0);

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = result.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;

                        TPixel pixel = transformer(image, frame, kernel);
                        output[pixelId] = pixel;
                    }
                }
            });
        }

        private static void CopyPixels<TPixel>(in ImageBuffer<TPixel> image, in Frame frame, Span<TPixel> result) where TPixel : struct
        {
            int i = 0;
            var source = image.Pixels.AsSpan();
            foreach (int y0 in frame.IterateY(frame.X))
            {
                int y = Math.Clamp(y0, 0, image.Height - 1);

                foreach (int x0 in frame.IterateX(y))
                {
                    int x = Math.Clamp(x0, 0, image.Width - 1);

                    int pixelId = x + y * image.Width;
                    result[i] = source[pixelId];
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
            var matrix = new double[size.Height, size.Width];
            double value = 1.0 / size.Height / size.Width;

            for (int i = 0; i < size.Height; i++)
            for (int j = 0; j < size.Width; j++)
                matrix[i, j] = value;

            return matrix;
        }
    }
}