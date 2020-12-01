using System.Text.RegularExpressions;

namespace Zek.Utils
{
    public enum PasswordStatus
    {
        Success,
        TooShort,
        TooLong,
        NeedMoreLowerChars,
        NeedMoreUpperChars,
        NeedMoreDigitChars,
        NeedMoreSpecialChars,
    }

    public class PasswordHelper
    {
        /// <summary>
        /// Generate Random Password
        /// </summary>
        /// <param name="minLength">Min Password Length</param>
        /// <param name="maxLength">Max Password Length</param>
        /// <param name="includeLetters">(e.g. abcdef)</param>
        /// <param name="includeMixedCase">(e.g. AbcDEf)</param>
        /// <param name="includeNumbers">(e.g. a9b8c7d)</param>
        /// <param name="includeSymbols">(e.g. a!b*c_d)</param>
        /// <param name="excludeSimilarChars">(e.g. i, l, o, 1, 0, I)</param>
        /// <param name="excludeAmbiguousChars">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns>Random Password</returns>
        public static string Generate(int minLength, int maxLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarChars = true, bool excludeAmbiguousChars = true)
        {
            var passwordLength = maxLength;

            if (minLength != maxLength)
                passwordLength = RandomHelper.GetRandom().Next(minLength, maxLength);
            return Generate(passwordLength, includeLetters, includeMixedCase, includeNumbers, includeSymbols, excludeSimilarChars);
        }

        public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string UppercaseSimilarCharsExcluded = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        public const string LowercaseSimilarCharsExcluded = "abcdefghijkmnpqrstuvwxyz";

        public const string Numbers = "0123456789";
        public const string NumbersSimilarCharsExcluded = "23456789";
        public const string Special = "!@#$%^&*()_-+=[{]};:>|./?";//From Oracle Membership
        public const string SpecialAmbiguousCharsExcluded  = "!@#$%^&*_-+=?";//From Oracle Membership


        /// <summary>
        /// Generate Random Password
        /// </summary>
        /// <param name="passwordLength">Password Length</param>
        /// <param name="includeLetters">(e.g. abcdef)</param>
        /// <param name="includeMixedCase">(e.g. AbcDEf)</param>
        /// <param name="includeNumbers">(e.g. a9b8c7d)</param>
        /// <param name="includeSymbols">(e.g. a!b*c_d)</param>
        /// <param name="excludeSimilarChars"></param>
        /// <param name="excludeAmbiguousChars"></param>
        /// <returns>Random Password</returns>
        public static string Generate(int passwordLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarChars = true, bool excludeAmbiguousChars = true)
        {
            return RandomHelper.GetRandomString(passwordLength, GetAllowedChars(includeLetters, includeMixedCase, includeNumbers, includeSymbols, excludeSimilarChars, excludeAmbiguousChars));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeLetters"></param>
        /// <param name="includeMixedCase"></param>
        /// <param name="includeNumbers"></param>
        /// <param name="includeSymbols"></param>
        /// <param name="excludeSimilarChars"></param>
        /// <param name="excludeAmbiguousChars">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns></returns>
        public static string GetAllowedChars(bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarChars = true, bool excludeAmbiguousChars = true)
        {
            var allowedChars = includeLetters ? (excludeSimilarChars ? LowercaseSimilarCharsExcluded : Lowercase) : string.Empty;
            allowedChars += includeMixedCase ? (excludeSimilarChars ? UppercaseSimilarCharsExcluded : Uppercase) : string.Empty;
            allowedChars += includeNumbers ? (excludeSimilarChars ? NumbersSimilarCharsExcluded : Numbers) : string.Empty;
            if (includeSymbols)
            {
                allowedChars += excludeAmbiguousChars ? SpecialAmbiguousCharsExcluded : Special;
            }

            return allowedChars;
        }

        /// <summary>
        /// Generate random password
        /// </summary>
        /// <param name="uppercaseLetters"></param>
        /// <param name="lowercaseLetters"></param>
        /// <param name="numbers"></param>
        /// <param name="symbols">Punctuations ( e.g. @#$% )</param>
        /// <param name="excludeSimilarChars">Exclude: I O ... </param>
        /// <param name="excludeAmbiguousChars">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns></returns>
        public static string Generate(int uppercaseLetters, int lowercaseLetters, int numbers, int symbols, bool excludeSimilarChars = true, bool excludeAmbiguousChars = true)
        {
            var password = string.Empty;
            if (uppercaseLetters > 0)
            {
                var allowedChars = excludeSimilarChars ? UppercaseSimilarCharsExcluded : Uppercase;
                password += RandomHelper.GetRandomString(uppercaseLetters, allowedChars);
            }
            if (lowercaseLetters > 0)
            {
                var allowedChars = excludeSimilarChars ? LowercaseSimilarCharsExcluded : Lowercase;
                password += RandomHelper.GetRandomString(lowercaseLetters, allowedChars);
            }
            if (numbers > 0)
            {
                var allowedChars = excludeSimilarChars ? NumbersSimilarCharsExcluded : Numbers;
                password += RandomHelper.GetRandomString(numbers, allowedChars);
            }
            if (symbols > 0)
            {
                var allowedChars = excludeAmbiguousChars ? SpecialAmbiguousCharsExcluded : Special;
                password += RandomHelper.GetRandomString(symbols, allowedChars);
            }

            return RandomHelper.Shuffle(password);
        }

   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="minRequiredPasswordLength"></param>
        /// <param name="minRequiredLowerChars"></param>
        /// <param name="minRequiredUpperChars"></param>
        /// <param name="minRequiredDigits"></param>
        /// <param name="minRequiredSpecialChars"></param>
        /// <returns></returns>
        public static PasswordStatus GetPasswordStatus(string password, int minRequiredPasswordLength, int minRequiredLowerChars, int minRequiredUpperChars, int minRequiredDigits, int minRequiredSpecialChars)
        {
            password ??= string.Empty;

            if (password.Length < minRequiredPasswordLength)
                return PasswordStatus.TooShort;
            if (password.Length > 128)
                return PasswordStatus.TooLong;

            if (StringHelper.FindCount(password, Lowercase) < minRequiredLowerChars)
                return PasswordStatus.NeedMoreLowerChars;

            if (StringHelper.FindCount(password, Uppercase) < minRequiredUpperChars)
                return PasswordStatus.NeedMoreUpperChars;

            if (StringHelper.FindCount(password, Numbers) < minRequiredDigits)
                return PasswordStatus.NeedMoreDigitChars;
            
            if (StringHelper.FindCount(password, Special) < minRequiredSpecialChars)
                return PasswordStatus.NeedMoreSpecialChars;

            return PasswordStatus.Success;
        }
    }
}
