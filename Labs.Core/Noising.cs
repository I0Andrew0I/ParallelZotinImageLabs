﻿using System;
using Labs.Core.Scheme;

namespace Labs.Core
{
    public static class Noising
    {
        public static void ImpulseNoise(Span<ARGB> result, ARGB.Channel channel, double noiseRatio, double whiteRatio, double blackRatio)
        {
            int amountTotal = (int) (noiseRatio * result.Length);
            int amountWhite = (int) (whiteRatio * amountTotal);
            int amountBlack = (int) (blackRatio * amountTotal);

            Random rnd = Random.Shared;
            ARGB whitePixel = SetComponents(channel, 255);
            ARGB blackPixel = SetComponents(channel, 0);


            for (int i = 0; i < amountTotal; i++)
            {
                int id = rnd.Next(0, result.Length);
                if (amountWhite > 0)
                {
                    amountWhite--;
                    whitePixel.Extract(channel, ref result[id]);
                }
                else if (amountBlack > 0)
                {
                    amountBlack--;
                    blackPixel.Extract(channel, ref result[id]);
                }
            }
        }

        public static void MultiplicativeNoise(Span<ARGB> result, ARGB.Channel channel, double noiseRatio, byte left, byte right)
        {
            int amountTotal = (int) (noiseRatio * result.Length);
            Random rnd = Random.Shared;

            for (int i = 0; i < amountTotal; i++)
            {
                int id = rnd.Next(0, result.Length);

                double noise = left + Math.Abs(right - left) * rnd.NextDouble();

                ARGB randomPixel = default;
                if (channel.HasFlag(ARGB.Channel.Red))
                    randomPixel.R = (byte) Math.Clamp(noise * result[id].R, 0, 255);
                if (channel.HasFlag(ARGB.Channel.Green))
                    randomPixel.G = (byte) Math.Clamp(noise * result[id].G, 0, 255);
                if (channel.HasFlag(ARGB.Channel.Blue))
                    randomPixel.B = (byte) Math.Clamp(noise * result[id].B, 0, 255);

                randomPixel.Extract(channel, ref result[id]);
            }
        }

        public static void AdditiveNoise(Span<ARGB> result, ARGB.Channel channel, double noiseRatio, byte left, byte right)
        {
            int amountTotal = (int) (noiseRatio * result.Length);
            Random rnd = Random.Shared;
            int amplitude = Math.Abs(right - left);

            for (int i = 0; i < amountTotal; i++)
            {
                int id = rnd.Next(0, result.Length);

                double noise = left + amplitude * rnd.NextDouble();

                ARGB randomPixel = default;
                if (channel.HasFlag(ARGB.Channel.Red))
                    randomPixel.R = (byte) Math.Clamp(noise + result[id].R, 0, 255);
                if (channel.HasFlag(ARGB.Channel.Green))
                    randomPixel.G = (byte) Math.Clamp(noise + result[id].G, 0, 255);
                if (channel.HasFlag(ARGB.Channel.Blue))
                    randomPixel.B = (byte) Math.Clamp(noise + result[id].B, 0, 255);

                randomPixel.Extract(channel, ref result[id]);
            }
        }

        private static ARGB SetComponents(ARGB.Channel channel, byte value)
        {
            ARGB pixel = default;
            if (channel.HasFlag(ARGB.Channel.Red))
                pixel.R = value;
            if (channel.HasFlag(ARGB.Channel.Green))
                pixel.G = value;
            if (channel.HasFlag(ARGB.Channel.Blue))
                pixel.B = value;
            return pixel;
        }
    }
}