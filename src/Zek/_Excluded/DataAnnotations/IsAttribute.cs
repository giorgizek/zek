namespace Zek.DataAnnotations
{
    public class IsAttribute : ContingentValidationAttribute
    {
        public Operator Operator { get; }
        public bool PassOnNull { get; set; }
        private readonly OperatorMetadata _metadata;

        public IsAttribute(Operator @operator, string dependentProperty) : base(dependentProperty)
        {
            Operator = @operator;
            PassOnNull = false;
            _metadata = OperatorMetadata.Get(Operator);
        }



        protected override bool InternalIsValid(object value, object dependentPropertyValue)
        {
            if (PassOnNull && (value == null || dependentPropertyValue == null) && (value != null || dependentPropertyValue != null))
                return true;

            return _metadata.IsValid(value, dependentPropertyValue);
        }
     

        protected override string DefaultErrorMessage => "{0} must be " + _metadata.ErrorMessage + " {1}.";
    }
}
