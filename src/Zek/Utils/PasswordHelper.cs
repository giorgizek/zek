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
        /// <param name="excludeSimilarCharacters">(e.g. i, l, o, 1, 0, I)</param>
        /// <param name="excludeAmbiguousCharacters">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns>Random Password</returns>
        public static string Generate(int minLength, int maxLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarCharacters = true, bool excludeAmbiguousCharacters = true)
        {
            var passwordLength = maxLength;

            if (minLength != maxLength)
                passwordLength = RandomHelper.GetRandom().Next(minLength, maxLength);
            return Generate(passwordLength, includeLetters, includeMixedCase, includeNumbers, includeSymbols, excludeSimilarCharacters);
        }

        public const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string UppercaseSimilarCharactersExcluded = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        public const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        public const string LowercaseSimilarCharactersExcluded = "abcdefghijklmnopqrstuvwxyz";

        public const string Numbers = "0123456789";
        public const string NumbersSimilarCharactersExcluded = "23456789";
        public const string SpecialChars = "*$-+?_=%{}/";


        /// <summary>
        /// Generate Random Password
        /// </summary>
        /// <param name="passwordLength">Password Length</param>
        /// <param name="includeLetters">(e.g. abcdef)</param>
        /// <param name="includeMixedCase">(e.g. AbcDEf)</param>
        /// <param name="includeNumbers">(e.g. a9b8c7d)</param>
        /// <param name="includeSymbols">(e.g. a!b*c_d)</param>
        /// <param name="excludeSimilarCharacters">(e.g. i, l, o, 1, 0, I)</param>
        /// <param name="excludeAmbiguousCharacters">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns>Random Password</returns>
        public static string Generate(int passwordLength, bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarCharacters = true, bool excludeAmbiguousCharacters = true)
        {
            return RandomHelper.GetRandomString(passwordLength, GetAllowedChars(includeLetters, includeMixedCase, includeNumbers, includeSymbols, excludeSimilarCharacters, excludeAmbiguousCharacters));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeLetters"></param>
        /// <param name="includeMixedCase"></param>
        /// <param name="includeNumbers"></param>
        /// <param name="includeSymbols"></param>
        /// <param name="excludeSimilarCharacters"></param>
        /// <param name="excludeAmbiguousCharacters">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns></returns>
        public static string GetAllowedChars(bool includeLetters, bool includeMixedCase, bool includeNumbers, bool includeSymbols, bool excludeSimilarCharacters = true, bool excludeAmbiguousCharacters = true)
        {
            var allowedChars = includeLetters ? (excludeSimilarCharacters ? "abcdefghijkmnpqrstuvwxyz" : "abcdefghijklmnopqrstuvwxyz") : string.Empty;
            allowedChars += includeMixedCase ? (excludeSimilarCharacters ? "ABCDEFGHJKLMNPQRSTUVWXYZ" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ") : string.Empty;
            allowedChars += includeNumbers ? (excludeSimilarCharacters ? NumbersSimilarCharactersExcluded : "0123456789") : string.Empty;
            if (includeSymbols)
            {
                allowedChars += excludeSimilarCharacters
                    ? (excludeAmbiguousCharacters
                        ? SymbolsWithoutAmbiguousCharacters
                        : SpecialChars)
                    : (excludeAmbiguousCharacters
                        ? @"!#$%&*+-=?@^_|"
                        : SpecialChars);
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
        /// <param name="excludeSimilarCharacters">Exclude: { } [ ] ( ) / \ ' " ` ~ , ; : . &lt; &gt; </param>
        /// <returns></returns>
        public static string Generate(int uppercaseLetters, int lowercaseLetters, int numbers, int symbols, bool excludeSimilarCharacters = true)
        {
            var password = string.Empty;
            if (uppercaseLetters > 0)
            {
                var allowedChars = excludeSimilarCharacters ? "ABCDEFGHJKLMNPQRSTUVWXYZ" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                password += RandomHelper.GetRandomString(uppercaseLetters, allowedChars);
            }
            if (lowercaseLetters > 0)
            {
                var allowedChars = excludeSimilarCharacters ? "abcdefghijkmnpqrstuvwxyz" : "abcdefghijklmnopqrstuvwxyz";
                password += RandomHelper.GetRandomString(lowercaseLetters, allowedChars);
            }
            if (numbers > 0)
            {
                var allowedChars = excludeSimilarCharacters ? NumbersSimilarCharactersExcluded : Numbers;
                password += RandomHelper.GetRandomString(numbers, allowedChars);
            }
            if (symbols > 0)
            {
                var allowedChars = SpecialChars;
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

            if (StringHelper.FindCount(password, "abcdefghijklmnopqrstuvwxyz") < minRequiredLowerChars)
                return PasswordStatus.NeedMoreLowerChars;

            if (StringHelper.FindCount(password, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") < minRequiredUpperChars)
                return PasswordStatus.NeedMoreUpperChars;

            if (StringHelper.FindCount(password, "0123456789") < minRequiredDigits)
                return PasswordStatus.NeedMoreDigitChars;

            if (StringHelper.FindCount(password, @"`-=\~!@#$%^&*()_+|[]{};':"",./<>?") < minRequiredSpecialChars)
                return PasswordStatus.NeedMoreDigitChars;

            return PasswordStatus.Success;
        }
    }
}
