using Advert.Application.Common.Cars.Models;
using AutoMapper;

namespace Advert.Application.Common.Cars;

public class CarsMappingProfile : Profile
{
    public CarsMappingProfile()
    {
        CreateMap<CarsCatalogResponse, CarParametersResponse>();
        CreateMap<CarParametersToSave, CarParametersResponse>();
        CreateMap<CarParametersToSave, CarsCatalogRequest>();
    }
}