using System;

namespace Labs.Core
{
    public class ImageBuffer<TPixel>
    {
        public ArraySegment<TPixel> Pixels { get; }
        public int Width { get; }
        public int Height { get; }

        public ImageBuffer(ArraySegment<TPixel> pixels, int width, int height)
        {
            Pixels = pixels;
            Width = width;
            Height = height;
        }
    }
}