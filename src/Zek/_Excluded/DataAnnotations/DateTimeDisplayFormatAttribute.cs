using System;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateTimeDisplayFormatAttribute : DisplayFormatAttribute
    {
        public DateTimeDisplayFormatAttribute()
        {
            DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}";
            ApplyFormatInEditMode = true;
        }
    }
}
