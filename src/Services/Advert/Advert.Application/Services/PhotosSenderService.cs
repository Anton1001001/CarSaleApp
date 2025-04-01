using Advert.Application.Abstractions;
using Advert.Application.Options;
using Advert.Application.Services.Interfaces;
using Advert.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;

namespace Advert.Application.Services;

public class PhotosSenderService(IUnitOfWork unitOfWork, IMessagePublisher messagePublisher, IOptions<MessageQueueOptions> options) : IPhotosSenderService
{
    public async Task SendAdvertsPhotosIdsAsync(CancellationToken cancellationToken)
    {
        var config = options.Value;
        var photosIds = await unitOfWork.AdvertRepository.GetPhotosIdsAsync(cancellationToken);
        if (photosIds.Count > 0)
        {
            await messagePublisher.PublishAsync(config.QueueName, message: photosIds, cancellationToken);
        }
    }
}