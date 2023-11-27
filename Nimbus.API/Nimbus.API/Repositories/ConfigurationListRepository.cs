using System;
using MongoDB.Bson;
using MongoDB.Driver;
using Nimbus.DataModels.Models.ConfigurationLists;

namespace Nimbus.API.Repositories
{
    public class ConfigurationListRepository : IConfigurationListRepository
    {
        private readonly IMongoCollection<ConfigurationList> _configLists;

        public ConfigurationListRepository(IConfiguration configuration)
        {
            IConfigurationSection mongoSettings = configuration.GetSection("MongoDBSettings");
            string connectionString = mongoSettings.GetValue<string>("ConnectionString");
            string databaseName = mongoSettings.GetValue<string>("DatabaseName");

            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);

            _configLists = database.GetCollection<ConfigurationList>("configurationlists");
        }

        public List<ConfigurationList> GetAll()
        {
            List<ConfigurationList> audits = _configLists.Find(Builders<ConfigurationList>.Filter.Empty).ToList();
            return audits;
        }

        public ConfigurationList GetByName(string name)
        {
            FilterDefinition<ConfigurationList> filter = Builders<ConfigurationList>.Filter.Eq(cL => cL.Name, name);

            ConfigurationList configList = _configLists.Find(filter).FirstOrDefault();

            return configList;
        }

        public void AddSingle(ConfigurationList configList)
        {
            _configLists.InsertOne(configList);
        }

        public void AddMany(List<ConfigurationList> configLists)
        {
            _configLists.InsertMany(configLists);
        }

        public void UpdateByReplace(ConfigurationList configList)
        {
            FilterDefinition<ConfigurationList> filter = Builders<ConfigurationList>.Filter.Eq(o => o.Id, configList.Id);
            _configLists.ReplaceOne(filter, configList);
        }

        public void Delete(ObjectId id)
        {
            FilterDefinition<ConfigurationList> filter = Builders<ConfigurationList>.Filter.Eq(o => o.Id, id);
            _configLists.DeleteOne(filter);
        }
    }
}