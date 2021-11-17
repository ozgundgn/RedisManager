using System;
using System.Collections.Generic;

namespace RedisManager.Abstract
{
    public interface IServiceStack
    {
        void SetLists<T>(string key, IEnumerable<T> list, DateTime expireDate);
        List<string> ScanAllKeys(string key);
        void PublishMessage(string channel, string message);
    }
}
