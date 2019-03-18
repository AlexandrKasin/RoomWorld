using System.Threading.Tasks;
using Service.DTO.FeedbackDTO;

namespace Service.Services.FeedbackServices
{
    public interface IFeedbackService
    {
        Task SendFeedback(FeedbackDTO feedbackDTO);
    }
}