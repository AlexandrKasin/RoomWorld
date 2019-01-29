using AutoMapper;
using Data.Entity;
using Data.Entity.ApartmentEntity;
using Data.Entity.ChatEntity;
using Data.Entity.UserEntity;
using Service.DTO;
using Service.DTO.apartmentReservation;
using Service.DTO.ApartmentDTO;
using Service.DTO.ChatDTO;
using Service.DTO.UserDTO;

namespace Service.Extension
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserDTO>();          
            CreateMap<User, ProfileDTO>();
            CreateMap<UserRegistrationDTO, User>();
            CreateMap<Message, MessageDTO>();

            CreateMap<ApartmentInsertDTO, Apartment>();
            CreateMap<ApartmentLocationDTO, ApartmentLocation>();
            CreateMap<RulesOfResidenceDTO, RulesOfResidence>();
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentReservationDTO, ApartmentReservation>();
            CreateMap<ReservationParamsDTO, ApartmentReservation>();
        }
    }
}