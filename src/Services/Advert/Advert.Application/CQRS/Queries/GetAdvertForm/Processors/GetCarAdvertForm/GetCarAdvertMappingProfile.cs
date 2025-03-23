using Advert.Application.CQRS.Queries.GetAdvertForm.Models;
using Advert.Application.Extensions;
using Advert.Domain.Entities;
using AutoMapper;

namespace Advert.Application.CQRS.Queries.GetAdvertForm.Processors.GetCarAdvertForm;

public class GetCarAdvertMappingProfile : Profile
{
    public GetCarAdvertMappingProfile()
    {
        
        CreateMap<Place, PlaceRegionResponse>()
            .ForCtorParam(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Label));
        
        CreateMap<Place, PlaceCityResponse>()
            .ForCtorParam(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Label));

        CreateMap<PhoneCode, PhoneCodeResponse>();
    }
}