using Advert.Application.Common.Advert.Models;
using Advert.Application.Extensions;
using Advert.Domain.Entities;
using AutoMapper;
using AdvertPublicStatus = Advert.Domain.Entities.AdvertPublicStatus;

namespace Advert.Application.Common.Advert;

public class AdvertMappingProfile : Profile
{
    public AdvertMappingProfile()
    {
        CreateMap<AdvertPrivateStatus, PrivateStatus>();
        CreateMap<AdvertPublicStatus, PublicStatus>();
        CreateMap<Domain.Entities.Advert, AdvertResponse>()
            .ForCtorParam(dest => dest.PublicStatus, opt =>
                opt.MapFrom(src => src.AdvertPublicStatus))
            .ForCtorParam(dest => dest.PrivateStatus, opt =>
                opt.MapFrom(src => src.AdvertPrivateStatus))
            .ForCtorParam(dest => dest.LocationName, opt =>
                opt.MapFrom(src => src.PlaceCity!.Label + ", " + src.PlaceRegion!.Label))
            .ForCtorParam(dest => dest.ShortLocationName, opt =>
                opt.MapFrom(src => src.PlaceCity!.ShortName));
        
    }
}