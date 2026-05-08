using BenchmarkDotNet.Running;
using System.Numerics;
using System.Text;
using Zek.Contracts;
using Zek.Test;
using Zek.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;






        BenchmarkRunner.Run<BenchmarkExecutor>();
        Console.ReadKey();
    }
}