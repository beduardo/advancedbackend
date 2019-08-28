using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedbackend.domain.config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace advancedbackend.controllers
{
    [Route("citymusic")]
    [ApiController]
    public class CityMusicController : ControllerBase
    {
        IOptions<AppSettings> Config;
        public CityMusicController(IOptions<AppSettings> config)
        {
            Config = config;
        }

        [HttpGet]
        public IActionResult Get(string d, float lat, float lon)
        {
            return Ok(Config);
            //throw new NotImplementedException();
        }
    }
}
