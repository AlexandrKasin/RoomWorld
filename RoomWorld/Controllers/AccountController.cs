using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomWorld.Models;
using RoomWorld.Services;

namespace RoomWorld.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("/registration")]
        public async Task Registration(User user)
        {
            try
            {
                userService.AddUser(user);
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
                response = userService.GetToken(email, password);
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