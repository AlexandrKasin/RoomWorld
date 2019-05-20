using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entity.ApartmentEntity;
using Data.Entity.UserEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.DTO.apartmentReservation;
using Service.DTO.ApartmentDTO;
using Service.Exceptions;

namespace Service.Services.ApartmentServices
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<ApartmentReservation> _reservationRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Apartment> _apartmentRepository;

        public ReservationService(IMapper mapper, IRepository<ApartmentReservation> reservationRepository,
            IRepository<User> userRepository, IHttpContextAccessor httpContextAccessor,
            IRepository<Apartment> apartmentRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _apartmentRepository = apartmentRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task AddReservationAsync(ReservationParamsDTO reservationParamsDTO)
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = (await _userRepository.GetAllAsync(x => x.Email.ToUpper().Equals(email.ToUpper())))
                .FirstOrDefault();
            if (user == null)
                throw new EntityNotExistException("User does not exist.");
            var apartment =
                await (await _apartmentRepository.GetAllAsync(x => x.Id == reservationParamsDTO.IdApartment))
                    .FirstOrDefaultAsync();
            var reservation = _mapper.Map<ApartmentReservation>(reservationParamsDTO);
            reservation.Apartment = apartment ?? throw new EntityNotExistException("Apartment not found.");
            reservation.CreatedBy = user.Id;
            reservation.Client = user;
            await _reservationRepository.InsertAsync(reservation);
        }

        public async Task<IList<ApartmentReservationDTO>> GetReservationsByEmailAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var reservations = await (await _reservationRepository.GetAllAsync(
                x => x.Client.Email.ToUpper().Equals(email.ToUpper()), x => x.Apartment, x => x.Client,
                x => x.Apartment.ApartmentLocation, x => x.Apartment.ApartmentImages)).ToListAsync();
            var reservationsDTO = _mapper.Map<IList<ApartmentReservationDTO>>(reservations);
            return reservationsDTO;
        }

        public async Task<IList<ApartmentReservationDTO>> GetOrdersForUsersFlatsAsync()
        {
            var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var orders = (await _reservationRepository.GetAllAsync(
                x => x.Apartment.Owner.Email.ToUpper().Equals(email.ToUpper()), x => x.Apartment, x => x.Client,
                x => x.Apartment.ApartmentLocation, x => x.Apartment.ApartmentImages));
            return _mapper.Map<IList<ApartmentReservationDTO>>(orders);
        }
    }
}