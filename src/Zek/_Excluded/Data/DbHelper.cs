using System.Data;
using Zek.Utils;

namespace Zek.Data
{
    public class DbHelper
    {
        /// <summary>
        /// Converts Datareader rows into List of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static List<T>? DataReaderMapToList<T>(IDataReader dr)// where T : new()
        {
            if (dr == null) return null;

            var result = new List<T>();


            var properties = typeof(T).GetProperties().Where(p => p.CanWrite).ToArray();
            var map = new Dictionary<string, int>();
            for (var i = 0; i < dr.FieldCount; i++)
            {

                var prop = properties.FirstOrDefault(x => string.Equals(x.Name, dr.GetName(i).Replace(" ", string.Empty), StringComparison.CurrentCultureIgnoreCase));
                if (prop == null || map.ContainsKey(prop.Name)) continue;
                map.Add(prop.Name, i);
            }
            //if (map.Count != 0 ) return null;
            properties = properties.Where(x => map.ContainsKey(x.Name)).ToArray();

            try
            {
                var rowIndex = -1;
                while (dr.Read())
                {
                    rowIndex++;
                    var obj = typeof(T) != typeof(string) ? Activator.CreateInstance<T>() : (object)null;
                    if (properties.Length > 0)
                    {
                        for (var i = 0; i < properties.Length; i++)
                        {
                            var value = dr.GetValue(map[properties[i].Name]);
                            try
                            {
                                if (value != null && value != DBNull.Value)
                                    properties[i].SetValue(obj, ConvertHelper.ChangeType(value, properties[i].PropertyType), null);
                            }
                            catch (InvalidCastException ex)
                            {
                                throw new InvalidCastException($"Can't change type (RowIndex: '{rowIndex}', ColumnIndex: '{map[properties[i].Name]}', Property: '{properties[i].Name}', ConversionType: '{properties[i].PropertyType}', Value: '{(value != null ? value.ToString() : string.Empty)}').", ex);
                            }
                            catch (FormatException ex)
                            {
                                throw new FormatException($"Can't change type (RowIndex: '{rowIndex}', ColumnIndex: '{map[properties[i].Name]}', Property: '{properties[i].Name}', ConversionType: '{properties[i].PropertyType}', Value: '{(value != null ? value.ToString() : string.Empty)}').", ex);
                            }
                        }
                    }
                    else//if it struct list (ex: List<int>);
                    {
                        //obj = (T)dr[0];
                        obj = dr.GetValue(0);
                    }

                    result.Add((T)obj);
                }
            }
            finally
            {
                //if (dr != null)
                dr.Close();
            }

            return result;



          



        /// <summary>
        /// Converts Datareader rows into Dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> DataReaderMapToDictionary<TKey, TValue>(IDataReader dr)// where T : new()
        {
            if (dr == null) return null;

            var result = new Dictionary<TKey, TValue>();

            try
            {
                while (dr.Read())
                {
                    result.Add((TKey)dr.GetValue(0), (TValue)dr.GetValue(1));
                }
            }
            finally
            {
                //if (dr != null)
                dr.Close();
            }

            return result;
        }
    }
}
