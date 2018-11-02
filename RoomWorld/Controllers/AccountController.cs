using System;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.dto;
using Service.DTO;
using Service.Exceptions;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IRegistrationService _registrationService;
        private readonly IProfileService _profileService;
        private readonly IEmailService _emailService;

        public AccountController(ITokenService tokenService, IRegistrationService registrationService,
            IProfileService profileService, IEmailService emailService)
        {
            _tokenService = tokenService;
            _registrationService = registrationService;
            _profileService = profileService;
            _emailService = emailService;
        }

        [HttpPost("/registration")]
        public async Task<IActionResult> Registration(User user)
        {
            try
            {
                var responce = await _registrationService.RegisterUserAsync(user);
                return Ok(responce);
            }
            catch (EmailAlredyExistsException e)
            {
                return StatusCode(401, e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("/user/change/profile")]
        public async Task<IActionResult> ChangeProfile(UserViewModel user)
        {
            try
            {
                await _profileService.ChangeProfileAsync(user);
                return Ok();
            }
            catch (IncorrectAuthParamsException e)
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
        public async Task<IActionResult> Token(Authorize authorize)
        {
            try
            { 
                var response = await _tokenService.GetTokenAsync(authorize);
                return Ok(response);
            }
            catch (IncorrectAuthParamsException e)
            {
                return StatusCode(401, e.Message);
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

        [HttpGet("/user/profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var user = await _profileService.GetProflieByEmailAsync();
                return Ok(user);
            }
            catch (IncorrectAuthParamsException e)
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
        public async Task<IActionResult> ChangePassword(ChangePasswordParams changePasswordParams)
        {
            try
            {
                await _registrationService.ChangePasswordAsync(changePasswordParams);
                return Ok();
            }
            catch (IncorrectAuthParamsException e)
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
            catch (InvalidEmailException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("password/change")]
        public async Task<IActionResult> ChangePassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            try
            {
                await _profileService.ResetPasswordByTokenAsync(resetPasswordViewModel);
                return Ok();
            }
            catch (InvalidTokenException e)
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
    }
}