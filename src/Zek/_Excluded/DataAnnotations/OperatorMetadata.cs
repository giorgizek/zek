using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Zek.DataAnnotations
{
    public class OperatorMetadata
    {
        public string ErrorMessage { get; set; }
        public Func<object, object, bool> IsValid { get; set; }

        static OperatorMetadata()
        {
            CreateOperatorMetadata();
        }

        private static Dictionary<Operator, OperatorMetadata> _operatorMetadata;

        public static OperatorMetadata Get(Operator @operator)
        {
            return _operatorMetadata[@operator];
        }

        private static void CreateOperatorMetadata()
        {
            _operatorMetadata = new Dictionary<Operator, OperatorMetadata>
            {
                [Operator.EqualTo] = new OperatorMetadata
                {
                    ErrorMessage = "equal to",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null && dependentPropertyValue == null)
                            return true;
                        return value != null && value.Equals(dependentPropertyValue);
                    }
                },
                [Operator.NotEqualTo] = new OperatorMetadata
                {
                    ErrorMessage = "not equal to",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null && dependentPropertyValue != null)
                            return true;
                        if (value == null)
                            return false;

                        return !value.Equals(dependentPropertyValue);
                    }
                },
                [Operator.GreaterThan] = new OperatorMetadata
                {
                    ErrorMessage = "greater than",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null || dependentPropertyValue == null)
                            return false;

                        return Comparer<object>.Default.Compare(value, dependentPropertyValue) >= 1;
                    }
                },
                [Operator.LessThan] = new OperatorMetadata
                {
                    ErrorMessage = "less than",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null || dependentPropertyValue == null)
                            return false;

                        return Comparer<object>.Default.Compare(value, dependentPropertyValue) <= -1;
                    }
                },
                [Operator.GreaterThanOrEqualTo] = new OperatorMetadata
                {
                    ErrorMessage = "greater than or equal to",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null && dependentPropertyValue == null)
                            return true;

                        if (value == null || dependentPropertyValue == null)
                            return false;

                        return Get(Operator.EqualTo).IsValid(value, dependentPropertyValue) || Comparer<object>.Default.Compare(value, dependentPropertyValue) >= 1;
                    }
                },
                [Operator.LessThanOrEqualTo] = new OperatorMetadata
                {
                    ErrorMessage = "less than or equal to",
                    IsValid = (value, dependentPropertyValue) =>
                    {
                        if (value == null && dependentPropertyValue == null)
                            return true;

                        if (value == null || dependentPropertyValue == null)
                            return false;

                        return Get(Operator.EqualTo).IsValid(value, dependentPropertyValue) || Comparer<object>.Default.Compare(value, dependentPropertyValue) <= -1;
                    }
                },
                [Operator.RegExMatch] = new OperatorMetadata
                {
                    ErrorMessage = "a match to",
                    IsValid = (value, dependentPropertyValue) => Regex.Match((value ?? "").ToString(), dependentPropertyValue.ToString()).Success
                },
                [Operator.NotRegExMatch] = new OperatorMetadata
                {
                    ErrorMessage = "not a match to",
                    IsValid = (value, dependentPropertyValue) => !Regex.Match((value ?? "").ToString(), dependentPropertyValue.ToString()).Success
                }
            };
        }
    }
}
