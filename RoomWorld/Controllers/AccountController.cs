using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTO;
using Service.DTO.UserDTO;
using Service.Exceptions;
using Service.Services.UserServices;

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
        public async Task<IActionResult> Registration(UserRegistrationDTO user)
        {
            try
            {
                var responce = await _registrationService.RegisterUserAsync(user);
                return Ok(responce);
            }
            catch (EntityAlredyExistsException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/user/change/profile")]
        public async Task<IActionResult> ChangeProfile([FromForm]ProfileDTO user)
        {
            try
            {
                await _profileService.ChangeProfileAsync(user);
                return Ok();
            }
            catch (IncorrectParamsException e)
            {
                return BadRequest(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/token")]
        public async Task<IActionResult> Token(AuthorizeDTO authorize)
        {
            try
            {
                var response = await _tokenService.GetTokenAsync(authorize);
                return Ok(response);
            }
            catch (EntityNotExistException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (IncorrectParamsException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return StatusCode(403, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/user/profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _profileService.GetProflieByEmailAsync();          
                return Ok(user);
            }
            catch (IncorrectParamsException e)
            {
                return BadRequest(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/user/change/password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordParamsDTO changePasswordParams)
        {
            try
            {
                await _registrationService.ChangePasswordAsync(changePasswordParams);
                return Ok();
            }
            catch (IncorrectParamsException e)
            {
                return BadRequest(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("password/reset/{email}")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                await _profileService.SendMessageResetPasswordAsync(email);
                return Ok();
            }
            catch (EntityNotExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("password/change")]
        public async Task<IActionResult> ChangePassword(ResetPasswordDTO resetPasswordViewModel)
        {
            try
            {
                await _profileService.ResetPasswordByTokenAsync(resetPasswordViewModel);
                return Ok();
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (TokenExpiredException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /**/
        [HttpGet("/get/version")]
        [Authorize]
        public async Task<IActionResult> GetVersion()
        {
            try
            {
                var user = await _profileService.GetProflieByEmailAsync();
                return Ok(user);
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

        [HttpPut("/test")]
        public async Task<IActionResult> TestOptimisticLock(byte[] version)
        {
            try
            {
                await _profileService.ChangeProfileAsync(new ProfileDTO{Email = "Al@gmail.com",Name = "asdasd",PhoneNumber = "283176511",Surname = "dfasdfsa",Version = version});
                return Ok();
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
        /**/
    }
}