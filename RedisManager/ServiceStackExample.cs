using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Business.Concrete;
using DataAccess.DAL;
using DataAccess.Models.Concrete;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using RedisManager.Repositories;
using ServiceStack;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;

namespace RedisManager
{
    public class ServiceStackExample
    {

        private readonly MainRedisServiceStackRepository redisClient;

        public ServiceStackExample()
        {
            redisClient = new MainRedisServiceStackRepository();
        }

        //User ları çekerken ServiceStack.Redis ile liste şeklinde kaydediyoruz daha sonra çekerken bu key i kontrol ederek veriyi çekiyoruz.
        public List<User> GetCurrentUsers(string key)
        {
            UserManager userManager = new UserManager(new EfUserDal());
            var currentUsers = userManager.GetAll();
            if (redisClient.Exists(key))
            {
                return redisClient.Get<List<User>>(key);
            }
            redisClient.SetLists<User>(key, currentUsers, DateTime.Now.AddDays(1));
            return currentUsers;
        }

        /// <summary>
        /// 1 veya daha fazla kanala abone olmak için 
        /// </summary>
        public void PooledSubscribe(string channel1, string channel2)
        {
            redisClient.PooledSubscribe(channel1,channel2);
        }
        
        public List<string> ScanAllKeys(string pattern)
        {
            return redisClient.ScanAllKeys(pattern);
        }
        public void PublisMessage(string channel, string message)
        {
            redisClient.PublishMessage(channel, message);
        }

    }
}
