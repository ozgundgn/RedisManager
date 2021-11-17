using Newtonsoft.Json;
using RedisManager.Abstract;
using StackExchange.Redis;

namespace RedisManager.Repositories
{
    public class MainRedisExchangeRepository : ICache
    {
        private readonly IDatabase _redisDb;
        private readonly MainRedisExchangeRepository _redisClient;

        public MainRedisExchangeRepository()
        {
            _redisDb = LocalRedisConnectionFactory.Connection.GetDatabase();
        }
        public void Dispose()
        {
            LocalRedisConnectionFactory.Connection.Dispose();
        }

        public T Get<T>(string key)
        {
            var redisObj = _redisDb.StringGet(key);
            return redisObj.HasValue ? JsonConvert.DeserializeObject<T>(redisObj) : default(T);
        }

        public string Get(string key)
        {
            return _redisDb.StringGet(key);
        }

        public bool Set<T>(string key, T obj)
        {
            return _redisDb.StringSet(key, JsonConvert.SerializeObject(obj));
        }

        public bool Remove(string key)
        {
            return _redisDb.KeyDelete(key);
        }


        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }
        
    }
}
