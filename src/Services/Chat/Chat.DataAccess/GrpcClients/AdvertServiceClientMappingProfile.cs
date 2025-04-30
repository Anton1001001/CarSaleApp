using Advert.GrpcService;
using AutoMapper;
using Chat.Core.CQRS.Queries.GetUserDialogs;

namespace Chat.DataAccess.GrpcClients;

public class AdvertServiceClientMappingProfile : Profile
{
    public AdvertServiceClientMappingProfile()
    {
        CreateMap<PublicStatus, PublicStatusResponse>();
        CreateMap<Price, PriceResponse>();
        CreateMap<AdvertPreview, AdvertPreviewResponse>();

    }
}