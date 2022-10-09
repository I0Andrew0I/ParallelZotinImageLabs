using System;

namespace Labs.Core
{
    public readonly record struct ImageBuffer<TPixel>
    {
        public ArraySegment<TPixel> Pixels { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }

        public ImageBuffer(ArraySegment<TPixel> pixels, int width, int height)
        {
            Pixels = pixels;
            Width = width;
            Height = height;
        }
    }
}