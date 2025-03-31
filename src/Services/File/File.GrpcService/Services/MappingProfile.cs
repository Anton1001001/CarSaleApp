using AutoMapper;
using File.Core.Common.Models;
using File.Core.Models;

namespace File.GrpcService.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PhotoSizeResponse, PhotoSize>();
        CreateMap<PhotoResponse, FileResponse>()
            .ForMember(dest => dest.Big, opt =>
                opt.MapFrom(src => src.Big))
            .ForMember(dest => dest.Medium, opt =>
                opt.MapFrom(src => src.Medium))
            .ForMember(dest => dest.Small, opt =>
                opt.MapFrom(src => src.Small))
            .ForMember(dest => dest.ExtraSmall, opt =>
                opt.MapFrom(src => src.ExtraSmall));
    }
}