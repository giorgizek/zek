using System;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class TimeDisplayFormatAttribute : DisplayFormatAttribute
    {
        public TimeDisplayFormatAttribute()
        {
            DataFormatString = "{0:HH:mm:ss}";
        }
    }
}
