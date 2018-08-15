using System;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Http;
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
        public async Task Registration(User user)
        {
            string responce;
            var password = user.Password;
            try
            {
                await _registrationService.RegistrateUserAsunc(user);
                responce = await _tokenService.GetTokenAsunc(user.Email, password);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(e.Message);
                return;
            }
            Response.StatusCode = 200;
            Response.ContentType = "application/json";
            await Response.WriteAsync(responce);
        }


        [HttpPost("/token")]
        public async Task Token(string email, string password)
        {            
            try
            {
                var response = await _tokenService.GetTokenAsunc(email, password);
                Response.StatusCode = 200;
                Response.ContentType = "application/json";
                await Response.WriteAsync(response);
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(e.Message);
                return;
            }
           
        }

        
    }
}