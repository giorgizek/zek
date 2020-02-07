using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Zek.Extensions;

namespace Zek.Model
{
    public static class ModelStateResultExtensions
    {
        //private static void AddToModelState(this ModelStateResult saveModelState, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary viewState)
        //{
        //    if (saveModelState == null)
        //        throw new ArgumentNullException(nameof(saveModelState));
        //    if (viewState == null)
        //        throw new ArgumentNullException(nameof(viewState));

        //    foreach (var item in saveModelState)
        //    {
        //        foreach (var error in item.Value.Errors)
        //        {
        //            viewState.AddModelError(item.Key, error.ErrorMessage);
        //        }
        //    }
        //}

        //private static object ToModelState(this ModelStateResult saveModelState)
        //{
        //    var errors = new Hashtable();
        //    foreach (var pair in saveModelState)
        //    {
        //        if (pair.Value.Errors.Count > 0)
        //        {
        //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //        }
        //    }

        //    return errors;
        //    var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
        //    saveModelState.AddToModelState(modelState);
        //    return modelState;
        //}

        //public static IActionResult ToActionResult(this ModelStateResult modelStateResult, object content = null)
        //{
        //    switch (modelStateResult.StatusCode)
        //    {
        //        case StatusCode.BadRequest:
        //            return new BadRequestObjectResult(modelStateResult);

        //        case StatusCode.Forbidden:
        //            return new ForbidResult();

        //        case StatusCode.NotFound:
        //            return new NotFoundObjectResult(modelStateResult);

        //        case StatusCode.InternalServerError:
        //            return new ObjectResult(modelStateResult) { StatusCode = modelStateResult.StatusCode.ToInt32() };

        //        //case StatusCode.OK:
        //        default:
        //            return content != null
        //                ? new OkObjectResult(content)
        //                : (IActionResult)new OkResult();
        //    }
        //}
        //public static IActionResult ToActionResult<TModel>(this ModelStateResult<TModel> modelStateResult)
        //{
        //    switch (modelStateResult.StatusCode)
        //    {
        //        case StatusCode.BadRequest:
        //            return new BadRequestObjectResult(modelStateResult);

        //        case StatusCode.Forbidden:
        //            return new ForbidResult();

        //        case StatusCode.NotFound:
        //            return new NotFoundObjectResult(modelStateResult);

        //        case StatusCode.InternalServerError:
        //            return new ObjectResult(modelStateResult) { StatusCode = modelStateResult.StatusCode.ToInt32() };

        //        //case StatusCode.OK:
        //        default:
        //            return new OkObjectResult(modelStateResult.Value);
        //    }
        //}
    }
}