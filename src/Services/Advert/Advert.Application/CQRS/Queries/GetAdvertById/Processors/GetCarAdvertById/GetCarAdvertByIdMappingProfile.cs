using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Cars;
using Advert.Application.Common.Cars.Models;
using Advert.Application.CQRS.Commands.CreateAdvert;
using Advert.Domain.Entities;
using AutoMapper;

namespace Advert.Application.CQRS.Queries.GetAdvertById.Processors.GetCarAdvertById;

public class GetCarAdvertByIdMappingProfile : Profile
{
    public GetCarAdvertByIdMappingProfile()
    {
        


    }
}