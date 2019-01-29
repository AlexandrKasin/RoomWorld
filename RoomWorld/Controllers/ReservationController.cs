using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTO.apartmentReservation;
using Service.Exceptions;
using Service.Services.ApartmentServices;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/users/reservations")]
        [Authorize]
        public async Task<IActionResult> OrderedFlats()
        {
            try
            {
                var orders = await _reservationService.GetReservationsByEmailAsync();
                return Ok(orders);
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


        [HttpGet("/apartments/reservations")]
        [Authorize]
        public async Task<IActionResult> OrderForUserFlats()
        {
            try
            {
                var orders = await _reservationService.GetOrdersForUsersFlatsAsync();
                return Ok(orders);
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

        [HttpPost("/reservation/create")]
        [Authorize]
        public async Task<IActionResult> OrderFlat(ReservationParamsDTO reservationParams)
        {
            try
            {
                await _reservationService.AddReservationAsync(reservationParams);
                return Ok();
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
    }
}