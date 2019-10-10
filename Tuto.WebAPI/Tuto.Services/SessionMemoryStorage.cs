using System;
using System.Collections.Concurrent;

namespace Tuto.Services.Interfaces
{
    public class SessionMemoryStorage<T> : ISessionStorage<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _cache;

        public SessionMemoryStorage()
        {
            _cache = new ConcurrentDictionary<Guid, T>();
        }

        public T Get(Guid sessionId)
        {
            if (_cache.TryGetValue(sessionId, out var item))
            {
                return item;
            }

            return null;
        }

        public void Set(Guid sessionId, T user) => _cache.AddOrUpdate(sessionId, user, (key, oldValue) => user);
    }
}
