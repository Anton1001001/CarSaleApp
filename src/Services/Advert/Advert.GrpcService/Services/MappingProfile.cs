using Advert.Application.Common.Advert.Models;
using AutoMapper;

namespace Advert.GrpcService.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PublicStatusResponse, PublicStatus>().ReverseMap();
        CreateMap<PriceResponse, Price>().ReverseMap();
        CreateMap<AdvertPreviewResponse, AdvertPreview>().ReverseMap();
    }
}