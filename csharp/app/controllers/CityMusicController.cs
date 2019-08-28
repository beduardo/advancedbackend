using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedbackend.domain.config;
using advancedbackend.domain.responsemodel;
using advancedbackend.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace advancedbackend.controllers
{
    [Route("citymusic")]
    [ApiController]
    public class CityMusicController : ControllerBase
    {
        IOptions<AppSettings> Config;
        ICityMusicService Service;
        public CityMusicController(IOptions<AppSettings> config, ICityMusicService service) 
        {
            Config = config;
            Service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string d, float lat, float lon)
        {
            try {
                if (string.IsNullOrWhiteSpace(d) && lat == 0 && lon == 0) {
                    return BadRequest(new ErrorMessageResponse {
                        Error = "BadRequest",
                        Message = "Invalid parameters"
                    });
                }

                if (d != null) {
                    var resp = await Service.GetTracksByCityName(d);

                    if (resp != null) {
                        return Ok(resp);
                    }

                    return NotFound();
                } else {
                    var resp = await Service.GetTracksByCoords(lat, lon);

                    if (resp != null) {
                        return Ok(resp);
                    }

                    return NotFound();
                }
            } catch(Exception ex) {
                return StatusCode(500, new ErrorMessageResponse {
                    Error = "InternalServerError",
                    Message = ex.Message,
                    Trace = ex.StackTrace
                });
            }
        }
    }
}
