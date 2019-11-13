using System;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateDisplayFormatAttribute : DisplayFormatAttribute
    {
        public DateDisplayFormatAttribute()
        {
            //DataFormatString = "{0:dd.MM.yyyy}";
            DataFormatString = "{0:yyyy-MM-dd}";
            //ApplyFormatInEditMode = true;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class GeorgianDateDisplayFormatAttribute : DisplayFormatAttribute
    {
        public GeorgianDateDisplayFormatAttribute()
        {
            DataFormatString = "{0:dd.MM.yyyy}";
        }
    }
}
