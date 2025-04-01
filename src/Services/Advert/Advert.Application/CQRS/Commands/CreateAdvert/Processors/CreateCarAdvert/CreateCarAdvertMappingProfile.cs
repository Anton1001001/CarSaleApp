using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.Common.Cars.Models;
using Advert.Application.Extensions;
using AutoMapper;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Processors.CreateCarAdvert;

public class CreateCarAdvertMappingProfile : Profile
{
    public CreateCarAdvertMappingProfile()
    {
        CreateMap<CarsParameters, CarsCatalogRequest>().ReverseMap();
        CreateMap<CarsParameters, CarParametersToSave>()
            .ForCtorParam(dest => dest.MileageKm, opt =>
                opt.MapFrom(src => -1));
    }
}