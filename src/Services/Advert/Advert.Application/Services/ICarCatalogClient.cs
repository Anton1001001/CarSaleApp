using Advert.Application.Common.Cars;
using Advert.Application.CQRS.Commands.CreateAdvert;

namespace Advert.Application.Services;

public interface ICarCatalogGrpcClient
{
    Task<CarsCatalogResponse> GetCarParametersAsync(CarsCatalogRequest request);
}