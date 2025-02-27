using Advert.Application.Common.Advert.Models;
using Advert.Application.CQRS.Commands.CreateAdvert.Parameters;
using MediatR;

namespace Advert.Application.CQRS.Commands.CreateAdvert;

public class CreateAdvertCommand : IRequest<AdvertResponse>
{
    public ParametersBase Parameters { get; set; }
}
