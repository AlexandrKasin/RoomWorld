using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.DTO.ApartmentDTO;
using Service.Exceptions;
using Service.Services.ApartmentServices;


namespace RoomWorld.Controllers
{
    [ApiController]
    public class FlatController : Controller
    {
        private readonly IApartmentService _apartmentService;

        public FlatController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        [HttpPost("/apartment/create")]
        [Authorize]
        public async Task<IActionResult> CreateApartment([FromForm] ApartmentInsertDTO apartment)
        {
            try
            {
                await _apartmentService.InsertApartmentAsync(apartment);
                return Ok();
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("/apartment/types")]
        [Authorize]
        public async Task<IActionResult> GetApartmentTypes()
        {
            try
            {
                var types = await _apartmentService.GetApartmentTypesAsync();
                return Ok(types);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("/apartment/get")]
        [Authorize]
        public async Task<IActionResult> GetApartment(int id)
        {
            try
            {
                var apartment = await _apartmentService.GetApartmentByIdAsync(id);
                return Ok(apartment);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/collection/apartment")]
        [Authorize]
        public async Task<IActionResult> GetCoollectionApartment(ApartmentSearchParamsDTO apartmentSearchParams)
        {
            try
            {
                var collectionApartment = await _apartmentService.GetApartmentByParamsAsync(apartmentSearchParams);
                return Ok(collectionApartment);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /* [HttpGet("/apartment")]
         [Authorize]
         public async Task<IActionResult> GetFlatById(int id)
         {
             try
             {
                 return Ok(await _flatService.GetFlatByIdAsync(id));
             }
             catch (EntityNotExistException e)
             {
                 return BadRequest(e.Message);
             }
             catch (DbUpdateConcurrencyException e)
             {
                 return BadRequest(e.Message);
             }
             catch (Exception e)
             {
                 return BadRequest(e);
             }
         }
 
         [HttpGet("/places/amount")]
         [Authorize]
         public async Task<IActionResult> AmountFlatByLoation([FromHeader] SearchParamsViewModel searchParams)
         {
             try
             {
                 var amountFlats = await _flatService.AmountFlatByParamsAsync(searchParams);
                 return Ok(amountFlats);
             }
             catch (DbUpdateConcurrencyException e)
             {
                 return BadRequest(e.Message);
             }
             catch (Exception e)
             {
                 return BadRequest(e);
             }
         }
 
         [HttpGet("/search")]
         [Authorize]
         public async Task<IActionResult> SearchFlats([FromHeader] SearchParamsViewModel searchParams)
         {
             try
             {
                 var flats = await _flatService.SearchFlatAsync(searchParams);
                 return Ok(flats);
             }
             catch (DbUpdateConcurrencyException e)
             {
                 return BadRequest(e.Message);
             }
             catch (Exception e)
             {
                 return BadRequest(e);
             }
         }*/
    }
}