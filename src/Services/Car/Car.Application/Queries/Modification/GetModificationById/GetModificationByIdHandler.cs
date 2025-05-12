using Microsoft.Extensions.Logging;

namespace Car.Application.Queries.Modification.GetModificationById;

public class GetModificationByIdHandler(
    IRepository<CarModification> modificationRepository,
    ILogger<GetModificationByIdHandler> logger) : IRequestHandler<GetModificationByIdQuery, GetModificationByIdResponse>
{
    public async Task<GetModificationByIdResponse> Handle(GetModificationByIdQuery request,
        CancellationToken cancellationToken)
    {
        var modification = await modificationRepository.GetByIdAsync(request.Id, cancellationToken);

        logger.LogInformation(
            "Modification parameters: \nEnginePower: {enginePower}\nEngineCapacity: {engineCapacity}\nGroundClearance: {groundClearance}\nFuelConsumption: {fuelConsumption}",
            modification.EnginePower, modification.EngineCapacity, modification.GroundClearance,
            modification.FuelConsumptionCombined);

        return new GetModificationByIdResponse(modification.Id, modification.Name, modification.EnginePower,
            modification.EngineCapacity, modification.GroundClearance, modification.FuelConsumptionCombined);
    }
}