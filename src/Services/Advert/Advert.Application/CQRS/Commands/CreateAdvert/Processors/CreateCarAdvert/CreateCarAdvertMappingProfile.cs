using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.Common.Cars.Models;
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