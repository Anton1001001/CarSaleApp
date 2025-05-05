namespace Chat.Core.CQRS.Queries.GetUserDialogs;

public record PublicStatusResponse(
    string Label, 
    string Name
);