using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;

namespace FloorLocation.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : Controller
    {
        [HttpPost]
        public IActionResult Locations()
        {
            Context context = new();
            List<Location> list = context.GetLocations();
            return Ok(list);
        }
    }
}

