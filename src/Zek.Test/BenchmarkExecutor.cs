using BenchmarkDotNet.Attributes;
using Zek.Utils;

namespace Zek.Test
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class BenchmarkExecutor
    {
        [Params(1_000, 1_000_000)]
        public int Count;

        [Benchmark]
        public void ExecuteShortInt()
        {
            for (var i = 0; i < Count; i++)
            {
                var encoded = ShortInt64.Encode(i);
                var decoded = ShortInt64.Decode(encoded);
            }

        }

        [Benchmark]
        public void ExecuteBase62()
        {
            for (var i = 0; i < Count; i++)
            {
                var encoded = UrlShortener.Encode(i);
                var decoded = UrlShortener.Decode(encoded);

            }

        }


    }


}
