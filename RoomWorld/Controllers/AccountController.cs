using System;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace RoomWorld.Controllers
{
    
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
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                var password = user.Password;
                await _registrationService.RegistrateUserAsunc(user);
                var responce = await _tokenService.GetTokenAsunc(user.Email, password);
                return Ok(responce);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(string email, string password)
        {            
            try
            {
                var response = await _tokenService.GetTokenAsunc(email, password);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
    }
}