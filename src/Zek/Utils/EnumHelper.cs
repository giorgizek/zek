using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zek.Utils
{
    public static class EnumHelper
    {
        public static T[] ToEnumArray<T>(string str, char[] separator = null)
        where T : Enum
        {
            if (str is null)
                return null;

            var split = StringHelper.Split(str, separator);
            var list = new List<T>();
            foreach (var s in split)
            {
                if (int.TryParse(s, out var num))
                {
                    list.Add((T)(object)num);
                }
            }
            return [.. list];
        }



        public static Dictionary<TEnum, string> GetDisplayDictionary<TEnum>()
        {
            var values = Enum.GetValues(typeof(TEnum));
            var result = new Dictionary<TEnum, string>();
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in values)
            {
                var field = value.GetType().GetField(value.ToString());
                if (field == null) continue;
                var attribute = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();

                result.Add((TEnum)value, attribute != null ? attribute.GetName() : value.ToString());
            }

            return result;
        }
        public static List<KeyPair<TEnum, string>> GetDisplayList<TEnum>()
        {
            var values = Enum.GetValues(typeof(TEnum));
            var result = new List<KeyPair<TEnum, string>>();
            var displayAttributeType = typeof(DisplayAttribute);

            foreach (var value in values)
            {
                var field = value.GetType().GetField(value.ToString());
                if (field == null) continue;
                var attribute = (DisplayAttribute)field.GetCustomAttributes(displayAttributeType, false).FirstOrDefault();

                result.Add(new KeyPair<TEnum, string>((TEnum)value, attribute != null ? attribute.GetName() : value.ToString()));
            }

            return result;
        }

        public static NameValueCollection ToNameValueCollection<T>() where T : struct
        {
            var result = new NameValueCollection();

            if (!typeof(T).IsEnum) return result;

            var enumType = typeof(T);
            var values = Enum.GetValues(enumType);
            foreach (var value in values)
            {
                var memInfo = enumType.GetMember(enumType.GetEnumName(value));
                var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                var description = descriptionAttributes.Length > 0
                    ? ((DescriptionAttribute)descriptionAttributes.First()).Description
                    : value.ToString();
                result.Add(description, value.ToString());
            }

            return result;
        }


        private static readonly object Lock = new();
        private static Dictionary<Type, HashSet<long>> _cache;
        //private static Dictionary<Type, HashSet<long>> Cache
        //{
        //    get
        //    {
        //        if (_cache == null)
        //        {
        //            lock (Lock)
        //            {
        //                if (_cache == null)
        //                    _cache = new Dictionary<Type, HashSet<long>>();
        //            }
        //        }
        //        return _cache;
        //    }
        //}

        //public static bool IsDefined<T>(long value, bool cache = false) where T : struct, IConvertible, IComparable, IFormattable
        //{
        //    return IsDefined(typeof(T), value, cache);
        //}

        public static bool IsDefined(Type enumType, object value, bool cache = false)
        {
            //if (enumType.GetTypeInfo().GetCustomAttribute<FlagsAttribute>() != null)
            //{
            //    return IsFlagsDefined(enumType, value);
            //}

            if (cache)
            {
                if (_cache == null)
                {
                    lock (Lock)
                    {
                        if (_cache == null)
                            _cache = [];
                    }
                }

                if (!_cache.TryGetValue(enumType, out var hash))
                {
                    lock (Lock)
                    {
                        hash = [];
                        foreach (var enumValue in Enum.GetValues(enumType))
                        {
                            hash.Add(Convert.ToInt64(enumValue));
                        }

                        _cache[enumType] = hash;
                    }
                }

                return hash.Contains(Convert.ToInt64(value));
            }

            return Enum.IsDefined(enumType, value);
        }

        public static bool IsFlagsDefined(Type enumType, object value)
        {
            var typeName = Enum.GetUnderlyingType(enumType).Name;

            switch (typeName)
            {
                case "Byte":
                    {
                        var typedValue = (byte)value;
                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "Int16":
                    {
                        var typedValue = (short)value;

                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "Int32":
                    {
                        var typedValue = (int)value;

                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "Int64":
                    {
                        var typedValue = (long)value;

                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "SByte":
                    {
                        var typedValue = (sbyte)value;

                        return EvaluateFlagEnumValues(Convert.ToInt64(typedValue), enumType);
                    }

                case "UInt16":
                    {
                        var typedValue = (ushort)value;
                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "UInt32":
                    {
                        var typedValue = (uint)value;
                        return EvaluateFlagEnumValues(typedValue, enumType);
                    }

                case "UInt64":
                    {
                        var typedValue = (ulong)value;
                        return EvaluateFlagEnumValues((long)typedValue, enumType);
                    }

                default:
                    var message = string.Format("Unexpected typeName of '{0}' during flags enum evaluation.", typeName);
                    throw new ArgumentOutOfRangeException(nameof(typeName), message);
            }
        }
        private static bool EvaluateFlagEnumValues(long value, Type enumType)
        {
            long mask = 0;
            foreach (var enumValue in Enum.GetValues(enumType))
            {
                var enumValueAsInt64 = Convert.ToInt64(enumValue);
                if ((enumValueAsInt64 & value) == enumValueAsInt64)
                {
                    mask |= enumValueAsInt64;
                    if (mask == value)
                        return true;
                }
            }
            return false;
        }
    }
}
