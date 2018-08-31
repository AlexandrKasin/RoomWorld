using System;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistrationService _registrationService;

        public AccountController(ITokenService tokenService, IRegistrationService registrationService)
        {
            _tokenService = tokenService;
            _registrationService = registrationService;
        }

        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(UserModel user)
        {
            try
            {
                var password = user.Password;
                await _registrationService.RegistrateUserAsunc(user);
                var responce = await _tokenService.GetTokenAsunc(user.Email, password);
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
                var response = await _tokenService.GetTokenAsunc(authorize.Email, authorize.Password);
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
    }
}