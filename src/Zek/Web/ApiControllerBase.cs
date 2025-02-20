using Microsoft.AspNetCore.Mvc;
using Zek.Contracts;

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
        public virtual IActionResult Auto(object? value)
        {
            return value != null
                ? new OkObjectResult(value)
                : NotFound();
        }


        [NonAction]
        public virtual IActionResult Auto(IApiResponse obj)
        {
            if (obj.Errors != null && obj.Errors.Count > 0)
            {
                //foreach (var pair in obj.Errors)
                //{
                //    foreach (var error in pair.Value)
                //    {
                //        ModelState.AddModelError(pair.Key, error);
                //    }
                //}
                return BadRequest(obj);
            }

            return new OkObjectResult(obj);
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
