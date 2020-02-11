using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Zek.Extensions;
using Zek.Model;

namespace Zek.Web
{
    public class ApiControllerBase : ControllerBase, IDisposable
    {
        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> or <see cref="NotFoundResult"/> object that produces an <see cref="StatusCodes.Status200OK"/> or <see cref="StatusCodes.Status404NotFound"/> response.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>The created <see cref="OkObjectResult"/> or <see cref="NotFoundResult"/> for the response.</returns>
        [NonAction]
        public virtual IActionResult Auto(object value)
        {
            return value != null
                ? new OkObjectResult(value)
                : (IActionResult)NotFound();
        }


        [NonAction]
        public virtual IActionResult Auto(ModelStateResult modelStateResult)
        {
            return modelStateResult.StatusCode switch
            {
                Model.StatusCode.BadRequest => (IActionResult) new BadRequestObjectResult(modelStateResult),
                Model.StatusCode.Forbidden => new ForbidResult(),
                Model.StatusCode.NotFound => new NotFoundObjectResult(modelStateResult),
                Model.StatusCode.InternalServerError => new ObjectResult(modelStateResult)
                {
                    StatusCode = modelStateResult.StatusCode.ToInt32()
                },
                _ => new OkResult()
            };
        }

        [NonAction]
        public virtual IActionResult Auto<T>(ModelStateResult<T> modelStateResult)
        {
            return modelStateResult.StatusCode switch
            {
                Model.StatusCode.BadRequest => (IActionResult) new BadRequestObjectResult(modelStateResult.Errors),
                Model.StatusCode.Forbidden => new ForbidResult(),
                Model.StatusCode.NotFound => new NotFoundObjectResult(modelStateResult.Errors),
                Model.StatusCode.InternalServerError => new ObjectResult(modelStateResult.Errors)
                {
                    StatusCode = modelStateResult.StatusCode.ToInt32()
                },
                _ => new OkObjectResult(modelStateResult.Value)
            };
        }


        /// <inheritdoc />
        public void Dispose() => Dispose(disposing: true);

        /// <summary>
        /// Releases all resources currently used by this <see cref="Controller"/> instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if this method is being invoked by the <see cref="Dispose()"/> method,
        /// otherwise <c>false</c>.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}