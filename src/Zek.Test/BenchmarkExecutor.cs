using BenchmarkDotNet.Attributes;
using Zek.Cryptography;
using Zek.Utils;

namespace Zek.Test
{
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
            //HashHelper.Init("test-benchmark-secret-key-123");
        }




        [Params(1_000, 1_000_000)]
        public int Count;

        [Benchmark]
        public void ExecuteUrlShortener1()
        {
            for (var i = 0; i < Count; i++)
            {
                var encoded = UrlShortener.Encode(i);
                var decoded = UrlShortener.Decode(encoded);
            }

        }

        [Benchmark]
        public void ExecuteUrlShortener2()
        {
            for (var i = 0; i < Count; i++)
            {
                //var encoded = UrlShortener2.Encode(i);
                //var decoded = UrlShortener2.Decode(encoded);
            }
        }
    }
}