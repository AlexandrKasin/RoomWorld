using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class FlatController : Controller
    {
        private readonly IFlatService _flatService;
        private readonly IOrderService _orderService;
        private readonly IUploadImagesService _uploadImagesService;

        public FlatController(IFlatService flatService,
            IOrderService orderService, IUploadImagesService uploadImagesService)
        {
            _flatService = flatService;
            _orderService = orderService;
            _uploadImagesService = uploadImagesService;
        }

        [HttpPost("/upload/images")]
        [Authorize]
        public async Task<IActionResult> RentFlat(IFormCollection formFile)
        {
            try
            {
                await _uploadImagesService.UploadAsync(formFile, User.Identities.FirstOrDefault()?.Name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/place/new")]
        [Authorize]
        public async Task<IActionResult> AddFlat(Flat flat)
        {
            try
            {
                await _flatService.AddFlatAsunc(flat, User.Identities.FirstOrDefault()?.Name);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/order")]
        [Authorize]
        public async Task<IActionResult> OrderFlat(OrderParams orderParams)
        {
            try
            {
                await _orderService.AddOrderAsunc(orderParams, User.Identities.FirstOrDefault()?.Name);
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
                return Ok(await _flatService.GetFlatByIdAsunc(id));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/places/amount")]
        [Authorize]
        public async Task<IActionResult> AmountFlatByLoation(SearchParams searchParams)
        {
            try
            {
                var amountFlats = (await _flatService.GetAllAsync(
                    x => x.Location.Country.ToLower() == searchParams.Country.ToLower()
                         && x.Location.City.ToLower() == searchParams.City.ToLower()
                         && x.Orders.All(o =>
                             !(o.DateFrom.Date <= searchParams.DateFrom.Date &&
                               o.DateTo.Date >= searchParams.DateFrom.Date) &&
                             !(o.DateFrom.Date <= searchParams.DateTo.Date &&
                               o.DateTo.Date >= searchParams.DateTo.Date) &&
                             !(o.DateFrom.Date > searchParams.DateFrom.Date &&
                               o.DateFrom.Date < searchParams.DateTo.Date)), x => x.Location)).Count();
                return Ok(amountFlats);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/search")]
        [Authorize]
        public async Task<IActionResult> SearchFlats(SearchParams searchParams)
        {
            try
            {
                var flats = await _flatService.SearchFlatAsunc(searchParams);
                return Ok(flats);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}