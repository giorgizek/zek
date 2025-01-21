using System.Text;

namespace Zek.Extensions.Collections
{
    public static class ArrayExtensions
    {
        public static string ToString<T>(this Array collection, string separator)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item);
                sb.Append(separator);
            }
            return sb.ToString(0, Math.Max(0, sb.Length - separator.Length));  // Remove at the end is faster
        }



    }
}
