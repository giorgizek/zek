using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredIfAnyNotEmptyAttribute : ModelAwareValidationAttribute
    {
        public string[] DependentProperties { get; }

        public RequiredIfAnyNotEmptyAttribute(params string[] dependentProperties)
        {
            if (dependentProperties == null)
                throw new ArgumentNullException(nameof(dependentProperties));

            DependentProperties = dependentProperties;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isAllEmpty = true;
            foreach (var dependentProperty in DependentProperties)
            {
                var runtimeProperty = validationContext.ObjectType.GetRuntimeProperty(dependentProperty);
                if (runtimeProperty == null)
                    return new ValidationResult($"Could not find a property named {dependentProperty}.");

                var dependentPropertyValue = runtimeProperty.GetValue(validationContext.ObjectInstance, null);
                if (dependentPropertyValue != null)
                    isAllEmpty = false;
            }

            if (!isAllEmpty && value == null)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            else
                return ValidationResult.Success;

        }
    }
}
