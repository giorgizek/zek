using Microsoft.AspNetCore.Mvc;

namespace Zek.Web
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(object? obj)
        {
            if (obj == null)
                return new NotFoundResult();

            return new OkObjectResult(obj);
        }
    }
}
