using BenchmarkDotNet.Attributes;
using Zek.Utils;

namespace Zek.Test
{

    public enum EndUserTransactionType
    {
        Withdrawal = 1,
        Deposit = 2,
        Chargeback = 3,
        Play = 4,
        Win = 5,
        Loss = 6,
        Bonus = 7,
    }

    [MemoryDiagnoser]
    [ShortRunJob]
    public class EnumParserBenchmark
    {


        private string input = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15"; // Example input
        private char[] separator = { ',' }; // Default separator
        [Benchmark]
        public EndUserTransactionType[]? ToEnumArrayBenchmark()
        {
            return EnumHelper.ParseEnumArray<EndUserTransactionType>(input, separator);
        }

        [Benchmark]
        public List<EndUserTransactionType>? ParseEnumListBenchmark()
        {
            return EnumHelper.ParseEnumList<EndUserTransactionType>(input, separator);
        }
    
    }

}
