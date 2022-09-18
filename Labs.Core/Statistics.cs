using System;
using System.Linq;
using ImageMagick;
using Labs.Core.Scheme;

namespace Labs.Core
{
    public static class Statistics
    {
        public static double MSAD(ArraySegment<ARGB> source, ArraySegment<ARGB> result)
        {
            double res = 0;
            for (int i = 0; i < source.Count; i++)
            {
                int r = Math.Abs(source[i].R - result[i].R);
                int g = Math.Abs(source[i].G - result[i].G);
                int b = Math.Abs(source[i].B - result[i].B);
                res += (r + g + b) / 3.0;
            }

            return res / source.Count;
        }

        public static double PSNR(ArraySegment<ARGB> source, ImageBuffer<ARGB> result)
        {
            MagickImage image1 = new(source.ToBytes(), new MagickReadSettings()
                {Format = MagickFormat.Bgra, Height = result.Height, Width = result.Width});
            MagickImage image2 = new(result.Pixels.ToBytes(), new MagickReadSettings()
                {Format = MagickFormat.Bgra, Height = result.Height, Width = result.Width});

            return image1.Compare(image2, ErrorMetric.PeakSignalToNoiseRatio);
        }

        public static double SSIM(ArraySegment<ARGB> source, ArraySegment<ARGB> result)
        {
            double[] ySource = new double[source.Count];
            double[] yResult = new double[source.Count];
            YUV value = default;
            for (int i = 0; i < source.Count; i++)
            {
                source[i].ToYUV(ref value);
                ySource[i] = value.Y;

                result[i].ToYUV(ref value);
                yResult[i] = value.Y;
            }

            double meanSource = ySource.Sum() / ySource.Length;
            double meanResult = yResult.Sum() / yResult.Length;

            double dispSource = 0, dispResult = 0;

            for (int i = 0; i < yResult.Length; i++)
            {
                dispSource += Math.Pow(ySource[i] - meanSource, 2);
                dispResult += Math.Pow(yResult[i] - meanResult, 2);
            }

            dispResult /= (yResult.Length - 1);
            dispSource /= (yResult.Length - 1);

            double covxy = 0;
            for (int i = 0; i < yResult.Length; i++)
            {
                covxy += ((ySource[i] - meanSource) * (yResult[i] - meanResult));
            }

            covxy /= (yResult.Length - 1);

            double c1 = 6.5025;
            double c2 = 58.5225;

            double d1 = (2 * meanResult * meanSource + c1) * (2 * covxy + c2);
            double d2 = (dispResult + dispSource + c2) * (Math.Pow(meanResult, 2) + Math.Pow(meanSource, 2) + c1);

            return d1 / d2;
        }
    }
}