using Advert.Application.Common.Advert.Models;
using Advert.Application.Extensions;
using Advert.Domain.Entities;
using AutoMapper;

namespace Advert.Application.Common.Advert;

public class AdvertMappingProfile : Profile
{
    public AdvertMappingProfile()
    {
        CreateMap<AdvertPrivateStatus, PrivateStatusResponse>();
        CreateMap<AdvertPublicStatus, PublicStatusResponse>();

        CreateMap<Domain.Entities.Advert, AdvertResponse>()
            .ForCtorParam(dest => dest.PublicStatus, opt =>
                opt.MapFrom(src => src.AdvertPublicStatus))
            .ForCtorParam(dest => dest.PrivateStatus, opt =>
                opt.MapFrom(src => src.AdvertPrivateStatus))
            .ForCtorParam(dest => dest.LocationName, opt =>
                opt.MapFrom(src => src.PlaceCity.Label + ", " + src.PlaceRegion.Label))
            .ForCtorParam(dest => dest.ShortLocationName, opt =>
                opt.MapFrom(src => src.PlaceCity.ShortName))
            .ForCtorParam(dest => dest.Price, opt =>
                opt.MapFrom(src => default(PriceResponse?)))
            .ForCtorParam(dest => dest.Parameters, opt =>
                opt.MapFrom(src => default(IVehicleParametersResponse?)))
            .ForCtorParam(dest => dest.Photos, opt =>
                opt.MapFrom(src => new List<PhotoResponse>()));
    }
}