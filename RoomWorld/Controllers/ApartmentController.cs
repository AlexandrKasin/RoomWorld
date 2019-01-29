using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTO.ApartmentDTO;
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

       
         [HttpPost("/collection/apartment/amount")]
         [Authorize]
         public async Task<IActionResult> AmountApartmentByLoation(ApartmentSearchParamsDTO apartmentSearchParams)
         {
             try
             {
                 var amountapartments = await _apartmentService.GetAmountApartmentByParamsAsync(apartmentSearchParams);
                 return Ok(amountapartments);
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

        [HttpGet("/collection/users/apartment")]
        [Authorize]
        public async Task<IActionResult> GetApartmentByEmail()
        {
            try
            {
                var apartments = await _apartmentService.GetApartmentByEmailAsync();
                return Ok(apartments);
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
    }
}