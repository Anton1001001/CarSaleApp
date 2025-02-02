namespace Car.Domain.Entities;

public record PaginationRequest(int PageSize = 5, int PageIndex = 0);