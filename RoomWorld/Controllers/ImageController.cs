using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IUploadImagesService _uploadImagesService;

        public ImageController(IUploadImagesService uploadImagesService)
        {
            _uploadImagesService = uploadImagesService;
        }

        [HttpPost("/upload/images")]
        [Authorize]
        public async Task<IActionResult> RentFlat(IFormCollection formFile)
        {
            try
            {
                await _uploadImagesService.UploadAsync(formFile, User.Identities.FirstOrDefault()?.Name);
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
    }
}