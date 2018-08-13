using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomWorld.Models;
using RoomWorld.Services;
using Service.iServices;

namespace RoomWorld.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService token;
        private readonly IRegistrationService registration;

        public AccountController(ITokenService token, IRegistrationService registration)
        {
            this.token = token;
            this.registration = registration;
        }

        [HttpPost("/registration")]
        public async Task Registration(User user)
        {
            try
            {
                await registration.RegistrateUserAsunc(user);
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


        [HttpPost("/token")]
        public async Task Token(string email, string password)
        {
            string response;
            try
            {
                response = await token.GetTokenAsunc(email, password);
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