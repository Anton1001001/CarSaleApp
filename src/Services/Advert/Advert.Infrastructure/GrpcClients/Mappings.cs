using Advert.Application.Common.Cars;
using Advert.Application.CQRS.Commands.CreateAdvert;
using AutoMapper;
using Car.GrpcService;

namespace Advert.Infrastructure.GrpcClients;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<CarsCatalogRequest, GetCarParametersRequest>().ReverseMap();
        CreateMap<GetCarParametersResponse, CarsCatalogResponse>().ReverseMap();
    }
}