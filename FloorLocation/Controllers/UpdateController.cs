using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;

namespace FloorLocation.Controllers
{
    [Route("api/update")]
    [ApiController]
    public class UpdateController : Controller
    {
        [HttpPost]
        public IActionResult Update(Location _objLocation)
        {
            return Ok(_objLocation.LocationName + " updated successfully.");
        }
    }
}

