using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGS.ServicesContract;

namespace PGS.WebApi.Controllers
{
    /// <summary>
    /// weather Api
    /// </summary>
    [Route("api/data")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private IWeatherService _weatherService;
        /// <summary>
        /// Inject
        /// </summary>
        /// <param name="weatherService"></param>
        public WeatherController(IWeatherService weatherService) 
        {
            _weatherService = weatherService;
        }
        /// <summary>
        /// All city weather Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {          
            return Ok( _weatherService.GetCityWeatherList()); 
        }
    }
}