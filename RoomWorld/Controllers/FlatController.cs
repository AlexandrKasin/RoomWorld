using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace RoomWorld.Controllers
{

    [ApiController]
    public class FlatController : Controller
    {
        private readonly IFlatService _flatService;
        private readonly IUserService _userService;

        public FlatController(IFlatService flatService, IUserService userService)
        {
            _flatService = flatService;
            _userService = userService;       
        }

        [HttpPost("/add-flat")]
        [Authorize]
        public async Task<IActionResult> RentFlat(Flat flat)
        {
            try
            {
                var email = User.Identities.First().Name;
                var user = await _userService.GetAllAsync((t => t.Email == email)).Result.FirstAsync();
                flat.User = user;
                await _flatService.AddFlatAsunc(flat);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/get-flat")]
        [Authorize]
        public async Task<IActionResult> GetFlat(FlatConfig flatConfig)
        {
            try
            {
                /*var flats = await _flatService.GetAllAsync(t =>
                        t.Location.Country == flatConfig.Country && t.Location.Sity == flatConfig.City && t.Accommodates >= flatConfig.Accommodates).Result
                    .ToListAsync();*/

                var flats = await _flatService.GetAllAsync(null, x => x.Location, x => x.Amentieses, x => x.Extrases);
                return Ok(flats);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/remove-flat")]
        [Authorize]
        public async Task<IActionResult> RemoveFlat(Flat flat)
        {
            try
            {               
                await _flatService.DeleteFlatAsunc(flat);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}