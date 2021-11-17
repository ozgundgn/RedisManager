using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using Newtonsoft.Json;
using RedisManager.Repositories;
using ServiceStack.Redis;
using ServiceStack.Text;
using StackExchange.Redis;

namespace RedisManager
{
    public class StackExchangeExample
    {
        private readonly ISubscriber _isubscriber;
        private readonly MainRedisExchangeRepository _redisClient;

        public StackExchangeExample()
        {
            _isubscriber = LocalRedisConnectionFactory.Connection.GetSubscriber();
            _redisClient = new MainRedisExchangeRepository();
        }
        //Sadece kanal 1'e abone olduk
        public void Subscribe(string channel)
        {
            _isubscriber.Subscribe(channel).OnMessage(x => Console.WriteLine(x));
        }

        public void PublishMessage(string channel,string message)
        {
            _isubscriber.Publish("channel-2", "lülü"); // İki kanala mesaj gönderiyoruz
            _isubscriber.Publish(channel, message);
        }

        public T GetTypedCachedValue<T>(string key)
        {
            var redisCached = _redisClient.Get(key);
            var firstDesiralize = JsonConvert.DeserializeObject<string>(redisCached); //Daha sonra bu listeyi kullanmak için Deserialize edip istediğimiz modele çeviriyoruz.
            var result = JsonConvert.DeserializeObject<T>(firstDesiralize);
            return result;
        }
    }
}
