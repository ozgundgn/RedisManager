using System;
using System.Collections.Generic;
using System.Linq;
using RedisManager.Abstract;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;

namespace RedisManager.Repositories
{
    public class MainRedisServiceStackRepository : ICache
    {
        private readonly IRedisClient _redisClient;
        private readonly IRedisClientsManager _clientsManager;

        public MainRedisServiceStackRepository()
        {
            _clientsManager = new BasicRedisClientManager("localhost?db=1");
            _redisClient = _clientsManager.GetClient();
        }

        public void Dispose()
        {
            _redisClient.Dispose();
        }

        public bool Exists(string key)
        {
            return _redisClient.ContainsKey(key);
        }

        public T Get<T>(string key)
        {
            return _redisClient.Get<T>(key);
        }

        public bool Remove(string key)
        {
            return _redisClient.Remove(key);
        }

        public bool Set<T>(string key, T obj)
        {
            return _redisClient.Set<T>(key, obj, DateTime.Now.AddDays(1));
        }

        public void SetLists<T>(string key, IEnumerable<T> list, DateTime expireTime)
        {
            IRedisTypedClient<T> redisTyped = _redisClient.As<T>();
            var currentList = redisTyped.Lists[key];
            var cachedList = currentList.Concat(list).ToList();
            _redisClient.Set(key, cachedList, expireTime);
        }
        public List<string> ScanAllKeys(string key)
        {
            return _redisClient.ScanAllKeys(key).ToList<string>();
        }
        public void PublishMessage(string channel,string message)
        {
             _redisClient.PublishMessage("channel-2", "sabit mesajımız Service Stackten");
             _redisClient.PublishMessage(channel, message);
        }

        public void PooledSubscribe(string channel1, string channel2)
        {
            new RedisPubSubServer(_clientsManager, channel1, channel2)
            {
                OnMessage = (channel, msg) => "Received '{0}' from '{1}'".Print(msg, channel)
            }.Start();
        }
    }
}
