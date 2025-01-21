using System.Text.RegularExpressions;
using System.Web;

namespace Zek.Web
{
    public static class HtmlHelper
    {
        public static string? ToPlainText(string? html)
        {
            if (string.IsNullOrEmpty(html))//<.*?>
                return html;
            return HttpUtility.HtmlDecode(Regex.Replace(html, @"<(.|\n)*?>", string.Empty));
        }
    }
    /* public static class HtmlHelperExtensions
     {

         //        /// <summary>
         //        /// Return a view path based on an action name and controller name
         //        /// </summary>
         //        /// <param name="html">Context for extension method</param>
         //        /// <param name="action">Action name</param>
         //        /// <param name="controller">Controller name</param>
         //        /// <returns>A string in the form "~/views/{controller}/{action}.cshtml</returns>
         //        public static string View(this IHtmlHelper html, string action, string controller) => $"~/Views/{controller}/{action}.cshtml";

         //        /// <summary>
         //        /// Return a view path based on an action name and controller name
         //        /// </summary>
         //        /// <param name="html">Context for extension method</param>
         //        /// <param name="action">Action name</param>
         //        /// <param name="controller">Controller name</param>
         //        /// <returns>A string in the form "~/views/{controller}/{action}.cshtml</returns>
         //        public static string View(this UrlHelper html, string action, string controller) => $"~/Views/{controller}/{action}.cshtml";


         /// <summary>
         /// Return Partial View.
         /// The element naming convention is maintained in the partial view by setting the prefix name from the expression.
         /// The name of the view (by default) is the class name of the Property or a UIHint("partial name").
         /// @Html.PartialFor(m => m.Address)  - partial view name is the class name of the Address property.
         /// </summary>
         /// <param name="html"></param>
         /// <param name="expression">Model expression for the prefix name (m => m.Address)</param>
         /// <returns>Partial View as Mvc string</returns>
         public static IHtmlContent PartialFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
         {
             return html.PartialFor(expression, null);
         }

         /// <summary>
         /// Return Partial View.
         /// The element naming convention is maintained in the partial view by setting the prefix name from the expression.
         /// </summary>
         /// <param name="html"></param>
         /// <param name="expression">Model expression for the prefix name (m => m.Group[2])</param>
         /// <param name="partialName">Partial View Name</param>
         /// <returns>Partial View as Mvc string</returns>
         public static IHtmlContent PartialFor<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string partialName)
         {
             var name = ExpressionHelper.GetExpressionText(expression);
             var modelName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
             var metaData = ExpressionMetadataProvider.FromLambdaExpression(expression, html.ViewData, html.MetadataProvider);
             //ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
             var model = metaData.Model;
             if (partialName == null)
             {
                 partialName = metaData.Metadata.TemplateHint ?? typeof(TProperty).Name;
             }

             // Use a ViewData copy with a new TemplateInfo with the prefix set
             var viewData = new ViewDataDictionary(html.ViewData);
             viewData.TemplateInfo.HtmlFieldPrefix = modelName;

             // Call standard MVC Partial
             return html.Partial(partialName, model, viewData);
         }




         //public static Task<IHtmlContent> ValidationSummaryPartialAsync<TModel>(this IHtmlHelper<TModel> html) => html.PartialAsync("_ValidationSummary");
         //public static Task RenderFilterPartialAsync<TModel>(this IHtmlHelper<TModel> html, object model) => html.RenderPartialAsync("_Filter", model);
         //public static Task RenderGridPartialAsync<TModel>(this IHtmlHelper<TModel> html, object model) => html.RenderPartialAsync("_Grid", model);
         //public static Task RenderValidationSummaryPartialAsync<TModel>(this IHtmlHelper<TModel> html) => html.RenderPartialAsync("_ValidationSummary");
         //public static Task RenderValidationScriptsPartialAsync<TModel>(this IHtmlHelper<TModel> html) => html.RenderPartialAsync("_ValidationScripts");
         //public static Task RenderEditFormToolbarFooterPartialAsync<TModel>(this IHtmlHelper<TModel> html) => html.RenderPartialAsync("_EditFormToolbarFooter");


     }*/
}
