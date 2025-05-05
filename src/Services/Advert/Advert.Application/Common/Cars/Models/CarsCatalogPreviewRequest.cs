namespace Advert.Application.Common.Cars.Models;

public record CarsCatalogPreviewRequest(    
    int BrandId,
    int ModelId,
    int GenerationId);