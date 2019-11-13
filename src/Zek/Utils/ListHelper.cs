using System.Collections.Generic;

namespace Zek.Utils
{
    public static class ListHelper
    {
        /// <summary>
        /// აბრუნებს შეერთებული კოლქეციებს (უნიკალურებს).
        /// </summary>
        /// <typeparam name="T">კოლექციის ტიპი.</typeparam>
        /// <param name="firstList"></param>
        /// <param name="secondList"></param>
        /// <returns>აბრუნებს მხოლოდ უნიკალურ ჩანაწერებს.</returns>
        public static List<T> GetMerged<T>(List<T> firstList, List<T> secondList)
        {
            var mergedList = new List<T>();
            foreach (var item in firstList)
            {
                if (!mergedList.Contains(item))
                    mergedList.Add(item);
            }

            foreach (var item in secondList)
            {
                if (!mergedList.Contains(item))
                    mergedList.Add(item);
            }

            return mergedList;
        }
    }
}
