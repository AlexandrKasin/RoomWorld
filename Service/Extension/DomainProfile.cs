using AutoMapper;
using Data.Entity;
using Service.dto;
using Service.DTO;
using Service.DTO.ApartmentDTO;

namespace Service.Extension
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserViewModel>();
            CreateMap<Apartment, FlatViewModel>();
            CreateMap<ApartmentLocation, LocationViewModel>();
            CreateMap<RulesOfResidence, HouseRulesViewModel>();
            CreateMap<Amenities, AmenitiesViewModel>();
            CreateMap<ApartmentImage, ImageViewModel>();
            CreateMap<ApartmentReservation, OrderViewModel>();
            CreateMap<OrderedFlatViewModel, ApartmentReservation>();
            CreateMap<User, ProfileViewModel>();
            CreateMap<UserRegistrationParamsViewModel, User>();
            CreateMap<Message, MessageViewModel>();

            CreateMap<ApartmentInsertDTO, Apartment>();
            CreateMap<ApartmentLocationDTO, ApartmentLocation>();
            CreateMap<RulesOfResidenceDTO, RulesOfResidence>();
            CreateMap<Apartment, ApartmentDTO>();
            CreateMap<ApartmentReservationDTO, ApartmentReservation>();
        }
    }
}