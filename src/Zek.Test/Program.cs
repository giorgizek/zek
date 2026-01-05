using BenchmarkDotNet.Running;
using System.Numerics;
using System.Text;
using Zek.Test;
using Zek.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var r = RandomHelper.GetRandom().Next(123456, 99999999);

        var u1 = UrlShortener.Encode(r);
        var id = UrlShortener.Decode("48aUGpDio1S");
        Console.WriteLine(u1);
        Console.WriteLine(id);




        //BenchmarkRunner.Run<BenchmarkExecutor>();
        Console.ReadKey();
    }
}



public static class Base62Encoder
{
    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string Encode(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        byte[] bytes = Encoding.UTF8.GetBytes(input);
        BigInteger value = new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        StringBuilder result = new StringBuilder();

        while (value > 0)
        {
            value = BigInteger.DivRem(value, 62, out var remainder);
            result.Insert(0, Base62Chars[(int)remainder]);
        }

        return result.ToString();
    }

    public static string Decode(string base62)
    {
        if (string.IsNullOrEmpty(base62))
            return string.Empty;

        BigInteger value = BigInteger.Zero;
        foreach (char c in base62)
        {
            int index = Base62Chars.IndexOf(c);
            if (index < 0)
                throw new ArgumentException("Invalid Base62 character: " + c);

            value = value * 62 + index;
        }

        byte[] bytes = value.ToByteArray(isUnsigned: true, isBigEndian: true);
        return Encoding.UTF8.GetString(bytes);
    }
}