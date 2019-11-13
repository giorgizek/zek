namespace Zek.DataAnnotations
{
    public class RegularExpressionIfAttribute : RequiredIfAttribute
    {
        public string Pattern { get; set; }

        public RegularExpressionIfAttribute(string pattern, string dependentProperty, Operator @operator, object dependentValue)
            : base(dependentProperty, @operator, dependentValue)
        {
            Pattern = pattern;
        }

        public RegularExpressionIfAttribute(string pattern, string dependentProperty, object dependentValue)
            : this(pattern, dependentProperty, Operator.EqualTo, dependentValue) { }

        protected override bool InternalIsValid(object value, object dependentPropertyValue)
        {
            if (Metadata.IsValid(dependentPropertyValue, DependentValue))
                return OperatorMetadata.Get(Operator.RegExMatch).IsValid(value, Pattern);

            return true;
        }


        public override string FormatErrorMessage(string name)
        {
            if (string.IsNullOrEmpty(ErrorMessageResourceName) && string.IsNullOrEmpty(ErrorMessage))
                ErrorMessage = DefaultErrorMessage;

            return string.Format(ErrorMessageString, name, DependentProperty, DependentValue, Pattern);
        }

        protected override string DefaultErrorMessage => "{0} must be in the format of {3} due to {1} being " + Metadata.ErrorMessage + " {2}";
    }
}