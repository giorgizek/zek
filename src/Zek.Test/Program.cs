using BenchmarkDotNet.Running;
using System.Text;
using Zek.Test;
using Zek.Utils;
using Zek.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;


        const string key = "SuperSecretKey_DoNotShare_12345";


        HashHelper.Init(key);

        var originalOrder = new OrderDto
        {
            OrderId = 101,
            Product = "Gaming Laptop",
            Price = 1500.00m
        };

      

        BenchmarkRunner.Run<BenchmarkExecutor>();
        //BenchmarkRunner.Run<EnumParserBenchmark>();






        //var a1 = new DateTime(2025, 1, 10);
        //var a2 = new DateTime(2025, 1, 20);

        //var b1 = new DateTime(2025, 1, 20);
        //var b2 = new DateTime(2025, 1, 30);

        //Console.WriteLine(OverlapHelper.Overlaps(a1, a2, b1, b2));


        Console.ReadKey();
    }
}

// 2. THE DTO
public class OrderDto
{
    public int OrderId { get; set; }
    public string Product { get; set; }
    public decimal Price { get; set; }
}