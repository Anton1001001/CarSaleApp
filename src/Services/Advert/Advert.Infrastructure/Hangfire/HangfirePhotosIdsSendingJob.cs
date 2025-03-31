using Advert.Application.JobManaging;
using Advert.Application.Services.Interfaces;
using Advert.Infrastructure.Options;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Advert.Infrastructure.Hangfire;

public class HangfirePhotosIdsSendingJob(
    IOptions<PhotosIdsSendingJobOptions> options,
    IRecurringJobManager recurringJobManager,
    IServiceScopeFactory scopeFactory) : IPhotosIdsSendingJob
{
    public void ScheduleJob()
    {
        var config = options.Value;
        recurringJobManager.AddOrUpdate(
            config.JobId,
            () => ExecuteAsync(CancellationToken.None),
            config.CronExpression);
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var photosSenderService = scope.ServiceProvider.GetRequiredService<IPhotosSenderService>();
        await photosSenderService.SendAdvertsPhotosIdsAsync(cancellationToken);
    }
}