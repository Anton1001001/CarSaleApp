using Advert.Application.Common.Advert.Models;
using Advert.Application.Extensions;
using AutoMapper;
using File.GrpcService;
using PhotoSize = Advert.Application.Common.Advert.Models.PhotoSize;

namespace Advert.Infrastructure.GrpcClients.FileService;

public class FileServiceMappingProfile : Profile
{
    public FileServiceMappingProfile()
    {
        CreateMap<File.GrpcService.PhotoSize, PhotoSize>();
        
        CreateMap<FileResponse, PhotoResponse>()
            .ForCtorParam(dest => dest.Big, opt =>
                opt.MapFrom(src => src.Big))
            .ForCtorParam(dest => dest.Medium, opt =>
                opt.MapFrom(src => src.Medium))
            .ForCtorParam(dest => dest.Small, opt =>
                opt.MapFrom(src => src.Small))
            .ForCtorParam(dest => dest.ExtraSmall, opt =>
                opt.MapFrom(src => src.ExtraSmall))
            .ForCtorParam(dest => dest.Main, opt =>
                opt.MapFrom(src => false));
    }
}