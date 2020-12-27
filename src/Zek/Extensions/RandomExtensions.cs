using System;

namespace Zek.Extensions
{
    public static class RandomExtensions
    {
        public static decimal NextDecimal(this Random rng)
        {
            return rng.Next() * (1M / int.MaxValue);
        }
        public static decimal NextDecimal(this Random rng, decimal maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }

            return rng.NextDecimal() * maxValue;
        }

        public static decimal NextDecimal(this Random rng, decimal minValue, decimal maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }

            var range = maxValue - minValue;
            if (range <= int.MaxValue)
            {
                return rng.NextDecimal() * range + minValue;
            }
            else
            {
                return (rng.GetSampleForLargeRange() * range) + minValue;
            }
        }


        private static decimal GetSampleForLargeRange(this Random rng)
        {
            // The distribution of double value returned by Sample
            // is not distributed well enough for a large range.
            // If we use Sample for a range [int.MinValue..int.MaxValue)
            // We will end up getting even numbers only.

            var result = rng.Next();
            // Note we can't use addition here. The distribution will be bad if we do that.
            var negative = (rng.Next() % 2 == 0);  // decide the sign based on second sample
            if (negative)
            {
                result = -result;
            }
            decimal d = result;
            d += (int.MaxValue - 1); // get a number in range [0 .. 2 * Int32MaxValue - 1)
            d /= 2 * (uint)int.MaxValue - 1;
            return d;
        }
    }
}
