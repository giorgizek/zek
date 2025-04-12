using BenchmarkDotNet.Running;
using System.Security.Cryptography;
using System.Text;
using Zek.Test;
using Zek.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;


        var random = RandomHelper.GetRandom().Next(100000, int.MaxValue);
        Console.WriteLine(random);
        Console.WriteLine();

        var encoded = ShortInt64.Encode(random);
        var decoded = ShortInt64.Decode(encoded);

        Console.WriteLine(encoded);
        Console.WriteLine(decoded);
        Console.WriteLine();



        var encoded2 = UrlShortener.Encode(random);
        var decoded2 = UrlShortener.Decode(encoded2);

        Console.WriteLine(encoded2);
        Console.WriteLine(decoded2);



        var decoded3 = UrlShortener.Decode(encoded);
        Console.WriteLine(decoded3);

        //BenchmarkRunner.Run<BenchmarkExecutor>();






        //var a1 = new DateTime(2025, 1, 10);
        //var a2 = new DateTime(2025, 1, 20);

        //var b1 = new DateTime(2025, 1, 20);
        //var b2 = new DateTime(2025, 1, 30);

        //Console.WriteLine(OverlapHelper.Overlaps(a1, a2, b1, b2));


        Console.ReadKey();
    }
}