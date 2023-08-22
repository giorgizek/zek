//using Microsoft.AspNetCore.Mvc;
//using Zek.Model.ViewModel;

//namespace Zek.Web
//{
//    public class MvcBaseController : BaseController
//    {
//        public string Title
//        {
//            get => ViewData[nameof(Title)]?.ToString();
//            set => ViewData[nameof(Title)] = value;
//        }
//        public string ReturnUrl { set => ViewData[nameof(ReturnUrl)] = value; }


//        [NonAction]
//        protected IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }

//        [NonAction]
//        protected IActionResult Error()
//        {
//            return View(nameof(Error));
//        }


//        [NonAction]
//        protected IActionResult Alert(AlertViewModel model)
//        {
//            if (!string.IsNullOrEmpty(model.PageTitle))
//                Title = model.PageTitle;

//            return View(nameof(Alert), model);
//        }

//        protected IActionResult DisplayTempalate<TModel>(TModel model)
//        {
//            var viewName = "DisplayTemplates" + typeof(TModel).Name;
//            return PartialView(viewName, model);
//        }
//        protected IActionResult EditorTemplate<TModel>(TModel model)
//        {
//            var viewName = "EditorTemplates" + typeof(TModel).Name;
//            return PartialView(viewName, model);
//        }
//    }


//}
