using Advert.Application.Common.Cars.Models;
using Advert.Application.Extensions;
using AutoMapper;

namespace Advert.Application.Common.Cars;

public class CarsMappingProfile : Profile
{
    public CarsMappingProfile()
    {
        CreateMap<CarsCatalogResponse, CarParametersResponse>()
            .ForCtorParam(dest => dest.Year, opt =>
                opt.MapFrom(src => -1))
            .ForCtorParam(dest => dest.MileageKm, opt =>
                opt.MapFrom(src => -1))
            .ForCtorParam(dest => dest.Registration, opt =>
                opt.MapFrom(src => default(string?)));
        CreateMap<CarParametersToSave, CarParametersResponse>();
        CreateMap<CarParametersToSave, CarsCatalogRequest>();
    }
}