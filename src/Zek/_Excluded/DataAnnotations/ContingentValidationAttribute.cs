using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Zek.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ContingentValidationAttribute : ModelAwareValidationAttribute
    {
        protected ContingentValidationAttribute(string dependentProperty)
        {
            if (dependentProperty == null)
                throw new ArgumentNullException(nameof(dependentProperty));

            DependentProperty = dependentProperty;
        }
       

        public string DependentProperty { get; }
        public string DependentPropertyDisplayName { get; set; }

        public override bool RequiresValidationContext => true;


        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, DependentPropertyDisplayName ?? DependentProperty);
        }

        protected override string DefaultErrorMessage => "{0} is invalid due to {1}.";

        //private object GetDependentPropertyValue(object container)
        //{
        //    var currentType = container.GetType();
        //    var value = container;

        //    foreach (var propertyName in DependentProperty.Split('.'))
        //    {
        //        var property = currentType.GetProperty(propertyName);
        //        value = property.GetValue(value, null);
        //        currentType = property.PropertyType;
        //    }

        //    return value;
        //}


        private static string GetDisplayNameForProperty(Type containerType, string propertyName)
        {
            var property = containerType.GetRuntimeProperties().SingleOrDefault(prop => IsPublic(prop) && string.Equals(propertyName, prop.Name, StringComparison.OrdinalIgnoreCase) && !prop.GetIndexParameters().Any());
            if (property == null)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The property {0}.{1} could not be found", containerType.FullName, propertyName));
            var attribute = property.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();
            return attribute != null
                ? attribute.GetName()
                : propertyName;
        }
        private static bool IsPublic(PropertyInfo p) => ((p.GetMethod != null) && p.GetMethod.IsPublic) || ((p.SetMethod != null) && p.SetMethod.IsPublic);


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var runtimeProperty = validationContext.ObjectType.GetRuntimeProperty(DependentProperty);
            if (runtimeProperty == null)
                return new ValidationResult($"Could not find a property named {DependentProperty}.");

            var dependentPropertyValue = runtimeProperty.GetValue(validationContext.ObjectInstance, null);
            if (InternalIsValid(value, dependentPropertyValue))
                return ValidationResult.Success;

            if (DependentPropertyDisplayName == null)
                DependentPropertyDisplayName = GetDisplayNameForProperty(validationContext.ObjectType, DependentProperty);

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        
        protected abstract bool InternalIsValid(object value, object dependentPropertyValue);
    }
}
