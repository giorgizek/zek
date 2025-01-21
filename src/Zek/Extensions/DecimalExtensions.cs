namespace Zek.Extensions
{
    public static class DecimalExtensions
    {
        //public static int ToFractionalUnit32(this decimal value)
        //{
        //    return (int)(value * 100M);
        //}

        //public static long ToFractionalUnit64(this decimal value)
        //{
        //    return (long)(value * 100M);
        //}




        /// <summary>
        /// Determines if the decimal value is less than or equal to the decimal parameter according to the defined precision.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="precision">The precision.  The number of digits after the decimal that will be considered when comparing.</param>
        /// <returns></returns>
        public static bool LessThan(this decimal value1, decimal value2, int precision) => Math.Round(value1 - value2, precision) < decimal.Zero;

        /// <summary>
        /// Determines if the decimal value is less than or equal to the decimal parameter according to the defined precision.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="precision">The precision.  The number of digits after the decimal that will be considered when comparing.</param>
        /// <returns></returns>
        public static bool LessThanOrEqual(this decimal value1, decimal value2, int precision) => Math.Round(value1 - value2, precision) <= decimal.Zero;

        /// <summary>
        /// Determines if the decimal value is greater than (>) the decimal parameter according to the defined precision.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="precision">The precision.  The number of digits after the decimal that will be considered when comparing.</param>
        /// <returns></returns>
        public static bool GreaterThan(this decimal value1, decimal value2, int precision) => Math.Round(value1 - value2, precision) > decimal.Zero;

        /// <summary>
        /// Determines if the decimal value is greater than or equal to (>=) the decimal parameter according to the defined precision.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="precision">The precision.  The number of digits after the decimal that will be considered when comparing.</param>
        /// <returns></returns>
        public static bool GreaterThanOrEqual(this decimal value1, decimal value2, int precision) => Math.Round(value1 - value2, precision) >= decimal.Zero;

        /// <summary>
        /// Determines if the decimal value is equal to (==) the decimal parameter according to the defined precision.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="precision">The precision.  The number of digits after the decimal that will be considered when comparing.</param>
        /// <returns></returns>
        public static bool AlmostEquals(this decimal value1, decimal value2, int precision) => Math.Round(value1 - value2, precision) == decimal.Zero;



    }
}
