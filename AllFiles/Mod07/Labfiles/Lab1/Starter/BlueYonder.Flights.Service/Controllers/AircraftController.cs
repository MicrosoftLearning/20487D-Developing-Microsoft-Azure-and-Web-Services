using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using BlueYonder.Flights.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueYonder.Flights.Service.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private string _baseUrl = "https://blueyonder{YourInitials}.blob.core.windows.net/aircraft-images/";
        
        [HttpGet("Image/{photoName}")]
        public ActionResult<string> GetImage(string photoName)
        {
            return _baseUrl + photoName + ".jpg";
        }
    }
}
