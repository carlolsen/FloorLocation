using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;

namespace FloorLocation.Controllers
{
    [Route("api/create")]
    [ApiController]
    public class CreateController : Controller
    {
        [HttpPost]
        public IActionResult Create(Location _objLocation)
        {
            return Ok(_objLocation.LocationName + " created successfully.");
        }
    }
}

