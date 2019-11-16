using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zek.Utils
{
    public enum PasswordValidatorStatus
    {
        PasswordTooShort,
        PasswordRequiresNonAlphanumeric,
        PasswordRequiresDigit,
        PasswordRequiresLower,
        PasswordRequiresUpper,
        PasswordRequiresUniqueChars
    }


    /// <summary>
    /// Provides an abstraction for validating passwords.
    /// </summary>
    public interface IPasswordValidator
    {
        /// <summary>
        /// Validates a password\.
        /// </summary>
        /// <param name="password">The password supplied for validation</param>
        /// <returns>Error list</returns>
        List<PasswordValidatorStatus> Validate(string password);
    }

    public class PasswordValidator : IPasswordValidator
    {
        public PasswordValidator(IOptions<IdentityOptions> optionsAccessor)
        {
            Options = optionsAccessor?.Value ?? new IdentityOptions();
        }

        public virtual IdentityOptions Options { get; set; }

        /// <summary>
        ///     Ensures that the string is of the required length and meets the configured requirements
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual List<PasswordValidatorStatus> Validate(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            
            var errors = new List<PasswordValidatorStatus>();
            var options = Options.Password;
            if (string.IsNullOrWhiteSpace(password) || password.Length < options.RequiredLength)
            {
                errors.Add(PasswordValidatorStatus.PasswordTooShort);
            }
            if (options.RequireNonAlphanumeric && password.All(IsLetterOrDigit))
            {
                errors.Add(PasswordValidatorStatus.PasswordRequiresNonAlphanumeric);
            }
            if (options.RequireDigit && !password.Any(IsDigit))
            {
                errors.Add(PasswordValidatorStatus.PasswordRequiresDigit);
            }
            if (options.RequireLowercase && !password.Any(IsLower))
            {
                errors.Add(PasswordValidatorStatus.PasswordRequiresLower);
            }
            if (options.RequireUppercase && !password.Any(IsUpper))
            {
                errors.Add(PasswordValidatorStatus.PasswordRequiresUpper);
            }
            if (options.RequiredUniqueChars >= 1 && password.Distinct().Count() < options.RequiredUniqueChars)
            {
                errors.Add(PasswordValidatorStatus.PasswordRequiresUniqueChars);
            }
            return errors;
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is a digit.
        /// </summary>
        /// <param name="c">The character to check if it is a digit.</param>
        /// <returns>True if the character is a digit, otherwise false.</returns>
        public virtual bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is a lower case ASCII letter.
        /// </summary>
        /// <param name="c">The character to check if it is a lower case ASCII letter.</param>
        /// <returns>True if the character is a lower case ASCII letter, otherwise false.</returns>
        public virtual bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is an upper case ASCII letter.
        /// </summary>
        /// <param name="c">The character to check if it is an upper case ASCII letter.</param>
        /// <returns>True if the character is an upper case ASCII letter, otherwise false.</returns>
        public virtual bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        /// <summary>
        /// Returns a flag indicating whether the supplied character is an ASCII letter or digit.
        /// </summary>
        /// <param name="c">The character to check if it is an ASCII letter or digit.</param>
        /// <returns>True if the character is an ASCII letter or digit, otherwise false.</returns>
        public virtual bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}
