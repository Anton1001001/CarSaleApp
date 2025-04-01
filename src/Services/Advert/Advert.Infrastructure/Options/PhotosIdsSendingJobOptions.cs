namespace Advert.Infrastructure.Options;

public class PhotosIdsSendingJobOptions
{
    public string JobId { get; set; } = string.Empty;
    public string CronExpression { get; set; } = string.Empty;
}