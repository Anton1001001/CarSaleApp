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
        CreateMap<GetCarParametersPreviewResponse, CarsCatalogPreviewResponse>();
        CreateMap<CarsCatalogPreviewRequest, GetCarParametersPreviewRequest>();
        CreateMap<CarsCatalogRequest, GetCarParametersRequest>().ReverseMap();
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

        CreateMap<GetCarParametersResponse, CarsCatalogResponse>()
            .ForCtorParam(dest => dest.EnginePower, opt =>
                opt.MapFrom(src => src.HasEnginePower ? src.EnginePower : default(int?)))
            .ForCtorParam(dest => dest.EngineCapacity, opt =>
                opt.MapFrom(src => src.HasEngineCapacity ? src.EngineCapacity : default(int?)))
            .ForCtorParam(dest => dest.GroundClearance, opt =>
                opt.MapFrom(src => src.HasGroundClearance ? src.GroundClearance : default(float?)))
            .ForCtorParam(dest => dest.FuelConsumptionCombined, opt =>
                opt.MapFrom(src => src.HasFuelConsumptionCombined ? src.FuelConsumptionCombined : default(float?)));
    }
}