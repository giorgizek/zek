using System;
using System.Collections.Generic;
using System.Text;
using Zek.Extensions;

namespace Zek.Utils
{
    public class ConsoleHelper
    {
        public static void WriteException(Exception ex) => WriteException(ex.ToExceptionString());

        public static void WriteException(string exception)
        {
            Console.WriteLine();
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(exception);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine();
        }
    }
}
