using BenchmarkDotNet.Running;
using System.Text;
using Zek.Test;

Console.OutputEncoding = Encoding.UTF8;
BenchmarkRunner.Run<BenchmarkExecutor>();
Console.ReadKey();