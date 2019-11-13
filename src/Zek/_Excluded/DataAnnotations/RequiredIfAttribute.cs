namespace Zek.DataAnnotations
{
    public class RequiredIfAttribute : ContingentValidationAttribute
    {
        public Operator Operator { get; }
        public object DependentValue { get; }
        protected OperatorMetadata Metadata { get; }

        public RequiredIfAttribute(string dependentProperty, Operator @operator, object dependentValue)
            : base(dependentProperty)
        {
            Operator = @operator;
            DependentValue = dependentValue;
            Metadata = OperatorMetadata.Get(Operator);
        }

        public RequiredIfAttribute(string dependentProperty, object dependentValue)
            : this(dependentProperty, Operator.EqualTo, dependentValue) { }

        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty, DependentValue);
        }

        protected override bool InternalIsValid(object value, object dependentPropertyValue)
        {
            if (Metadata.IsValid(dependentPropertyValue, DependentValue))
                return !string.IsNullOrEmpty(value?.ToString().Trim());

            return true;
        }

        protected override string DefaultErrorMessage => "{0} is required due to {1} being " + Metadata.ErrorMessage + " {2}";
    }
}