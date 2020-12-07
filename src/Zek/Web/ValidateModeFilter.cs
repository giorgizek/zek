using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Zek.Web
{
    public class ValidateModeFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            if (!context.ModelState.IsValid)
            {
                context.Result =  new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                await next();
            }

            //var result = await next();
            // execute any code after the action executes
        }
    }
}