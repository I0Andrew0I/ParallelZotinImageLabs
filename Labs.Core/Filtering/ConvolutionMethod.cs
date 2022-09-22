using System;
using System.Threading.Tasks;
using Labs.Core.Scheme;

namespace Labs.Core.Filtering
{
    public abstract record ConvolutionMethod<TPixel, TChannel>(in ImageBuffer<TPixel> Image, TChannel Channels)
        where TPixel : struct, IColor<TPixel, TChannel>
    {
        protected ArraySegment<TPixel> Pixels = Image.Pixels;

        public virtual void Apply(Frame frameShape, ImageBuffer<TPixel> resultImage, int numThreads)
        {
            ParallelOptions po = new ParallelOptions {MaxDegreeOfParallelism = numThreads};
            int imageWidth = Image.Width;
            int imageHeight = Image.Height;
            int mWidth = frameShape.Width;
            int mHeight = frameShape.Height;
            int step = (int) Math.Truncate(imageHeight / 16.0);
            bool round = frameShape is EllipsoidsFrame;

            Parallel.For(0, 16, po, (int iter) =>
            {
                int from = iter * step;
                int to = iter == 15 ? imageHeight : (iter + 1) * step;
                var output = resultImage.Pixels.AsSpan();
                Frame frame = round ? new EllipsoidsFrame(0, 0, mWidth, mHeight) : new Frame(0, 0, mWidth, mHeight);

                for (int y = from; y < to; y++)
                {
                    for (int x = 0; x < imageWidth; x++)
                    {
                        int pixelId = x + y * imageWidth;
                        frame.X = x;
                        frame.Y = y;

                        TPixel pixel = SlideFrame(frame, ref output, pixelId);
                        pixel.Extract(Channels, ref output[pixelId]);
                    }
                }
            });
        }

        protected abstract TPixel SlideFrame(in Frame f, ref Span<TPixel> output, int pixelId);
    }
}