using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class DynamicYearRangeAttribute : YearRangeAttribute
    {
        public DynamicYearRangeAttribute(int minimum, int maximum) : base(DateTime.Now.Year + minimum, DateTime.Now.Year + maximum)
        {
        }

        public DynamicYearRangeAttribute(int minimum) : base(DateTime.Now.Year + minimum)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class YearRangeAttribute : RangeAttribute, IClientModelValidator
    {
        public YearRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
        {
        }

        public YearRangeAttribute(int minimum) : base(minimum, DateTime.Today.Year)
        {
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-range", string.Format(ErrorMessageString, context.ModelMetadata.DisplayName, Minimum, Maximum));
            MergeAttribute(context.Attributes, "data-val-range-max", Maximum.ToString());
            MergeAttribute(context.Attributes, "data-val-range-min", Minimum.ToString());
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return;
            attributes.Add(key, value);
        }
    }
}
