using AutoMapper;
using Data.Entity;
using Service.dto;

namespace Service
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, UserViewModel>();       
            CreateMap<Flat, FlatViewModel>();
            CreateMap<Location, LocationViewModel>();
            CreateMap<HouseRules, HouseRulesViewModel>();
            CreateMap<Amenties, AmenitiesViewModel>();
            CreateMap<Image, ImageViewModel>();
            CreateMap<Order, OrderViewModel>();
        }
    }
}
