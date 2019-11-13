using System;
using System.Reflection;

namespace Zek.Extensions
{
    public static class TypeExtensions
    {

        /// <summary>
        ///  Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, Single, Decimal, String, DateTime and Guid.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Return true if type is primitive. Otherwise false</returns>
        public static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive
                   || type == typeof(string)
                   || type == typeof(char?)
                   || type == typeof(DateTime)
                   || type == typeof(DateTime?)
                   || type == typeof(Guid)
                   || type == typeof(Guid?)
                   || type == typeof(bool?)
                   || type == typeof(byte?)
                   || type == typeof(sbyte?)
                   || type == typeof(short?)
                   || type == typeof(ushort?)
                   || type == typeof(int?)
                   || type == typeof(uint?)
                   || type == typeof(long?)
                   || type == typeof(ulong?)
                   || type == typeof(decimal)
                   || type == typeof(decimal?)
                   || type == typeof(double?)
                   || type == typeof(float?)
                ;
        }
    }
}
