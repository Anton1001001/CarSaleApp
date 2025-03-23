using Advert.Application.Common.Cars.Models;
using Advert.Application.CQRS.Queries.GetAdvertForm.Models;
using Advert.Application.Extensions;
using AutoMapper;
using Car.GrpcService;
using DriveType = Car.GrpcService.DriveType;

namespace Advert.Infrastructure.GrpcClients.CarsCatalog;

public class CarsCatalogServiceMappingProfile : Profile
{
    public CarsCatalogServiceMappingProfile()
    {
        CreateMap<CarsCatalogRequest, GetCarParametersRequest>().ReverseMap();
        CreateMap<GetCarParametersResponse, CarsCatalogResponse>().ReverseMap();
        CreateMap<BodyType, BodyTypeResponse>();
        CreateMap<EngineType, EngineTypeResponse>();
        CreateMap<TransmissionType, TransmissionTypeResponse>();
        CreateMap<DriveType, DriveTypeResponse>();
        CreateMap<Modification, ModificationResponse>();
        CreateMap<Generation, GenerationResponse>();
        CreateMap<Brand, BrandResponse>();
        CreateMap<Model, ModelResponse>();
        CreateMap<Color, ColorResponse>();
        CreateMap<InteriorColor, InteriorColorResponse>();
        CreateMap<InteriorMaterial, InteriorMaterialResponse>();
        CreateMap<Year, YearResponse>()
            .ForCtorParam(dest => dest.Year, opt =>
                opt.MapFrom(src => src.Year_));
    }
}