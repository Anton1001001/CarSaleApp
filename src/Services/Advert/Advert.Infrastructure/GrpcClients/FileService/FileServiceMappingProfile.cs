using Advert.Application.Common.Advert.Models;
using AutoMapper;
using File.GrpcService;

namespace Advert.Infrastructure.GrpcClients.FileService;

public class FileServiceMappingProfile : Profile
{
    public FileServiceMappingProfile()
    {
        CreateMap<FileResponse, PhotoResponse>()
            .ForMember(dest => dest.Big, opt =>
                opt.MapFrom(src => src.Big))
            .ForMember(dest => dest.Medium, opt =>
                opt.MapFrom(src => src.Medium))
            .ForMember(dest => dest.Small, opt =>
                opt.MapFrom(src => src.Small))
            .ForMember(dest => dest.ExtraSmall, opt =>
                opt.MapFrom(src => src.ExtraSmall))
            .ForMember(dest => dest.Main, opt =>
                opt.MapFrom(_ => false));
    }
}