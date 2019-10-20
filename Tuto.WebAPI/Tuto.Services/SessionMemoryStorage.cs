using System;
using System.Collections.Concurrent;
using Tuto.Services.Interfaces;

namespace Tuto.Services
{
    public class SessionMemoryStorage<T> : ISessionStorage<T> where T : class
    {
        private readonly ConcurrentDictionary<Guid, T> _cache = new ConcurrentDictionary<Guid, T>();

        public T Get(Guid sessionId) => _cache.TryGetValue(sessionId, out var item) ? item : null;

        public void Set(Guid sessionId, T user) => _cache.AddOrUpdate(sessionId, user, (key, oldValue) => user);

        public bool TryRemove(Guid sessionId) => _cache.TryRemove(sessionId, out _);
    }
}
