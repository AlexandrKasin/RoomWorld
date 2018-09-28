using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistrationService _registrationService;
        private readonly IProfileService _profileService;

        public AccountController(ITokenService tokenService, IRegistrationService registrationService,
            IProfileService profileService)
        {
            _tokenService = tokenService;
            _registrationService = registrationService;
            _profileService = profileService;
        }

        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                await _registrationService.RegistrateUserAsunc(user);
                var responce =
                    await _tokenService.GetTokenAsunc(new Authorize() {Email = user.Email, Password = user.Password});
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

        [HttpPost("/user/profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _profileService.GetProflieByEmailAsync(User.Identities.FirstOrDefault()?.Name);
                return Ok(user);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}