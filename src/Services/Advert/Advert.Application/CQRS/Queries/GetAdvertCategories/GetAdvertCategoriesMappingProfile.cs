using Advert.Domain.Entities;
using AutoMapper;

namespace Advert.Application.CQRS.Queries.GetAdvertCategories;

public class GetAdvertCategoriesMappingProfile : Profile
{
    public GetAdvertCategoriesMappingProfile()
    {
        CreateMap<AdvertCategory, GetAdvertCategoryResponse>();
    }
}