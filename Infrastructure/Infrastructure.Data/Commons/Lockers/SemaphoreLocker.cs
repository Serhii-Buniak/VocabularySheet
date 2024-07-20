using System.Collections.Concurrent;

namespace Infrastructure.Data.Commons.Lockers;

public class SemaphoreLocker
{
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new ConcurrentDictionary<string, SemaphoreSlim>();

    public async Task<IDisposable> LockKey(string key, CancellationToken cancellationToken)
    {
        var semaphoreSlim = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

        await semaphoreSlim.WaitAsync(cancellationToken);

        return new Releaser(semaphoreSlim);
    }

    private class Releaser : IDisposable
    {
        private readonly SemaphoreSlim _semaphoreSlim;

        public Releaser(SemaphoreSlim semaphoreSlim)
        {
            _semaphoreSlim = semaphoreSlim;
        }

        public void Dispose()
        {
            _semaphoreSlim.Release();
        }
    }
}

