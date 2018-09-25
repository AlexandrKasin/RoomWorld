﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.dto;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class FlatController : Controller
    {
        private readonly IFlatService _flatService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public FlatController(IFlatService flatService, IUserService userService, IMapper mapper,
            IOrderService orderService)
        {
            _flatService = flatService;
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost("/add-flat")]
        [Authorize]
        public async Task<IActionResult> RentFlat(Flat flat)
        {
            try
            {
                var email = User.Identities.First().Name;
                var user = await (await _userService.GetAllAsync(t => t.Email == email)).FirstOrDefaultAsync();
                flat.User = user; 
                await _flatService.AddFlatAsunc(flat);
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
                var flat = await (await _flatService.GetAllAsync(x => x.Id == id, x => x.Location, x => x.Amentieses,
                    x => x.Extrases, x => x.HouseRuleses, x => x.Images, x => x.Orders)).FirstOrDefaultAsync();
                return Ok(_mapper.Map<FlatViewModel>(flat));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/amount-places")]
        [Authorize]
        public async Task<IActionResult> AmountFlatByLoation(SearchParams searchParams)
        {
            try
            {
                var amount = (await _flatService.GetAllAsync(
                    x => x.Location.Country.ToLower() == searchParams.Country.ToLower()
                         && x.Location.City.ToLower() == searchParams.City.ToLower()
                         && x.Orders.All(o => !(o.DateFrom.Date <= searchParams.DateFrom.Date && o.DateTo.Date >= searchParams.DateFrom.Date)
                                              && !(o.DateFrom.Date <= searchParams.DateTo.Date && o.DateTo.Date >= searchParams.DateTo.Date)
                                              && !(o.DateFrom.Date > searchParams.DateFrom.Date && o.DateFrom.Date < searchParams.DateTo.Date))
                    , x => x.Location)).Count();
                return Ok(amount);
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
                var flats = (await _flatService.GetAllAsync(
                        x => x.Location.Country.ToLower() == searchParams.Country.ToLower()
                             && x.Location.City.ToLower() == searchParams.City.ToLower() 
                             && x.Orders.All(o => !(o.DateFrom.Date <= searchParams.DateFrom.Date && o.DateTo.Date >= searchParams.DateFrom.Date) 
                                                  && !(o.DateFrom.Date <= searchParams.DateTo.Date && o.DateTo.Date >= searchParams.DateTo.Date)
                                                  && !(o.DateFrom.Date > searchParams.DateFrom.Date && o.DateFrom.Date < searchParams.DateTo.Date))
                                                  ,x => x.Location, x => x.Amentieses, x => x.Extrases, x => x.HouseRuleses, x => x.Images))
                    .Skip(searchParams.Skip)
                    .Take(searchParams.Take);
                var flatsModel = _mapper.Map<ICollection<FlatViewModel>>(flats);
                return Ok(flatsModel);
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