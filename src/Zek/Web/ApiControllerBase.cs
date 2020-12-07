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
                Model.StatusCode.BadRequest => new BadRequestObjectResult(modelStateResult.Errors),
                Model.StatusCode.Forbidden => new ForbidResult(),
                Model.StatusCode.NotFound => new NotFoundObjectResult(modelStateResult.Errors),
                Model.StatusCode.InternalServerError => new ObjectResult(modelStateResult.Errors)
                {
                    StatusCode = (int)modelStateResult.StatusCode
                },
                _ => new OkResult()
            };
        }

        [NonAction]
        public virtual IActionResult Auto<T>(ModelStateResult<T> modelStateResult)
        {
            return modelStateResult.StatusCode switch
            {
                Model.StatusCode.BadRequest => new BadRequestObjectResult(modelStateResult.Errors),
                Model.StatusCode.Forbidden => new ForbidResult(),
                Model.StatusCode.NotFound => new NotFoundObjectResult(modelStateResult.Errors),
                Model.StatusCode.InternalServerError => new ObjectResult(modelStateResult.Errors)
                {
                    StatusCode = (int)modelStateResult.StatusCode
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



    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class ApiControllerBase<TService> : ApiControllerBase
        where TService : IDisposable
    {
        public ApiControllerBase(TService service)
        {
            Service = service;
        }
        protected TService Service { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Service?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class ApiControllerBase<TService1, TService2> : ApiControllerBase
        where TService1 : IDisposable
        where TService2 : IDisposable
    {
        public ApiControllerBase(TService1 service1, TService2 service2)
        {
            Service1 = service1;
            Service2 = service2;
        }

        protected TService1 Service1 { get; set; }
        protected TService2 Service2 { get; set; }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Service1?.Dispose();
                Service2?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}