using Microsoft.AspNetCore.Http;
using System;
using System.Text;

namespace Zek.Web
{
    public static class SessionExtensions
    {

        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));

            var bytes = new byte[]
            {
                (value ? (byte)1 : (byte)0)
            };
            session.Set(key, bytes);
        }
        public static bool GetBoolean(this ISession session, string key)
        {
            //byte[] value;
            //if (!session.TryGetValue(key, out value))
            //    return false;

            //return BitConverter.ToBoolean(value, 0);

            var data = session.Get(key);
            if (data == null || data.Length == 0)
            {
                return false;
            }

            return data[0] != 0;
        }


        //public static int GetInt32(this ISession session, string key)
        //{
        //    byte[] value;
        //    if (!session.TryGetValue(key, out value))
        //        return 0;

        //    return BitConverter.ToInt32(value, 0);
        //}
        //public static void SetInt32(this ISession session, string key, bool value)
        //{
        //    session.Set(key, BitConverter.GetBytes(value));
        //}


        public static void SetByte(this ISession session, string key, byte value)
        {
            var bytes = new byte[]
            {
                value
            };
            session.Set(key, bytes);
        }
        public static byte GetByte(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length == 0)
            {
                return 0;
            }

            return data[0];
        }

        public static void SetInt16(this ISession session, string key, short value)
        {
            var bytes = new byte[]
            {
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };
            session.Set(key, bytes);
        }
        public static short GetInt16(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
            {
                return 0;
            }
            return (short)(data[0] << 8 | data[1]);
        }

        public static void SetInt32(this ISession session, string key, int value)
        {
            var bytes = new byte[]
            {
                (byte)(value >> 24),
                (byte)(0xFF & (value >> 16)),
                (byte)(0xFF & (value >> 8)),
                (byte)(0xFF & value)
            };
            session.Set(key, bytes);
        }
        public static int GetInt32(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null || data.Length < 4)
            {
                return 0;
            }
            return data[0] << 24 | data[1] << 16 | data[2] << 8 | data[3];
        }

        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.UTF8.GetBytes(value));
        }
        public static string GetString(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return Encoding.UTF8.GetString(data);
        }


        public static byte[] Get(this ISession session, string key)
        {
            byte[] value = null;
            session.TryGetValue(key, out value);
            return value;
        }
    }
}