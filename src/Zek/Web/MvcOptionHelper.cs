namespace Zek.Web
{
    //todo MvcOptionHelper
    /*public static class MvcOptionHelper
    {
        /// <summary>
        /// A value for the '{0}' property was not provided.
        /// </summary>
        private static string FormatModelBinding_MissingBindRequiredMember(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.ModelBinding_MissingBindRequiredMember, p0);
        }

        /// <summary>
        /// A value is required.
        /// </summary>
        private static string FormatKeyValuePair_BothKeyAndValueMustBePresent()
        {
            return ModelBindingResources.KeyValuePair_BothKeyAndValueMustBePresent;
        }

        /// <summary>
        /// The value '{0}' is invalid.
        /// </summary>
        private static string FormatModelBinding_NullValueNotValid(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.ModelBinding_NullValueNotValid, p0);
        }


        /// <summary>
        /// The value '{0}' is not valid for {1}.
        /// </summary>
        private static string FormatModelState_AttemptedValueIsInvalid(object p0, object p1)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.ModelState_AttemptedValueIsInvalid, p0, p1);
        }

        /// <summary>
        /// The supplied value is invalid for {0}.
        /// </summary>
        private static string FormatModelState_UnknownValueIsInvalid(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.ModelState_UnknownValueIsInvalid, p0);
        }

        /// <summary>
        /// The value '{0}' is invalid.
        /// </summary>
        private static string FormatHtmlGeneration_ValueIsInvalid(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.HtmlGeneration_ValueIsInvalid, p0);
        }


        /// <summary>
        /// The field {0} must be a number.
        /// </summary>
        private static string FormatHtmlGeneration_ValueMustBeNumber(object p0)
        {
            return string.Format(CultureInfo.CurrentCulture, ModelBindingResources.HtmlGeneration_ValueMustBeNumber, p0);
        }



        private static void ConfigModelBindingMessageProvider(ModelBindingMessageProvider provider)
        {
            provider.MissingBindRequiredValueAccessor = FormatModelBinding_MissingBindRequiredMember;
            provider.MissingKeyOrValueAccessor = FormatKeyValuePair_BothKeyAndValueMustBePresent;
            //provider.MissingRequestBodyRequiredValueAccessor = FormatModelBinding_MissingRequestBodyRequiredMember;
            provider.ValueMustNotBeNullAccessor = FormatModelBinding_NullValueNotValid;
            provider.AttemptedValueIsInvalidAccessor = FormatModelState_AttemptedValueIsInvalid;
            provider.UnknownValueIsInvalidAccessor = FormatModelState_UnknownValueIsInvalid;
            provider.ValueIsInvalidAccessor = FormatHtmlGeneration_ValueIsInvalid;
            provider.ValueMustBeANumberAccessor = FormatHtmlGeneration_ValueMustBeNumber;
        }

        public static void Config(MvcOptions options)
        {
            ConfigModelBindingMessageProvider(options.ModelBindingMessageProvider);
        }
    }*/
}
