using System.Text.RegularExpressions;

namespace Zek.Utils
{
    public enum IbanValidationResult
    {
        IsValid,
        ValueMissing,
        ValueTooSmall,
        ValueTooBig,
        ValueFailsModule97Check,
        CountryCodeNotKnown
    }

    /// <summary>
    /// ვალიდაციის კლასი (ნომრიანი ტექსტის, ელ.ფოსტის).
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Check if string is only digits.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            foreach (var c in value)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        //public static bool IsValidEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        return false;

        //    // only return true if there is only 1 '@' character
        //    // and it is neither the first nor the last character
        //    var found = false;
        //    for (var i = 0; i < email.Length; i++)
        //    {
        //        if (email[i] == '@')
        //        {
        //            if (found || i == 0 || i == email.Length - 1)
        //            {
        //                return false;
        //            }
        //            found = true;
        //        }
        //    }

        //    return found;
        //}

        private const string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private static Regex CreateEmailRegEx()
        {
            // Workaround for CVE-2015-2526
            // If no REGEX_DEFAULT_MATCH_TIMEOUT is specified in the AppDomain, default to 2 seconds. 
            // if we're on Netstandard 1.0 we don't have access to AppDomain, so just always use 2 second timeout there. 

#if NETSTANDARD1_1 || NETSTANDARD1_6
			return new Regex(_expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
#else
            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(_emailExpression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
                }
            }
            catch
            {
            }

            return new Regex(_emailExpression, RegexOptions.IgnoreCase);
#endif
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            Regex regex = CreateEmailRegEx();

            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;
        }




        /// <summary>
        /// Validate if user charactes and length is valid
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="allowedUserNameCharacters"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string userName, int minLength = 3, int maxLength = 256, string allowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+")
        {
            if (userName == null || userName.Length < minLength || userName.Length > maxLength)
                return false;

            if (!string.IsNullOrEmpty(allowedUserNameCharacters) && userName.Any(c => !allowedUserNameCharacters.Contains(c)))
                return false;

            return true;
        }




        #region Phone

        private const string AdditionalPhoneNumberCharacters = "-.()";
        private const string ExtensionAbbreviationExtDot = "ext.";
        private const string ExtensionAbbreviationExt = "ext";
        private const string ExtensionAbbreviationX = "x";


        public static bool IsValidPhone(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;


            phoneNumber = phoneNumber.Replace("+", string.Empty).TrimEnd();
            phoneNumber = RemoveExtension(phoneNumber);

            var digitFound = false;
            foreach (var c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    digitFound = true;
                    break;
                }
            }

            if (!digitFound)
            {
                return false;
            }

            foreach (var c in phoneNumber)
            {
                if (!(char.IsDigit(c)
                    || char.IsWhiteSpace(c)
                    || AdditionalPhoneNumberCharacters.IndexOf(c) != -1))
                {
                    return false;
                }
            }

            return true;
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            var lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationExtDot, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationExtDot.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationExt, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationExt.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationX, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationX.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            return potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
            {
                return false;
            }

            foreach (var c in potentialExtension)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion


      


        /*
        /// <summary>
        /// პლასტიკური ბარათის ნომრის ვალიდაცია Luhn/mod10 ალგორითმის მიხედვით.
        /// </summary>
        /// <param name="email">ბარათის ნომერი, with or without punctuation.</param>
        /// <returns>აბრუნებს true-ს თუ ბარათის ნომერი სწორია, სხვა შემთხვევაში false.</returns>
        public static bool IsCreditCardValid(string email)
        {
            const string allowed = "0123456789";
            int i;

            var cleanNumber = new StringBuilder();
            for (i = 0; i < email.Length; i++)
            {
                if (allowed.IndexOf(email.Substring(i, 1), StringComparison.Ordinal) >= 0)
                    cleanNumber.Append(email.Substring(i, 1));
            }

            if (cleanNumber.Length < 13 || cleanNumber.Length > 16)
                return false;

            for (i = cleanNumber.Length + 1; i <= 16; i++)
                cleanNumber.Insert(0, "0");

            var total = 0;
            var number = cleanNumber.ToString();

            for (i = 1; i <= 16; i++)
            {
                var multiplier = 1 + i % 2;
                var digit = int.Parse(number.Substring(i - 1, 1));
                var sum = digit * multiplier;
                if (sum > 9)
                    sum -= 9;
                total += sum;
            }
            return total % 10 == 0;
        }*/


        /*
                private static readonly IDictionary<string, int> Lengths = new Dictionary<string, int>
                {
                    {"AL", 28},
                    {"AD", 24},
                    {"AT", 20},
                    {"AZ", 28},
                    {"BE", 16},
                    {"BH", 22},
                    {"BA", 20},
                    {"BR", 29},
                    {"BG", 22},
                    {"CR", 21},
                    {"HR", 21},
                    {"CY", 28},
                    {"CZ", 24},
                    {"DK", 18},
                    {"DO", 28},
                    {"EE", 20},
                    {"FO", 18},
                    {"FI", 18},
                    {"FR", 27},
                    {"GE", 22},
                    {"DE", 22},
                    {"GI", 23},
                    {"GR", 27},
                    {"GL", 18},
                    {"GT", 28},
                    {"HU", 28},
                    {"IS", 26},
                    {"IE", 22},
                    {"IL", 23},
                    {"IT", 27},
                    {"KZ", 20},
                    {"KW", 30},
                    {"LV", 21},
                    {"LB", 28},
                    {"LI", 21},
                    {"LT", 20},
                    {"LU", 20},
                    {"MK", 19},
                    {"MT", 31},
                    {"MR", 27},
                    {"MU", 30},
                    {"MC", 27},
                    {"MD", 24},
                    {"ME", 22},
                    {"NL", 18},
                    {"NO", 15},
                    {"PK", 24},
                    {"PS", 29},
                    {"PL", 28},
                    {"PT", 25},
                    {"RO", 24},
                    {"SM", 27},
                    {"SA", 24},
                    {"RS", 22},
                    {"SK", 24},
                    {"SI", 19},
                    {"ES", 24},
                    {"SE", 24},
                    {"CH", 21},
                    {"TN", 24},
                    {"TR", 26},
                    {"AE", 23},
                    {"GB", 22},
                    {"VG", 24}
                };

                public IbanValidationResult IsValidIban(string email)
                {
                    // Check if email is missing
                    if (string.IsNullOrEmpty(email))
                        return IbanValidationResult.ValueMissing;

                    if (email.Length < 2)
                        return IbanValidationResult.ValueTooSmall;

                    var countryCode = email.Substring(0, 2).ToUpper();

                    var countryCodeKnown = Lengths.TryGetValue(countryCode, out var lengthForCountryCode);
                    if (!countryCodeKnown)
                    {
                        return IbanValidationResult.CountryCodeNotKnown;
                    }

                    // Check length.
                    if (email.Length < lengthForCountryCode)
                        return IbanValidationResult.ValueTooSmall;

                    if (email.Length > lengthForCountryCode)
                        return IbanValidationResult.ValueTooBig;

                    email = email.ToUpper();
                    var newIban = email.Substring(4) + email.Substring(0, 4);

                    newIban = Regex.Replace(newIban, @"\D", match => (match.Value[0] - 55).ToString());

                    var remainder = long.Parse(newIban) % 97;

                    return remainder != 1 ? IbanValidationResult.ValueFailsModule97Check : IbanValidationResult.IsValid;
                }
                */
    }
}
