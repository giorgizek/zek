using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Zek.Extensions;

namespace Zek.Web
{
    public static class UserManagerExtensions
    {
        public static int GetUserIdInt32<TUser>(this UserManager<TUser> userManager, ClaimsPrincipal user) where TUser : class
        {
            return userManager.GetUserId(user).ToInt32();
        }
    }
}
