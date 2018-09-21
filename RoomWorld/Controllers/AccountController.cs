using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistrationService _registrationService;
        private readonly IUserService _userService;

        public AccountController(ITokenService tokenService, IRegistrationService registrationService, IUserService userService)
        {
            _tokenService = tokenService;
            _registrationService = registrationService;
            _userService = userService;
        }

        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                var password = user.Password;
                await _registrationService.RegistrateUserAsunc(user);
                var responce =
                    await _tokenService.GetTokenAsunc(new Authorize() {Email = user.Email, Password = password});
                return Ok(responce);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(Authorize authorize)
        {
            try
            {
                var response = await _tokenService.GetTokenAsunc(authorize);
                return Ok(response);
            }
            catch (ArgumentException)
            {
                return Unauthorized();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("/get-profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await (await _userService.GetAllAsync(t => t.Email == User.Identities.First().Name)).FirstOrDefaultAsync();
                return Ok(user);
            }
    
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}