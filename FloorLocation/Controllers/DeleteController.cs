using Microsoft.AspNetCore.Mvc;
using FloorLocation.Models;

namespace FloorLocation.Controllers
{
    [Route("api/delete")]
    [ApiController]
    public class DeleteController : Controller
    {
        [HttpPost]
        public IActionResult Delete(Location _objLocation)
        {
            Context context = new();
            context.DeleteLocation(_objLocation);
            return Ok(_objLocation);
        }
    }
}

