using MongoDB.Bson;
using Nimbus.DataModels.Models.ConfigurationLists;

namespace Nimbus.API.Repositories
{
    public interface IConfigurationListRepository
    {
        void AddMany(List<ConfigurationList> configLists);
        void AddSingle(ConfigurationList configList);
        void Delete(ObjectId id);
        List<ConfigurationList> GetAll();
        ConfigurationList GetByName(string name);
        void UpdateByReplace(ConfigurationList configList);
    }
}