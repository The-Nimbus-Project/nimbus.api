using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Nimbus.API.Repositories;
using Nimbus.DataModels.Models.ConfigurationLists;

namespace Nimbus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationListController : ControllerBase
    {
        private readonly IConfigurationListRepository _configListRepository;

        public ConfigurationListController(IConfigurationListRepository configListRepository)
        {
            _configListRepository = configListRepository;
        }

        // https://localhost:5001/api/configList/
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_configListRepository.GetAll());
        }

        // https://localhost:5001/api/configList/randomConfigList
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            ConfigurationList configList = _configListRepository.GetByName(name);

            if (configList == null)
            {
                return NotFound();
            }

            return Ok(configList);
        }

        // https://localhost:5001/api/configList/
        [HttpPost]
        public IActionResult Post(ConfigurationList configList)
        {
            _configListRepository.AddSingle(configList);
            return CreatedAtAction("Get", new { name = configList.Name }, configList);
        }

        // https://localhost:5001/api/configList/
        [HttpPost("PostMany")]
        public IActionResult PostMany(List<ConfigurationList> configLists)
        {
            _configListRepository.AddMany(configLists);
            return NoContent();
        }

        // https://localhost:5001/api/configList/5
        [HttpPut("{_id}")]
        public IActionResult Put([FromRoute] ObjectId _id, [FromBody] ConfigurationList configList)
        {
            if (_id != configList.Id)
            {
                return BadRequest();
            }

            _configListRepository.UpdateByReplace(configList);
            return NoContent();
        }

        // https://localhost:5001/api/configList/5
        [HttpDelete("{_id}")]
        public IActionResult Delete(ObjectId id)
        {
            _configListRepository.Delete(id);
            return NoContent();
        }
    }
}