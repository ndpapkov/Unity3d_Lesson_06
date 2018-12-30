using System;

namespace Utils
{
    public static class DisposableExtensions
    {
        public static void SafeDispose(this IDisposable disposable)
        {
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}