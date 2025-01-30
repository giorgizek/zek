namespace Zek.Services
{
    public class BaseSmsSender
    {
        public static string ParseMobile(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
                return mobile;

            var result = new string(mobile.ToCharArray().Where(char.IsDigit).ToArray());

            //foreach (var c in mobile)
            //    if (char.IsDigit(c)) result += c;

            if (result.StartsWith("995") && result.Length == 11)
                result = result.Insert(3, "5");
            else if (result.StartsWith("8") && result.Length == 9)
                result = "9955" + result.Substring(1);
            else if (result.StartsWith("5") && result.Length == 9)
                result = "995" + result;
            return result;
        }
    }
}
