using System.Collections.Concurrent;

namespace Zek.Utils
{
    /// <summary>
    /// Synchronization helper: a static lock collection associated with a key.
    /// NamedLock manages the lifetime of critical sections that can be accessed by a key (name) throughout the application. 
    /// It also have some helper methods to allow a maximum wait time (timeout) to aquire the lock and safelly release it.    
    /// Note: this nuget package contains c# source code and depends on System.Collections.Concurrent introduced in .Net 4.0.
    /// </summary>
    /// <example>
    /// // create a lock for this key
    /// using (var padlock = new NamedLock (key))
    /// {
    ///     if (padlock.Enter (TimeSpan.FromMilliseconds (100)))
    ///     {
    ///         // do something as we now own the lock
    ///     }
    ///     else
    ///     {
    ///         // do some other thing since we could not aquire the lock
    ///     }
    /// }
    /// </example>
    public class NamedLock : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NamedLock" /> class.
        /// </summary>
        /// <param name="key">The named lock key.</param>
        public NamedLock(string key)
        {
            _key = key;
            _padlock = GetOrAdd(_key);
        }

        
        #region *   Internal static methods   *

        private static readonly ConcurrentDictionary<string, CountedLock> WaitLock = new(StringComparer.Ordinal);

        private static object GetOrAdd(string key)
        {
            var padlock = WaitLock.GetOrAdd(key, LockFactory);
            padlock.Increment();
            return padlock;
        }

        private static void ReleaseOrRemove(string key)
        {
            if (WaitLock.TryGetValue(key, out var padlock))
            {
                if (padlock.Decrement() <= 0)
                    WaitLock.TryRemove(key, out padlock);
            }
        }

        private static CountedLock LockFactory(string key) => new();

        private class CountedLock
        {
            private int _counter;

            public int Increment() => Interlocked.Increment(ref _counter);

            public int Decrement() => Interlocked.Decrement(ref _counter);
        }

        #endregion

        #region *   Internal variables & properties *


        private readonly object _padlock;

        private bool _isLocked;

        /// <summary>
        /// Check if a lock was aquired.
        /// </summary>
        public bool IsLocked => _isLocked;


        private readonly string _key;
        /// <summary>
        /// Gets the lock key name.
        /// </summary>
        public string Key => _key;

        /// <summary>
        /// Gets the internal lock object.
        /// </summary>
        public object Lock => _padlock;

        #endregion






        #region *   Internal variables & properties *

        /// <summary>
        /// Tries to aquire a lock.
        /// </summary>
        public bool Enter()
        {
            if (!_isLocked)
            {
                Monitor.Enter(_padlock, ref _isLocked);
            }
            return _isLocked;
        }

        /// <summary>
        /// Tries to aquire a lock respecting the specified timeout.
        /// </summary>
        /// <param name="waitTimeoutMilliseconds">The wait timeout milliseconds.</param>
        /// <returns>If the lock was aquired in the specified timeout</returns>
        public bool Enter(int waitTimeoutMilliseconds)
        {
            if (!_isLocked)
            {
                Monitor.TryEnter(_padlock, waitTimeoutMilliseconds, ref _isLocked);
            }
            return _isLocked;
        }

        /// <summary>
        /// Tries to aquire a lock respecting the specified timeout.
        /// </summary>
        /// <param name="waitTimeout">The wait timeout.</param>
        /// <returns>If the lock was aquired in the specified timeout</returns>
        public bool Enter(TimeSpan waitTimeout)
        {
            return Enter((int)waitTimeout.TotalMilliseconds);
        }

        /// <summary>
        /// Releases the lock if it was already aquired.
        /// Called also at "Dispose".
        /// </summary>
        public bool Exit()
        {
            if (_isLocked)
            {
                _isLocked = false;
                Monitor.Exit(_padlock);
            }
            return false;
        }

        #endregion

        #region *   Factory methods     *

        /// <summary>
        /// Creates a new instance and tries to aquire a lock.
        /// </summary>
        /// <param name="key">The named lock key.</param>
        public static NamedLock CreateAndEnter(string key)
        {
            var item = new NamedLock(key);
            item.Enter();
            return item;
        }

        /// <summary>
        /// Creates a new instance and tries to aquire a lock.
        /// </summary>
        /// <param name="key">The named lock key.</param>
        /// <param name="waitTimeoutMilliseconds">The wait timeout milliseconds.</param>
        public static NamedLock CreateAndEnter(string key, int waitTimeoutMilliseconds)
        {
            var item = new NamedLock(key);
            item.Enter(waitTimeoutMilliseconds);
            return item;
        }

        /// <summary>
        /// Creates a new instance and tries to aquire a lock.
        /// </summary>
        /// <param name="key">The named lock key.</param>
        /// <param name="waitTimeout">The wait timeout.</param>
        public static NamedLock CreateAndEnter(string key, TimeSpan waitTimeout)
        {
            return CreateAndEnter(key, (int)waitTimeout.TotalMilliseconds);
        }

        #endregion




        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// Releases aquired lock and related resources.
        /// </summary>
        public void Dispose()
        {
            Exit();
            ReleaseOrRemove(_key);
        }
    }


    public class NamedLockException : Exception
    {
        public NamedLockException() : base("Couldn't aquire the lock")
        {
            
        }
        public NamedLockException(string message) : base(message)
        {
            
        }
    }
}