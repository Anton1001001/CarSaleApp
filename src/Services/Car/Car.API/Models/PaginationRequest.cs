namespace Car.API.Models;

public record PaginationRequest(int PageSize = 5, int PageIndex = 0);