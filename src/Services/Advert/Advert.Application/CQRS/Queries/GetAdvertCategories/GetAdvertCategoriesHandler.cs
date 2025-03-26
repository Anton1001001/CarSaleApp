using Advert.Domain.Entities;
using Advert.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertCategories;

public class GetAdvertCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAdvertCategoriesQuery, List<GetAdvertCategoryResponse>>
{
    public async Task<List<GetAdvertCategoryResponse>> Handle(GetAdvertCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Repository<AdvertCategory>().GetAllAsync(cancellationToken);
        var response = mapper.Map<List<GetAdvertCategoryResponse>>(categories);
        
        return response;
    }
}