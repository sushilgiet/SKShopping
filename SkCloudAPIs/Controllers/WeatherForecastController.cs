using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureServiceBus;
using CosmoDB;
using AzureEventHub;
using AzureStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;

namespace SkCloudAPIs.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        string _endPoint = string.Empty;
        string _accountkey = string.Empty;
        string _db = string.Empty;
        string _container = string.Empty;
        CosmosClient _cosmoclient = null;
       

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _endPoint = _configuration.GetSection("CosmoDB:accountEndpoint").Value;
            _accountkey = _configuration.GetSection("CosmoDB:key").Value;
            _db = "weather";
            _container = "items";
            _cosmoclient = CosmoDBClient.InitializeCosmosClientInstanceAsync(_db, _container, _endPoint, _accountkey).GetAwaiter().GetResult();
        }

       [HttpGet]
       public void GetSettings()
        {

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            CosmosDBService<WeatherForecast> service = new CosmosDBService<WeatherForecast>(_cosmoclient, _db, _container);
            return service.GetItemsAsync("select * from C").GetAwaiter().GetResult();
        }
       
        [HttpPost]
        public IActionResult Post(WeatherForecast forecast)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
          CosmosDBService<WeatherForecast> service = new CosmosDBService<WeatherForecast>(_cosmoclient,_db,_container);
          service.AddItemAsync(forecast,forecast.id).GetAwaiter().GetResult();
            return Ok();
        }
     
        [HttpPut]
        public IActionResult Put(WeatherForecast forecast)
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            CosmosDBService<WeatherForecast> service = new CosmosDBService<WeatherForecast>(_cosmoclient, _db, _container);
            service.UpdateItemAsync(forecast.id,forecast).GetAwaiter().GetResult();
            return Ok();
        }
    }
}
