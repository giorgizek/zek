namespace Zek.Extensions
{
    public static class AsyncExtensions
    {
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


        /// <summary>
        /// Use this for API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static async Task<T> WithNoContext<T>(this Task<T> task)
        {
            return await task.ConfigureAwait(false);
        }

        /// <summary>
        /// use this for API
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public static async Task WithNoContext(this Task task)
        {
            await task.ConfigureAwait(false);
        }
    }
}
