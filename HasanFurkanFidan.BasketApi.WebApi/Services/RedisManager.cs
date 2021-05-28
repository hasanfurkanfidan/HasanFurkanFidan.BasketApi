using HasanFurkanFidan.BasketApi.WebApi.Dtos;
using HasanFurkanFidan.UdemyCourse.SHARED.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasanFurkanFidan.BasketApi.WebApi.Services
{
    public class RedisManager
    {
        private readonly string _host;
        private readonly int _port;
        private  ConnectionMultiplexer _connectionMultilexer;
        public RedisManager(string host,int port)
        {
            _host = host;
            _port = port;
        }
        public void Connect() => _connectionMultilexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDatabase(int db = 1) => _connectionMultilexer.GetDatabase(db);
     
    }
}
