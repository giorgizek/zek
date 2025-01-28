using System.Text;

namespace Zek.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToExceptionString(this Exception ex, string? title = null, string? customerExplanation = null, DateTime? date = null, bool throwException = false)
        {
            try
            {
                if (date == null)
                    date = DateTime.Now;

                var sb = new StringBuilder();

                if (!string.IsNullOrEmpty(customerExplanation))
                    sb.AppendLine("[Customer Explanation]")
                        .AppendLine(customerExplanation)
                        .AppendLine();


                sb.AppendLine("[General Info]")
                    .AppendLine("Date:       \t" + date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"))
                    .AppendLine();

                sb.AppendLine("[Exception Info]");
                if (!string.IsNullOrEmpty(title))
                    sb.AppendLine("Title:      \t" + title);

                AppendExceptionString(ex, sb, string.Empty);

                return sb.ToString();
            }
            catch
            {
                if (throwException)
                    throw;

                return null;
            }
        }

        private static void AppendExceptionString(Exception ex, StringBuilder sb, string indent = "")
        {
            if (indent == null)
                indent = string.Empty;
            else if (indent.Length > 0)
            {
                sb.AppendLine(indent + "[Inner Exception]");
                sb.AppendLine(indent + "Type: " + ex.GetType().FullName);
                sb.AppendLine(indent + "Message: " + ex.Message);
                sb.AppendLine(indent + "Source: " + ex.Source);
                sb.AppendLine(indent + "TargetSite: " + ex.TargetSite);
                sb.AppendLine(indent + "StackTrace: " + ex.StackTrace);
            }

            if (indent.Length == 0)
            {
                sb.AppendLine(indent + "Type:       \t" + ex.GetType().FullName);
                sb.AppendLine(indent + "Message:    \t" + ex.Message);
                sb.AppendLine(indent + "Source:     \t" + ex.Source);
                sb.AppendLine(indent + "TargetSite: \t" + ex.TargetSite);
                sb.AppendLine(indent + "StackTrace: \t" + ex.StackTrace);
            }

            if (ex.InnerException != null)
            {
                sb.AppendLine();
                AppendExceptionString(ex.InnerException, sb, indent + "  ");
            }
        }
    }
}
