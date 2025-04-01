using Advert.Application.Errors;
using Advert.Domain.Entities;
using Advert.Domain.Interfaces.Repositories;
using AutoMapper;
using FluentResults;
using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertCategories;

public class GetAdvertCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAdvertCategoriesQuery, Result<List<GetAdvertCategoryResponse>>>
{
    public async Task<Result<List<GetAdvertCategoryResponse>>> Handle(GetAdvertCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Repository<AdvertCategory>().GetAllAsync(cancellationToken);

        if (categories.Count == 0)
        {
            return new CategoryNotFoundError(message: "List of categories is empty");
        }
        var response = mapper.Map<List<GetAdvertCategoryResponse>>(categories);
        
        return response;
    }
}