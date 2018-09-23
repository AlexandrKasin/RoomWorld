using AutoMapper;
using Data.Entity;
using Service.dto;
using System.Collections.Generic;

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
            //CreateMap<ICollection<Flat>, ICollection<FlatViewModel>>();
        }
    }
}
