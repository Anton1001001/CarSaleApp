using Advert.Application.Common.Cars;
using Advert.Application.CQRS.Commands.CreateAdvert.Parameters;
using AutoMapper;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;

public class CreateCarAdvertMappingProfile : Profile
{
    public CreateCarAdvertMappingProfile()
    {
        CreateMap<CarsParameters, CarsCatalogRequest>().ReverseMap();
        CreateMap<CarsParameters, CarParametersToSave>();
    }
}