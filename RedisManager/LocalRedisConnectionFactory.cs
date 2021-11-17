using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisManager
{
    public class LocalRedisConnectionFactory : IDisposable
    {
        private static ConnectionMultiplexer ConnMultiplexer { get; set; }

        static LocalRedisConnectionFactory()
        {
            ConnMultiplexer = ConnectionMultiplexer.Connect(string.Format("{0}:{1},defaultDatabase ={2},abortConnect=false,allowAdmin=true,ConnectTimeout=15000,ConfigCheckSeconds=60,ConnectRetry=30,SyncTimeout=15000", "127.0.0.1", 6379, 0));
        }

        public static ConnectionMultiplexer Connection
        {
            get { return ConnMultiplexer; }
        }

        public void Dispose()
        {
            if (ConnMultiplexer.IsConnected)
                ConnMultiplexer.Dispose();
        }
    }
}
