namespace Zek.DataAnnotations
{
    public class RequiredIfEmptyAttribute : ContingentValidationAttribute
    {
        public RequiredIfEmptyAttribute(string dependentProperty)
            : base(dependentProperty) { }

        protected override bool InternalIsValid(object value, object dependentPropertyValue)
        {
            if (string.IsNullOrEmpty((dependentPropertyValue ?? string.Empty).ToString().Trim()))
                return !string.IsNullOrEmpty(value?.ToString().Trim());

            return true;
        }

        protected override string DefaultErrorMessage => "{0} is required due to {1} being empty.";
        
    }
}