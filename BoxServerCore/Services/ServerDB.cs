using System;
using System.Linq;
using System.Threading.Tasks;
using BoxProtocol.Models;
using BoxProtocol.Interfaces;
using Xamarin.Essentials;
using Nest;
using MagicOnion.Server;
using MagicOnion;
using System.Collections.Generic;


namespace BoxProtocol
{
    public class ServerDB : ServiceBase<IServerDB>, IServerDB
    {
        private static bool Initialized;
        private static ElasticClient _client;
        public static ElasticClient Client()
        {
            if (!Initialized)
            {
                var node = new Uri("http://127.0.0.1:9200");
                var settings = new ConnectionSettings(node);
                _client = new ElasticClient(settings);
                Initialized = true;
                _client.Indices.Create("data_base");
            }
            return _client;
        }

        public UnaryResult<bool> Add(Item item)
        {
            var client = Client();
            client.Index<Item>(item, idx => idx.Index("data_base"));
            return new UnaryResult<bool>(true);
        }
        public UnaryResult<List<Item>> GetAll() 
        {
            var docs = Client().Search<Item>(s => s
                .Index("data_base")
                .Query(q => q
                    .MatchAll())
            );
            return new UnaryResult<List<Item>>(docs.Documents.ToList());
        }     
        
    }
}


  