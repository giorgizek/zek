using System.Reflection;
using Zek.Extensions;

namespace Zek.Utils
{
    public class ExceptionHelper
    {
        public static void WriteConsole(Exception ex, string? fileName = null)
        {
            ConsoleHelper.WriteException(ex);
            WriteWin(ex, fileName);
        }

        public static void WriteWin(Exception ex, string? fileName = null)
        {
            try
            {
                var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var path = Path.Combine(dir, "Error", DateTime.Today.ToString("yyyy-MM-dd") + (!string.IsNullOrEmpty(fileName) ? " " + fileName.Trim() : string.Empty) + ".txt");
                var isNew = FileHelper.CreateFileIfNotExists(path);

                using (var writer = File.AppendText(path))
                {
                    if (!isNew)
                    {
                        writer.WriteLine();
                        writer.WriteLine();
                    }

                    writer.WriteLine(ex.ToExceptionString());
                    writer.WriteLine("----------------------------");
                }

            }
            catch { }
        }
    }
}
