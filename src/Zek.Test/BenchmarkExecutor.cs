using BenchmarkDotNet.Attributes;
using Zek.Cryptography;
using Zek.Utils;

namespace Zek.Test
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class BenchmarkExecutor
    {
        private record SampleDto(int Id, string Name, string Email);

        private SampleDto _dto = null!;
        private string _dtoHash = null!;
        private string _rawHash = null!;

        [GlobalSetup]
        public void Setup()
        {
            HashHelper.Init("test-benchmark-secret-key-123");
            _dto = new SampleDto(42, "John Doe", "john@example.com");
            _dtoHash = HashHelper.ComputeHash(_dto);
            _rawHash = HashHelper.Hash(42, "John Doe", "john@example.com");
        }

        [Params(1_000, 1_000_000)]
        public int Count;

        //[Benchmark]
        //public void ExecuteUrlShortener1()
        //{
        //    for (var i = 0; i < Count; i++)
        //    {
        //        var encoded = UrlShortener.Encode(i);
        //        var decoded = UrlShortener.Decode(encoded);
        //    }

        //}

        //[Benchmark]
        //public void ExecuteUrlShortener2()
        //{
        //    for (var i = 0; i < Count; i++)
        //    {
        //        //var encoded = UrlShortener2.Encode(i);
        //        //var decoded = UrlShortener2.Decode(encoded);
        //    }
        //}

        [Benchmark]
        public void HashHelper_ComputeHash()
        {
            for (var i = 0; i < Count; i++)
                HashHelper.ComputeHash(_dto);
        }

        [Benchmark]
        public void HashHelper_ComputeHash_WithSalt()
        {
            for (var i = 0; i < Count; i++)
                HashHelper.ComputeHash(_dto, "salt-value");
        }

        [Benchmark]
        public void HashHelper_Verify()
        {
            for (var i = 0; i < Count; i++)
                HashHelper.Verify(_dto, _dtoHash);
        }

        [Benchmark]
        public void HashHelper_Hash()
        {
            for (var i = 0; i < Count; i++)
                HashHelper.Hash(42, "John Doe", "john@example.com");
        }

        [Benchmark]
        public void HashHelper_VerifyHash()
        {
            for (var i = 0; i < Count; i++)
                HashHelper.VerifyHash(_rawHash, 42, "John Doe", "john@example.com");
        }
    }
}