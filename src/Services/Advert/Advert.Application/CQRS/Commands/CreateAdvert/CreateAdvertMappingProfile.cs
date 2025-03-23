using Advert.Application.Common.Advert.Models;
using Advert.Application.Common.Advert.Models.Parameters;
using Advert.Application.Extensions;
using Advert.Domain.Entities;
using AutoMapper;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class CreateAdvertMappingProfile : Profile
{
    public CreateAdvertMappingProfile()
    {
        CreateMap<PhoneNumberRequest, AdvertPhoneNumber>();
        
        CreateMap<PhotoRequest, ICollection<AdvertPhoto>>()
            .ConvertUsing(src => src.Files.Select(fileId => new AdvertPhoto
            {
                FileId = fileId,
                IsMain = fileId == src.Main
            }).ToList());

        CreateMap<ParametersBase, Domain.Entities.Advert>()
            .ForCtorParam(dest => dest.AdvertPhoneNumbers, opt =>
                opt.MapFrom(src => src.Phones))
            .ForCtorParam(dest => dest.AdvertPhotos, opt =>
                opt.MapFrom(src => src.Photos))
            .ForCtorParam(dest => dest.PriceAmount, opt =>
                opt.MapFrom(src => src.Price))
            .ForCtorParam(dest => dest.PriceCurrency, opt =>
                opt.MapFrom(src => src.Currency));
        
    }
}