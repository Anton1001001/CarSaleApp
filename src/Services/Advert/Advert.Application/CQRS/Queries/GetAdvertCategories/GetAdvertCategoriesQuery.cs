using MediatR;

namespace Advert.Application.CQRS.Queries.GetAdvertCategories;

public record GetAdvertCategoriesQuery : IRequest<List<GetAdvertCategoryResponse>>;