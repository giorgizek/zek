using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Zek.Cryptography;
using Zek.Utils;

namespace Zek.Test
{
    public static class HashHelper2
    {
        const string key = "SuperSecretKey_DoNotShare_12345";
        public static string Hash(params object?[] values)
        {
            var joined = StringHelper.Join("_", StringHelper.Join("_", values), key);
            return KnuthHelper.KnuthHex(joined);
        }
        public static bool VerifyHash(string hash, params object[] values)
        {
            return hash == Hash(values);
        }
    }


    [MemoryDiagnoser]
    [ShortRunJob]
    public class BenchmarkExecutor
    {
        [GlobalSetup]
        public void Setup()
        {
            // Pass any string here. 
            // Since it's a benchmark, the actual value doesn't matter, 
            // as long as it's consistent across runs.
            HashHelper.Init("test-benchmark-secret-key-123");
        }


        private static OrderDto originalOrder = new()
        {
            OrderId = 101,
            Product = "Gaming Laptop",
            Price = 1500.00m
        };

        [Params(1_000, 1_000_000)]
        public int Count;

        //[Benchmark]
        //public void ExecuteShortInt()
        //{
        //    for (var i = 0; i < Count; i++)
        //    {
        //        //var encoded = ShortInt64.Encode(i);
        //        //var decoded = ShortInt64.Decode(encoded);
        //    }

        //}

        //[Benchmark]
        //public void ExecuteBase62()
        //{
        //    for (var i = 0; i < Count; i++)
        //    {
        //        var encoded = UrlShortener.Encode(i);
        //        var decoded = UrlShortener.Decode(encoded);

        //    }

        //}
        [Benchmark]
        public void ExecuteHashHelper()
        {
            for (var i = 0; i < Count; i++)
            {
                var signature = HashHelper.ComputeHash(originalOrder);

                bool isValid = HashHelper.Verify(originalOrder, signature);

            }

        }



        [Benchmark]
        public void ExecuteHashHelper2()
        {
            for (var i = 0; i < Count; i++)
            {
                var signature = HashHelper2.Hash(originalOrder.OrderId, originalOrder.Product);

                bool isValid = HashHelper2.VerifyHash(signature, originalOrder.OrderId, originalOrder.Product);

            }

        }
    }


}
