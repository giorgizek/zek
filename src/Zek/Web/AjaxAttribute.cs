using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;

namespace Zek.Web
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxAttribute : ActionMethodSelectorAttribute
    {
        public AjaxAttribute(bool ajax = true) { Ajax = ajax; }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return Ajax == routeContext.HttpContext.Request.IsAjaxRequest();
        }

        public bool Ajax { get; set; }
    }
}
