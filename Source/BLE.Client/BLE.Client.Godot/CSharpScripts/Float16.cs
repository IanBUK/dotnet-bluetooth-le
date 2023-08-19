using System;
namespace BLEScan
{
    public static class Float16
    {
        public static double FromFloat16(this double result, short value)
        {
            double f;
            var signBit = (value & 0x8000) > 0;
            var exponent = (value & 0x7C00) >> 10;
            var mantissa = value & 0x03FF;

            // zero
            if ((value & 0x7FFF) == 0) return signBit ? -0 : 0;
            // infinity & NaN
            if (exponent == 0x001F)
            {
                if (mantissa == 0) return signBit ? -0 : 0;
                return 0;
            }

            // subnormal/normal
            f = exponent == 0 ? 0 : 1;

            // Process mantissa
            for (var i = 9; i >= 0; i--)
            {
                f *= 2;
                var shift = 1 << i;
                var mask = mantissa & shift;
                if (mask != 0) f = f + 1;
            }

            f = f * Math.Pow(2.0, exponent - 25);
            if (exponent == 0) f = f * Math.Pow(2.0, -13); // 5.96046447754e-8;
            return signBit ? -f : f;
        }

        public static double FromFloat16(this double result, byte upper, byte lower)
        {
            double f;
            var value = (upper << 8) + lower;
            var signBit = (value & 0x8000) > 0;
            var exponent = (value & 0x7C00) >> 10;
            var mantissa = value & 0x03FF;

            // zero
            if ((value & 0x7FFF) == 0) return signBit ? -0 : 0;
            // infinity & NaN
            if (exponent == 0x001F)
            {
                if (mantissa == 0) return signBit ? -0 : 0;
                return 0;
            }

            // subnormal/normal
            f = exponent == 0 ? 0 : 1;

            // Process mantissa
            for (var i = 9; i >= 0; i--)
            {
                f *= 2;
                var shift = 1 << i;
                var mask = mantissa & shift;
                if (mask != 0) f = f + 1;
            }

            f = f * Math.Pow(2.0, exponent - 25);
            if (exponent == 0) f = f * Math.Pow(2.0, -13); // 5.96046447754e-8;
            return signBit ? -f : f;
        }
    }
}

