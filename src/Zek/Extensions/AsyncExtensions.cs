namespace Zek.Extensions
{
    public static class AsyncExtensions
    {
        [Obsolete("use native methods")]
        public static async Task<bool> Invert(this Task<bool> task)
        {
            return !await task;
        }


        public static void RunSync(this Task task)
        {
            task.GetAwaiter().GetResult();
        }
        public static TResult RunSync<TResult>(this Task<TResult> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}
