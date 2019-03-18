using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.FeedbackDTO;
using Service.Exceptions;
using Service.Services.FeedbackServices;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("/feedback/send")]
        [Authorize]
        public async Task<IActionResult> FeedbackSend(FeedbackDTO feedback)
        {
            try
            {
                await _feedbackService.SendFeedback(feedback);
                return Ok();
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