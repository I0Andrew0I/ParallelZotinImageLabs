using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Labs.Core.Scheme;

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

        public static void CopyFrom(this Bitmap bitmap, ArraySegment<ARGB> data)
        {
            Span<byte> image = bitmap.LockImage(out var locked);
            data.ToBytes().CopyTo(image);
            bitmap.UnlockBits(locked);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<byte> ToBytes(this ArraySegment<ARGB> source) =>
            MemoryMarshal.Cast<ARGB, byte>(source.AsSpan());

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

        public static ArraySegment<TValue> PoolCopy<TValue>(Span<TValue> source, ArraySegment<TValue> reuse)
        {
            if (source.Length > reuse.Count)
            {
                Reuse(reuse);
                return PoolCopy(source);
            }

            var result = reuse.Slice(0, source.Length);
            source.CopyTo(result);
            return result;
        }

        public static ArraySegment<TValue> PoolCopy<TValue>(Span<TValue> source)
        {
            var result = Pool<TValue>(source.Length);
            source.CopyTo(result);
            return result;
        }

        public static ArraySegment<TValue> Pool<TValue>(int length, ArraySegment<TValue> reuse)
        {
            if (length > reuse.Count) Reuse(reuse);

            return Pool<TValue>(length);
        }

        private static ArraySegment<TValue> Pool<TValue>(int length)
        {
            var rent = ArrayPool<TValue>.Shared.Rent(length);
            var result = new ArraySegment<TValue>(rent, 0, length);
            return result;
        }

        public static ArraySegment<TValue> Reuse<TValue>(ArraySegment<TValue> array)
        {
            if (array.Array != null)
                ArrayPool<TValue>.Shared.Return(array.Array!);

            array = ArraySegment<TValue>.Empty;
            return array;
        }
        
        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum input) where TEnum : Enum
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return (TEnum) value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Overflow(double n, double low, double up) =>
            n < low ? -low - n : n > up ? n - up : 0;
    }
}