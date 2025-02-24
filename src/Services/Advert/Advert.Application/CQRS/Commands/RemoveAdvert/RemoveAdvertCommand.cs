using System.Text.Json.Serialization;
using Advert.Application.Common.Advert.Models;
using MediatR;

namespace Advert.Application.CQRS.Commands.RemoveAdvert;

public record RemoveAdvertCommand(string RemoveReason) : IRequest<AdvertResponse>
{
    [JsonIgnore]
    public int Id { get; init; }
}