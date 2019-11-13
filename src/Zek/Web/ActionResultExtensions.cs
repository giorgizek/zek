using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Zek.Web
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(object obj)
        {
            if (obj == null)
                return new NotFoundResult();

            return new OkObjectResult(obj);
        }
    }
}
