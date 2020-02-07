using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zek.Extensions;
using Zek.Extensions.Security.Claims;
using Zek.Model;

namespace Zek.Web
{
    public class BaseController : Controller
    {
        //private int? _userId;
        //protected virtual int UserId
        //{
        //    get
        //    {
        //        if (_userId == null)
        //            _userId = User.GetUserId().ToInt32();
        //        return _userId.Value;
        //    }
        //    set => _userId = value;
        //}

        //private string[] _roles;
        //public string[] Roles
        //{
        //    get => _roles ??= User.GetRoles().ToArray();
        //    set => _roles = value;
        //}


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
            switch (modelStateResult.StatusCode)
            {
                case Model.StatusCode.BadRequest:
                    return new BadRequestObjectResult(modelStateResult);

                case Model.StatusCode.Forbidden:
                    return new ForbidResult();

                case Model.StatusCode.NotFound:
                    return new NotFoundObjectResult(modelStateResult);

                case Model.StatusCode.InternalServerError:
                    return new ObjectResult(modelStateResult)
                    {
                        StatusCode = modelStateResult.StatusCode.ToInt32()
                    };

                default:
                    return new OkResult();
            }
        }

        [NonAction]
        public virtual IActionResult Auto<T>(ModelStateResult<T> modelStateResult)
        {
            switch (modelStateResult.StatusCode)
            {
                case Model.StatusCode.BadRequest:
                    return new BadRequestObjectResult(modelStateResult.Errors);

                case Model.StatusCode.Forbidden:
                    return new ForbidResult();

                case Model.StatusCode.NotFound:
                    return new NotFoundObjectResult(modelStateResult.Errors);

                case Model.StatusCode.InternalServerError:
                    return new ObjectResult(modelStateResult.Errors)
                    {
                        StatusCode = modelStateResult.StatusCode.ToInt32()
                    };

                default:
                    return new OkObjectResult(modelStateResult.Value);
            }
        }
    }
}
