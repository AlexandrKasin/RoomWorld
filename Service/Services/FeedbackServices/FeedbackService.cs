using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity.Feedback;
using Data.Entity.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.DTO.FeedbackDTO;
using Service.Exceptions;

namespace Service.Services.FeedbackServices
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<User> _userRepository;

        public FeedbackService(IRepository<Feedback> feedbackRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor, IRepository<User> userRepository)
        {
            _feedbackRepository = feedbackRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task SendFeedback(FeedbackDTO feedbackDTO)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = await (await _userRepository.GetAllAsync(t => t.Email.ToUpper().Equals(email.ToUpper())))
                .FirstOrDefaultAsync();
            var feedback = _mapper.Map<Feedback>(feedbackDTO);
            feedback.User = user ?? throw new EntityNotExistException("User does not exist.");
            await _feedbackRepository.InsertAsync(feedback);
        }
    }
}