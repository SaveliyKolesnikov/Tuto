using System;

namespace Tuto.Services.Interfaces
{
    public interface ISessionStorage<T>
    {
        T Get(Guid sessionId);

        void Set(Guid sessionId, T value);
    }
}
