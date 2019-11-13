using Microsoft.AspNetCore.Mvc;

namespace Zek.Web
{
    public static class ControllerExtensions
    {
        public static string GetBaseUrl(this Controller controller)
        {
            return controller.Request.GetBaseUrl();
        }
       
    }
}
