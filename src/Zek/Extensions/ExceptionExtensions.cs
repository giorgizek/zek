using System.Text;

namespace Zek.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToExceptionString(this Exception? ex, string? title = null, string? customerExplanation = null, DateTime? date = null, bool throwException = false)
        {

            if (ex is null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            var timestamp = date ?? DateTime.UtcNow;

            // 1. Add optional user context
            if (!string.IsNullOrWhiteSpace(customerExplanation))
            {
                sb.AppendLine("[Customer Explanation]")
                  .AppendLine(customerExplanation)
                  .AppendLine();
            }

            // 2. Add general information
            sb.AppendLine("[General Info]")
              .AppendLine($"Date (UTC): \t{timestamp:yyyy-MM-dd HH:mm:ss.fff}")
              .AppendLine();

            // 3. Add exception details iteratively
            sb.AppendLine("[Exception Info]");
            if (!string.IsNullOrWhiteSpace(title))
            {
                sb.AppendLine($"Title: \t\t{title}");
            }

            var currentException = ex;
            var indentLevel = 0;
            const string indent = "  ";

            while (currentException != null)
            {
                var currentIndent = new string(' ', indentLevel * indent.Length);

                if (indentLevel > 0)
                {
                    sb.AppendLine()
                      .AppendLine($"{currentIndent}[Inner Exception]");
                }

                sb.AppendLine($"{currentIndent}Type: \t\t{currentException.GetType().FullName}");
                sb.AppendLine($"{currentIndent}Message: \t{currentException.Message}");
                sb.AppendLine($"{currentIndent}Source: \t{currentException.Source}");
                sb.AppendLine($"{currentIndent}TargetSite: \t{currentException.TargetSite}");

                if (!string.IsNullOrWhiteSpace(currentException.StackTrace))
                {
                    sb.AppendLine($"{currentIndent}StackTrace:")
                      .AppendLine(currentException.StackTrace.Trim());
                }

                currentException = currentException.InnerException;
                indentLevel++;
            }

            return sb.ToString();
        }
    }
}
