using Business;
using DataAccess.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DataAccess.Models;
using DataAccess.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RedisManager;
using RedisManager.Repositories;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;

namespace Redis.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceStackExample serviceStack = new ServiceStackExample();
            StackExchangeExample stackExchange = new StackExchangeExample();

            string key = "urn:users:current";
            string channel1 = "channel-1";
            string channel2 = "channel-2";
            ////1.
            ////User tablomuzdan user ları çekmek istiyoruz.
            GetCurrentUsers(serviceStack, key);

            ////2.
            ////Kanallara abone olup, yayınlanan mesajları görmek ve mesaj göndermek istiyoruz.

            ////Tek kanala abone olmak için
            //OneChannelSucbscribeAndPublish(stackExchange, key,channel1);

            ////Birden fazla kanala abone olmak için
            ManyChannelSubscribeAndPublish(serviceStack, key, channel1, channel2);

            ////var list = serviceStack.ScanAllKeys("urn:users:*"); //başlangıç kısmı urn:users: olan patternlerin kayıtlarını getirir

            Console.ReadLine();
        }


        public static void GetCurrentUsers(ServiceStackExample serviceStack, string key)
        {
            var currentUsers = serviceStack.GetCurrentUsers(key);
            foreach (var user in currentUsers) Debug.WriteLine(user.FirstName);
        }

        public static void OneChannelSucbscribeAndPublish(StackExchangeExample stackExchange, string key, string channel)
        {
            stackExchange.Subscribe(channel);
            //Mesaj gönder
            stackExchange.PublishMessage(channel, "Redisten selam mesajı");
        }

        public static void ManyChannelSubscribeAndPublish(ServiceStackExample serviceStack, string key, string channel1, string channel2)
        {
            serviceStack.PooledSubscribe(channel1, channel2);
            serviceStack.PublisMessage(channel1, JsonConvert.SerializeObject(new InformMessage() { Message = "denemeler", MessageId = 1 })); // Serialize ederek string olarak mesajımızı yayınlıyoruz
        }
    }
}
