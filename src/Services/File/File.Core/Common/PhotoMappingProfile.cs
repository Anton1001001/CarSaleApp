using AutoMapper;
using File.Core.Common.Models;
using File.Core.Models;
using File.Core.Extensions;

namespace File.Core.Common;

public class PhotoMappingProfile : Profile
{
    public PhotoMappingProfile()
    {
        CreateMap<PhotoSize, PhotoSizeResponse>();
        CreateMap<Photo, PhotoResponse>()
            .ForCtorParam(dest => dest.Big, opt =>
                opt.MapFrom(src => src.Big))
            .ForCtorParam(dest => dest.Medium, opt =>
                opt.MapFrom(src => src.Medium))
            .ForCtorParam(dest => dest.Small, opt =>
                opt.MapFrom(src => src.Small))
            .ForCtorParam(dest => dest.ExtraSmall, opt =>
                opt.MapFrom(src => src.ExtraSmall));
        
    }
}