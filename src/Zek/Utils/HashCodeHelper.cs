namespace Zek.Utils
{
    public static class HashCodeHelper
    {
        public static int CalcHashCode(params int[] values)
        {
            var count = values.Length;
            var result = 0;
            for (var i = 0; i < count; i++)
            {
                result ^= RotateValue(values[i], count);
            }
            return result;
        }
        public static int CalcHashCode(params object[] values)
        {
            var result = 0;
            foreach (var value in values)
            {
                if (value != null)
                {
                    result ^= value.GetHashCode();
                }
            }
            return result;
        }
        public static int RotateValue(int val, int count)
        {
            var shift = (13 * count) & 0x1F;
            return (val << shift) | (val >> (32 - shift));
        }
    }
}
