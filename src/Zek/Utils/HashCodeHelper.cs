namespace Zek.Utils
{
    public static class HashCodeHelper
    {
        //public static int CalcHashCode2(params int[] array)
        //{
        //    var num = 0x15051505;
        //    var num2 = num;
        //    var i = 0;
        //    for (var j = array.Length * 2; j > 0; j -= 4, i += 2)
        //    {
        //        num = (((num << 5) + num) + (num >> 0x1b)) ^ array[i];
        //        if (j <= 2)
        //        {
        //            break;
        //        }
        //        num2 = (((num2 << 5) + num2) + (num2 >> 0x1b)) ^ array[i + 1];
        //    }
        //    return num + (num2 * 0x5d588b65);
        //}
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
                if (value != null)
                    result ^= value.GetHashCode();
            return result;
        }
        public static int RotateValue(int val, int count)
        {
            var shift = (13 * count) & 0x1F;
            return (val << shift) | (val >> (32 - shift));
        }
    }
}
