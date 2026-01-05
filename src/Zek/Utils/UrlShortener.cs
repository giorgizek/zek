namespace Zek.Utils
{
    public static class UrlShortener
    {
        private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        // Fast lookup for decoding (maps ASCII value to index)
        private static readonly int[] LookupTable;

        static UrlShortener()
        {
            // Initialize lookup table for O(1) decoding
            LookupTable = new int[128];
            Array.Fill(LookupTable, -1);
            for (int i = 0; i < Base62Chars.Length; i++)
            {
                LookupTable[Base62Chars[i]] = i;
            }
        }


        public static string Encode(long id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Number must be non-negative.");

            if (id == 0) return "0";

            // Stackalloc is fast and avoids heap allocation. 
            // 13 chars is enough for long.MaxValue in Base62.
            Span<char> buffer = stackalloc char[13];
            int index = buffer.Length;

            while (id > 0)
            {
                // Fill buffer backwards
                buffer[--index] = Base62Chars[(int)(id % 62)];
                id /= 62;
            }

            return new string(buffer[index..]);
        }


        public static long Decode(string token)
        {
            // 1. Fail fast on empty input
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Code cannot be null or empty.", nameof(token));

            long result = 0;
            // 2. Use a Loop for performance
            foreach (char c in token)
            {
                // 3. Bounds check using the array length (Safe against Unicode)
                if (c >= LookupTable.Length || LookupTable[c] == -1)
                    throw new ArgumentException($"Invalid character: {c}");

                // 4. Overflow protection (Critical)
                try
                {
                    result = checked(result * 62 + LookupTable[c]);
                }
                catch (OverflowException)
                {
                    throw new FormatException("Input code is too long to be a valid ID.");
                }

            }
            return result;
        }
    }
}
