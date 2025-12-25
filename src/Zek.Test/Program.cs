using System.Text;
using Zek.Test;
using Zek.Utils;
using Zek.Web;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;



        // --- STEP 1: INITIALIZATION ---
        // Ideally done in Startup.cs or Program.cs
        Console.WriteLine("--- System Startup ---");
        HashHelper.Init("SuperSecretKey_DoNotShare_12345");
        Console.WriteLine("Secret key initialized.\n");


        // --- STEP 2: THE SENDER ---
        Console.WriteLine("--- Sender ---");
        var originalOrder = new OrderDto
        {
            OrderId = 101,
            Product = "Gaming Laptop",
            Price = 1500.00m
        };

        // Generate the signature/hash
        string signature = HashHelper.ComputeHash(originalOrder);

        Console.WriteLine($"Sending Order: {originalOrder.Product} for ${originalOrder.Price}");
        Console.WriteLine($"Generated Signature: {signature}\n");


        // --- STEP 3: THE RECEIVER (Valid Scenario) ---
        Console.WriteLine("--- Receiver (Valid Check) ---");

        // Receiver gets the object and the signature from the API/Message Queue
        bool isValid = HashHelper.Verify(originalOrder, signature);

        if (isValid)
            Console.WriteLine("SUCCESS: Signature matches. Order is authentic.");
        else
            Console.WriteLine("ERROR: Signature mismatch!");


        // --- STEP 4: THE HACKER (Tampering Scenario) ---
        Console.WriteLine("\n--- Receiver (Tampered Data Check) ---");

        // Simulating a Man-in-the-Middle attack where data is modified
        originalOrder.Price = 5.00m; // Hacker changes price from 1500 to 5

        Console.WriteLine($"Hacker changed price to: ${originalOrder.Price}");

        // The signature is still the original one!
        bool isTamperedValid = HashHelper.Verify(originalOrder, signature);

        if (isTamperedValid)
            Console.WriteLine("CRITICAL FAIL: Tampered data was accepted!");
        else
            Console.WriteLine("SUCCESS: Tampering detected. Signature rejected.");

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

// 2. THE DTO
public class OrderDto
{
    public int OrderId { get; set; }
    public string Product { get; set; }
    public decimal Price { get; set; }
}