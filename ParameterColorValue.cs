﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
#if !NET35
using System.Runtime.CompilerServices;
#endif

namespace ConnectorLib.JSON
{
    ///<remarks>This is a simplified version of System.Drawing.Color with no external dependencies.</remarks>
    [DebuggerDisplay("{NameAndARGBValue}")]
#if NETSTANDARD1_3_OR_GREATER
    [Serializable]
#endif
#if !NET35
    [TypeForwardedFrom("System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
#endif
    public readonly struct ParameterColorValue : IEquatable<ParameterColorValue>
    {
        public static readonly ParameterColorValue Empty = new();

        // NOTE : The "zero" pattern (all members being 0) must represent
        //      : "not set". This allows "Color c;" to be correct.

        //private const short StateKnownColorValid = 0x0001;
        private const short StateARGBValueValid = 0x0002;
        private const short StateValueMask = StateARGBValueValid;
        private const short StateNameValid = 0x0008;
        private const long NotDefinedValue = 0;

        // Shift counts and bit masks for A, R, G, B components in ARGB mode

        internal const int ARGBAlphaShift = 24;
        internal const int ARGBRedShift = 16;
        internal const int ARGBGreenShift = 8;
        internal const int ARGBBlueShift = 0;
        internal const uint ARGBAlphaMask = 0xFFu << ARGBAlphaShift;
        internal const uint ARGBRedMask = 0xFFu << ARGBRedShift;
        internal const uint ARGBGreenMask = 0xFFu << ARGBGreenShift;
        internal const uint ARGBBlueMask = 0xFFu << ARGBBlueShift;

        // Standard 32bit sRGB (ARGB)
        private readonly long value; // Do not rename (binary serialization)

        // State flags.
        private readonly short state; // Do not rename (binary serialization)

        private ParameterColorValue(long value, short state)
        {
            this.value = value;
            this.state = state;
        }

        public byte R => unchecked((byte)(Value >> ARGBRedShift));

        public byte G => unchecked((byte)(Value >> ARGBGreenShift));

        public byte B => unchecked((byte)(Value >> ARGBBlueShift));

        public byte A => unchecked((byte)(Value >> ARGBAlphaShift));

        public bool IsEmpty => state == 0;

        // Used for the [DebuggerDisplay]. Inlining in the attribute is possible, but
        // against best practices as the current project language parses the string with
        // language specific heuristics.

        private long Value
        {
            get
            {
                if ((state & StateValueMask) != 0)
                {
                    return value;
                }

                return NotDefinedValue;
            }
        }

        private static ParameterColorValue FromArgb(uint argb) => new(argb, StateARGBValueValid);

        public static ParameterColorValue FromArgb(int argb) => FromArgb(unchecked((uint)argb));

        public static ParameterColorValue FromArgb(byte alpha, byte red, byte green, byte blue)
        {
            return FromArgb(
                (uint)alpha << ARGBAlphaShift |
                (uint)red << ARGBRedShift |
                (uint)green << ARGBGreenShift |
                (uint)blue << ARGBBlueShift
            );
        }

        public static ParameterColorValue FromArgb(int alpha, ParameterColorValue baseColor)
        {
            return FromArgb(
                (uint)alpha << ARGBAlphaShift |
                (uint)baseColor.Value & ~ARGBAlphaMask
            );
        }

        public static ParameterColorValue FromArgb(byte red, byte green, byte blue) => FromArgb(byte.MaxValue, red, green, blue);

#if !NET35
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private void GetRgbValues(out byte r, out byte g, out byte b)
        {
            uint value = (uint)Value;
            r = (byte)((value & ARGBRedMask) >> ARGBRedShift);
            g = (byte)((value & ARGBGreenMask) >> ARGBGreenShift);
            b = (byte)((value & ARGBBlueMask) >> ARGBBlueShift);
        }

        public float GetBrightness()
        {
            GetRgbValues(out byte r, out byte g, out byte b);

            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            return (max + min) / (byte.MaxValue * 2f);
        }

        public float GetHue()
        {
            GetRgbValues(out byte r, out byte g, out byte b);

            if (r == g && g == b)
                return 0f;

            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            float delta = max - min;
            float hue;

            if (r == max)
                hue = (g - b) / delta;
            else if (g == max)
                hue = (b - r) / delta + 2f;
            else
                hue = (r - g) / delta + 4f;

            hue *= 60f;
            if (hue < 0f)
                hue += 360f;

            return hue;
        }

        public float GetSaturation()
        {
            GetRgbValues(out byte r, out byte g, out byte b);

            if (r == g && g == b)
                return 0f;

            int min = Math.Min(Math.Min(r, g), b);
            int max = Math.Max(Math.Max(r, g), b);

            int div = max + min;
            if (div > byte.MaxValue)
                div = byte.MaxValue * 2 - max - min;

            return (max - min) / (float)div;
        }

        public int ToArgb() => unchecked((int)Value);

        public override string ToString()
        {
            if ((state & StateValueMask) != 0)
            {
                return nameof(ParameterColorValue) + " [A=" + A + ", R=" + R + ", G=" + G + ", B=" + B + "]";
            }

            return nameof(ParameterColorValue) + " [Empty]";
        }

        public static bool operator ==(ParameterColorValue left, ParameterColorValue right) =>
            left.value == right.value
                && left.state == right.state;

        public static bool operator !=(ParameterColorValue left, ParameterColorValue right) => !(left == right);

        public override bool Equals(object? obj) => obj is ParameterColorValue other && Equals(other);

        public bool Equals(ParameterColorValue other) => this == other;

        public override int GetHashCode() => unchecked(value.GetHashCode() * 397 ^ state.GetHashCode());
    }
}
