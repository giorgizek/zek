using System.Text;

namespace Zek.Utils
{
    public class NumberConvertHelper
    {
        private static readonly char[] Abc = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] AbcRu = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Э', 'Ю', 'Я' };
        private static readonly int[] Arabics = { 1, 5, 10, 50, 100, 1000 };
        private static readonly char[] Romans = { 'I', 'V', 'X', 'L', 'C', 'M' };
        private static readonly int[] Subs = { 0, 0, 0, 2, 2, 4 };

        public static string ToAbc(int value)
        {
            if (value < 1)
            {
                return string.Empty;
            }
            var num = 0;
            while (value > 26)
            {
                num++;
                value -= 26;
            }
            return new string(Abc[value - 1], num + 1);
        }

        public static string ToAbcNumeric(int value)
        {
            char ch;
            var num = 0;
            while (value > 26)
            {
                num++;
                value -= 26;
            }
            if (num == 0)
            {
                ch = (char)(value + 64);
                return ch.ToString();
            }
            ch = (char)(value + 64);
            return (ch.ToString() + num.ToString());
        }

        public static string ToAbcRu(int value)
        {
            if (value < 1)
            {
                return string.Empty;
            }
            var num = 0;
            while (value > 26)
            {
                num++;
                value -= 26;
            }
            return new string(AbcRu[value - 1], num + 1);
        }

        public static string ToArabic(int value, bool useEasternDigits) => ToArabic(value.ToString(), useEasternDigits);

        public static string ToArabic(string value, bool useEasternDigits)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < value.Length; i++)
            {
                var num2 = i;
                if ((num2 >= 48) && (num2 <= 57))
                {
                    num2 += 1584;
                    if (useEasternDigits)
                    {
                        num2 += 144;
                    }
                }
                builder.Append((char)num2);
            }
            return builder.ToString();
        }

        public static string ToRoman(int value)
        {
            var builder = new StringBuilder();
            while (value > 0)
            {
                for (var i = 5; i >= 0; i--)
                {
                    if (value >= Arabics[i])
                    {
                        builder.Append(Romans[i]);
                        value -= Arabics[i];
                        continue;
                    }
                    for (var j = Subs[i]; j < i; j++)
                    {
                        if ((Arabics[j] != (Arabics[i] - Arabics[j])) && (value >= (Arabics[i] - Arabics[j])))
                        {
                            builder.Append(Romans[j]);
                            builder.Append(Romans[i]);
                            value -= Arabics[i] - Arabics[j];
                            break;
                        }
                    }
                }
            }
            return builder.ToString();
        }

    }
}
