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
        private readonly string _password;
        private ConnectionMultiplexer _connectionMultilexer;
        public RedisManager(string host, int port,string password)
        {
            _host = host;
            _port = port;
            _password = password;
        }

        public void Connect() {
            ConfigurationOptions options = ConfigurationOptions.Parse($"{_host}:{_port}");
      
            options.Password = "PkfBr5PfNIWdN26j0SsdeLm14x2CLb3z";
            _connectionMultilexer = ConnectionMultiplexer.Connect(options);
        } 
        public IDatabase GetDatabase(int db = 1) => _connectionMultilexer.GetDatabase(db);

    }
}
