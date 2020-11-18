using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zek.Web
{
    public static class SelectListExtensions
    {

        /// <summary>
        /// Returns a collection of SelectListItem for each of the items in the collection passed in, with a specific list item selected and optionally an empty list item.
        /// </summary>
        /// <example>
        /// <code>
        /// people.ToSelectList(x => x.PersonId, x => x.Name, "2345");
        /// // or
        /// people.ToSelectList(x => x.PersonId, x => x.Name, "2345", false); if you don't want the empty list item
        /// </code>
        /// </example>
        /// <param name="source"></param>
        /// <param name="keySelector">The property to use as the value attribute of each list item.</param>    
        /// <param name="textSelector">The property to use as the text attribute of each list item.</param>
        /// <param name="currentKey">The String value of the list item that should be selected by default.</param>
        /// <param name="includeEmptyListItem">Whether or not a default list item should be the first list item before those from the collection.</param>
        public static IList<SelectListItem> ToSelectList<TType, TKey>(this IEnumerable<TType> source, Func<TType, TKey> keySelector, Func<TType, string> textSelector, TKey currentKey = default(TKey), bool includeEmptyListItem = false) where TType : class
        {
            return ToSelectList(source, keySelector, textSelector, currentKey == null ? null : new[] { currentKey }, includeEmptyListItem ? new SelectListItem() : null);
        }

        /// <summary>
        /// Returns a collection of SelectListItem for each of the items in the collection passed in, with a specific list item selected and a custom empty list item.
        /// </summary>
        /// <example>
        /// <code>
        /// people.ToSelectList(x => x.PersonId, x => x.Name, "2345", new SelectListItem() {Text = "-- Pick One --", Value = ""});
        /// </code>
        /// </example>
        /// <param name="source"></param>
        /// <param name="keySelector">The property to use as the value attribute of each list item.</param>    
        /// <param name="textSelector">The property to use as the text attribute of each list item.</param>
        /// <param name="currentKey">The String value of the list item that should be selected by default.</param>
        /// <param name="emptyListItem">The list item to use as the first list item before those from the collection.</param>
        public static IList<SelectListItem> ToSelectList<TType, TKey>(this IEnumerable<TType> source, Func<TType, TKey> keySelector, Func<TType, string> textSelector, TKey currentKey, SelectListItem emptyListItem) where TType : class
        {
            return ToSelectList(source, keySelector, textSelector, currentKey == null ? null : new[] { currentKey }, emptyListItem);
        }

        public static IList<SelectListItem> ToSelectList<TType, TKey>(this IEnumerable<TType> source, Func<TType, TKey> keySelector, Func<TType, string> textSelector, IEnumerable<TKey> currentKeys, bool includeEmptyListItem = false) where TType : class
        {
            return ToSelectList(source, keySelector, textSelector, currentKeys, includeEmptyListItem ? new SelectListItem() : null);
        }
        public static IList<SelectListItem> ToSelectList<TType, TKey>(this IEnumerable<TType> source, Func<TType, TKey> keySelector, Func<TType, string> textSelector, IEnumerable<TKey> currentKeys, SelectListItem emptyListItem) where TType : class
        {
            var selectList = new List<SelectListItem>();
            if (source != null)
                selectList = source.Select(item => new SelectListItem
                {
                    //Value = keySelector.Invoke(x).ToString(),
                    //Text = textSelector.Invoke(x),
                    //Selected = currentKeys != null && currentKeys.Contains(keySelector.Invoke(x))

                    Value = keySelector(item).ToString(),
                    Text = textSelector(item),
                    Selected = currentKeys != null && currentKeys.Contains(keySelector(item))
                }).ToList();

            if (emptyListItem != null)
                selectList.Insert(0, emptyListItem);

            return selectList;
        }


        public static SelectList ToSelectList(this Dictionary<Enum, string> data, object selectedValue = null, string dataGroupField = null)
        {
            return new SelectList(data, "Key", "Value", selectedValue);
        }
        public static SelectList ToSelectList(this Dictionary<int, string> data, object selectedValue = null, string dataGroupField = null)
        {
            return new SelectList(data, "Key", "Value", selectedValue);
        }
        public static SelectList ToSelectList(this Dictionary<short, string> data, object selectedValue = null, string dataGroupField = null)
        {
            return new SelectList(data, "Key", "Value", selectedValue);
        }
        public static SelectList ToSelectList(this Dictionary<byte, string> data, object selectedValue = null, string dataGroupField = null)
        {
            return new SelectList(data, "Key", "Value", selectedValue);
        }
    }
}
