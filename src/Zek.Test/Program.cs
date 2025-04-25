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

        string input = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15"; // Example input

        var z = EnumHelper.ParseEnumArray<EndUserTransactionType>(input);


        //BenchmarkRunner.Run<BenchmarkExecutor>();
        //BenchmarkRunner.Run<EnumParserBenchmark>();






        //var a1 = new DateTime(2025, 1, 10);
        //var a2 = new DateTime(2025, 1, 20);

        //var b1 = new DateTime(2025, 1, 20);
        //var b2 = new DateTime(2025, 1, 30);

        //Console.WriteLine(OverlapHelper.Overlaps(a1, a2, b1, b2));


        Console.ReadKey();
    }
}