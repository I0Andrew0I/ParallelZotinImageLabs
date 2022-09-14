using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CA1416

namespace Labs.Core
{
    public static class UtilityExtensions
    {
        public static ArraySegment<byte> CopyBytes(this Bitmap bitmap)
        {
            BitmapData sourceData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int size = sourceData.Stride * sourceData.Height;
            byte[] rent = ArrayPool<byte>.Shared.Rent(size);
            Marshal.Copy(sourceData.Scan0, rent, 0, size);
            bitmap.UnlockBits(sourceData);
            return new ArraySegment<byte>(rent, 0, size);
        }

        public static void CopyFrom(this Bitmap bitmap, ArraySegment<byte> data)
        {
            BitmapData resultData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(data.Array, 0, resultData.Scan0, data.Count);
            bitmap.UnlockBits(resultData);
        }

        public static Image ReadImage(string filePath)
        {
            using FileStream fs = new(filePath, FileMode.Open);
            Image img = Image.FromStream(fs);
            return img;
        }

        public static Span<byte> LockImage(this Bitmap bitmap, out BitmapData locked)
        {
            locked = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            int size = locked.Stride * locked.Height;
            Span<byte> span;
            unsafe
            {
                span = new Span<byte>((void*) locked.Scan0, size);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<TValue> Cast<TValue>(this Span<byte> source) where TValue : struct =>
            MemoryMarshal.Cast<byte, TValue>(source);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<TValue> Cast<TValue>(this ArraySegment<byte> source) where TValue : struct =>
            source.AsSpan().Cast<TValue>();

        public static TimeSpan CalculateTime(List<TimeSpan> tests)
        {
            var ticks = tests.Select(t => t.Ticks).ToArray();
            double mean = ticks.Sum() / (double) ticks.Length;
            double disp = ticks.Sum(t => Math.Abs(mean - t)) / ticks.Length;

            double l = mean - disp * 2;
            double r = mean + disp * 2;
            ticks = ticks.Where(t => t >= l && t <= r).ToArray();
            long meanTime = (long) Math.Round(ticks.Sum() / (double) ticks.Length);

            return new TimeSpan(meanTime);
        }
    }
}