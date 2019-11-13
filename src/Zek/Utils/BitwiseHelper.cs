namespace Zek.Utils
{
    public static class BitwiseHelper
    {
        public static bool HasFlag(int flags, int flagToCheck)
        {
            return (flags & flagToCheck) == flagToCheck;
        }

     
        public static int AddFlags(int flags, params int[] flagsToAdd)
        {
            if (flagsToAdd == null) return flags;
            foreach (var flag in flagsToAdd)
            {
                flags |= flag;
            }

            return flags;
        }

    
        public static int DeleteFlags(int flags, params int[] flagsToDelete)
        {
            if (flagsToDelete == null) return flags;
            foreach (var flag in flagsToDelete)
            {
                flags &= ~flag;
            }

            return flags;
        }

        public static int ToggleFlag(int flags, int flagToToggle)
        {
            flags ^= flagToToggle;
            return flags;
        }


        public static int MinFlag(int value)
        {
            var result = 1;
            while ((result & value) == 0 && result != 0)
                result <<= 1;
            return result;
        }

        public static int MaxFlag(int value)
        {
            var result = (int.MaxValue >> 1) + 1; // because C2
            while ((result & value) == 0 && result != 0)
                result >>= 1;
            return result;
        }

        public static long MinFlag(long value)
        {
            var result = 1;
            while ((result & value) == 0 && result != 0)
                result <<= 1;
            return result;
        }

        public static long MaxFlag(long value)
        {
            var result = (int.MaxValue >> 1) + 1; // because C2
            while ((result & value) == 0 && result != 0)
                result >>= 1;
            return result;
        }
    }
}
