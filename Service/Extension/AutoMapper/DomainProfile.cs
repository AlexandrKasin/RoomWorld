using System.Linq;
using AutoMapper;
using Data.Entity.ApartmentEntity;
using Data.Entity.ChatEntity;
using Data.Entity.Feedback;
using Data.Entity.UserEntity;
using Service.DTO;
using Service.DTO.apartmentReservation;
using Service.DTO.ApartmentDTO;
using Service.DTO.ChatDTO;
using Service.DTO.FeedbackDTO;
using Service.DTO.UserDTO;

namespace Service.Extension.AutoMapper
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
            CreateMap<ApartmentLocation, ApartmentLocationDTO>()
                .ForPath(x => x.Coordinates.Latitude, x => x.MapFrom(z => z.Coordinates.X))
                .ForPath(x => x.Coordinates.Longitude, x => x.MapFrom(z => z.Coordinates.Y));
            CreateMap<RulesOfResidenceDTO, RulesOfResidence>();
            CreateMap<Apartment, ApartmentDTO>()
                .ForMember(x => x.Images, x => x.MapFrom(z => z.ApartmentImages.Select(y => y.Url)))
                .ForMember(x => x.ApartmentTypeString, x => x.MapFrom(z => z.ApartmentType.Name));
            CreateMap<ApartmentReservationDTO, ApartmentReservation>();
            CreateMap<ReservationParamsDTO, ApartmentReservation>();
            CreateMap<FeedbackDTO, Feedback>();
        }
    }
}