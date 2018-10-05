using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
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

        [HttpPost("/place/new")]
        [Authorize]
        public async Task<IActionResult> AddFlat(Flat flat)
        {
            try
            {
                await _flatService.AddFlatAsync(flat, User.Identities.FirstOrDefault()?.Name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet("/flat")]
        [Authorize]
        public async Task<IActionResult> GetFlatById(int id)
        {
            try
            {
                return Ok(await _flatService.GetFlatByIdAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("/places/amount")]
        [Authorize]
        public async Task<IActionResult> AmountFlatByLoation([FromHeader] SearchParams searchParams)
        {
            try
            {
                var amountFlats = await _flatService.AmountFlatByParamsAsync(searchParams);
                return Ok(amountFlats);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("/search")]
        [Authorize]
        public async Task<IActionResult> SearchFlats([FromHeader] SearchParams searchParams)
        {
            try
            {
                var flats = await _flatService.SearchFlatAsync(searchParams);
                return Ok(flats);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}