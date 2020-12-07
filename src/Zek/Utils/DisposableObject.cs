using System;

namespace Zek.Utils
{
    public abstract class DisposableObject : IDisposable
    {
        protected bool Disposed { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (Disposed)
                return;

            if (disposing)
            {
                DisposeResources();
            }
            DisposeUnmanagedResources();

            Disposed = true;
        }

        protected abstract void DisposeResources();
        protected virtual void DisposeUnmanagedResources() { }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableObject()
        {
            Dispose(false);
        }


    }
}
