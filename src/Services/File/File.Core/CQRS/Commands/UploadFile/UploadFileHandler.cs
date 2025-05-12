using AutoMapper;
using File.Core.Abstractions;
using File.Core.Common.Models;
using File.Core.Constants;
using File.Core.Errors;
using File.Core.Errors.Base;
using File.Core.Models;
using File.Core.Options;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using static File.Core.Helpers.StreamConverter;
using static File.Core.Helpers.ImageResizer;

namespace File.Core.CQRS.Commands.UploadFile;

public class UploadFileHandler(
    IStorageService storageService,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IOptions<ImageSizeOptions> imageSizeOptions,
    ILogger<UploadFileHandler> logger)
    : IRequestHandler<UploadFileCommand, Result<PhotoResponse>>
{
    public async Task<Result<PhotoResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        // for (int i = 0; i < 300; i++)
        // {
        //     await Task.Delay(1000, cancellationToken);
        //     logger.LogInformation("Second {second}", i);
        // }
        //
        // return new Result<PhotoResponse>();

        var config = imageSizeOptions.Value;
        var mimeType = request.File.ContentType.ToLower();
        
        if (!ImageFormats.IsSupported(mimeType))
        {
            return new FileBadRequestError(
                message: "Неверный формат. Можно загрузить изображения только в формате JPG или PNG");
        }
        
        var fileExtension = Path.GetExtension(request.File.FileName).ToLower();
        
        await using var stream = request.File.OpenReadStream();
        using var image = await Image.LoadAsync(stream, cancellationToken);
        
        var (bigWidth, bigHeight) = CalculateSize(image.Width, image.Height, config.MaxLength);
        var (mediumWidth, mediumHeight) = CalculateSize(image.Width, image.Height, config.MediumLength);
        var (smallWidth, smallHeight) = CalculateSize(image.Width, image.Height, config.SmallLength);
        var (extraSmallWidth, extraSmallHeight) = CalculateSize(image.Width, image.Height, config.ExtraSmallLength);
        
        var bigImage = ConvertToStream(ResizeImage(image, bigWidth, bigHeight), mimeType);
        var mediumImage = ConvertToStream(ResizeImage(image, mediumWidth, mediumHeight), mimeType);
        var smallImage = ConvertToStream(ResizeImage(image, smallWidth, smallHeight), mimeType);
        var extraSmallImage = ConvertToStream(ResizeImage(image, extraSmallWidth, extraSmallHeight), mimeType);
        
        var photo = new Photo { MimeType = mimeType };
        await unitOfWork.PhotoRepository.AddAsync(photo, cancellationToken);
        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError("File.Upload", "Error while saving data");
        }
        
        var photoId = photo.Id;
        
        var uploadTasks = new[]
        {
            storageService.UploadFileAsync(bigImage, $"{photoId}/{PhotoSizeNames.Big}{fileExtension}"),
            storageService.UploadFileAsync(mediumImage, $"{photoId}/{PhotoSizeNames.Medium}{fileExtension}"),
            storageService.UploadFileAsync(smallImage, $"{photoId}/{PhotoSizeNames.Small}{fileExtension}"),
            storageService.UploadFileAsync(extraSmallImage, $"{photoId}/{PhotoSizeNames.ExtraSmall}{fileExtension}")
        };
        
        var uploadResults = await Task.WhenAll(uploadTasks);
        
        var bigUrl = uploadResults[0];
        var mediumUrl = uploadResults[1];
        var smallUrl = uploadResults[2];
        var extraSmallUrl = uploadResults[3];
        
        photo.Big = new PhotoSize { Width = bigWidth, Height = bigHeight, Url = bigUrl };
        photo.Medium = new PhotoSize { Width = mediumWidth, Height = mediumHeight, Url = mediumUrl };
        photo.Small = new PhotoSize { Width = smallWidth, Height = smallHeight, Url = smallUrl };
        photo.ExtraSmall = new PhotoSize { Width = extraSmallWidth, Height = extraSmallHeight, Url = extraSmallUrl };
        
        saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!saveResult)
        {
            return new InternalServerError("File.Upload", "Error while saving data");
        }
        
        var response = mapper.Map<PhotoResponse>(photo);
        
        return response;
    }
}
