using System;

namespace RedisManager.Abstract
{
    internal interface ICache : IDisposable
    {
        T Get<T>(string key);
        bool Set<T>(string key, T obj);
        bool Remove(string key);
        bool Exists(string key);
    }

}
