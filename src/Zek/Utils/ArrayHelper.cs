namespace Zek.Utils
{
    public static class ArrayHelper
    {
        public static T Coalesce<T>(params T[] args)
        {
            if (args == null)
                return default(T);

            foreach (var arg in args)
            {
                if (arg != null)
                    return arg;
            }

            return default(T);
        }
    }
}
