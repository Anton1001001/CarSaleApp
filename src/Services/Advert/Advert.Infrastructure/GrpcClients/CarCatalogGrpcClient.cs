using Advert.Application.Common.Cars;
using Advert.Application.Services;
using AutoMapper;
using Car.GrpcService;
using static Car.GrpcService.CarCatalog;

namespace Advert.Infrastructure.GrpcClients;

public class CarCatalogGrpcClient(CarCatalogClient carCatalogClient, IMapper mapper) : ICarCatalogGrpcClient
{
    public async Task<CarsCatalogResponse> GetCarParametersAsync(CarsCatalogRequest request)
    {
        var catalogRequest = mapper.Map<GetCarParametersRequest>(request);
        var response = await carCatalogClient.GetCarParametersAsync(catalogRequest);
        return mapper.Map<CarsCatalogResponse>(response);
    }
}