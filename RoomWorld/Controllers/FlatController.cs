using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class FlatController : Controller
    {
        private readonly IFlatService _flatService;

        public FlatController(IFlatService flatService)
        {
            _flatService = flatService;
        }

        [HttpPost("/rent")]
        [Authorize]
        public async Task<IActionResult> RentFlat(Flat flat)
        {
            await _flatService.AddFlatAsunc(flat);
            await _flatService.DeleteFlatAsunc(flat);
            return Ok();
        }
    }
}