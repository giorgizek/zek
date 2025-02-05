using System.Collections;

namespace Zek.Utils
{
    public static class CheckHelper
    {
        public static bool IsNullOrDefault(object? obj)
        {
            return (obj == null) ||
                   (obj == DBNull.Value) ||
                   (obj is string && ((string)obj).Length == 0) ||

                   (obj is byte && (byte)obj == 0) ||
                   (obj is short && (short)obj == 0) ||
                   (obj is int && (int)obj == 0) ||
                   (obj is long && (long)obj == 0L) ||

                   (obj is decimal && (decimal)obj == decimal.Zero) ||
                   (obj is double && (double)obj == 0D) ||
                   (obj is float && (float)obj == 0F) ||

                   (obj is sbyte && (sbyte)obj == 0) ||
                   (obj is ushort && (ushort)obj == 0) ||
                   (obj is uint && (uint)obj == 0U) ||
                   (obj is ulong && (ulong)obj == 0UL) ||

                   (obj is Guid && (Guid)obj == Guid.Empty) ||
                   (obj is Array && ((Array)obj).Length == 0) ||
                   (obj is IList && ((IList)obj).Count == 0);
        }
    }
}
