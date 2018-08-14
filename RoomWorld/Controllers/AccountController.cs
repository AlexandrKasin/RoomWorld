using System;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.iServices;

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

        [HttpPost("/registrationService")]
        public async Task Registration(User user)
        {
            try
            {
                await _registrationService.RegistrateUserAsunc(user);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(e.Message);
                return;
            }
            Response.StatusCode = 200;
            await Response.WriteAsync("Succesfull");
        }


        [HttpPost("/tokenService")]
        public async Task Token(string email, string password)
        {
            string response;
            try
            {
                response = await _tokenService.GetTokenAsunc(email, password);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(e.Message);
                return;
            }

            Response.ContentType = "application/json";
            await Response.WriteAsync(response);
        }

        
    }
}