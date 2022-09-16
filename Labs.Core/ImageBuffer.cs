namespace Labs.Core
{
    public class ImageBuffer<TPixel>
    {
        public TPixel[] Pixels { get; }
        public int Width { get; }
        public int Height { get; }

        public ImageBuffer(TPixel[] pixels, int width, int height)
        {
            Pixels = pixels;
            Width = width;
            Height = height;
        }
    }
}