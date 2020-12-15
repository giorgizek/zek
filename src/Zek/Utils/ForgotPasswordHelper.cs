using System;

namespace Zek.Utils
{
    public static class ForgotPasswordHelper
    {
        public static string GenerateId(string email)
        {
            return new PasswordHasher().HashPassword($"{email}{Guid.NewGuid():N}");
        }
        //public static string GenerateId(string userName, string email)
        //{
        //    return new PasswordHasher().HashPassword($"{userName}{email}{Guid.NewGuid():N}");
        //}
    }
}
