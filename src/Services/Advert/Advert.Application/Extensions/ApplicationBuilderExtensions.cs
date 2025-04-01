using Advert.Application.JobManaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Advert.Application.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseHangfireJobs(this IApplicationBuilder app)
    {
        var photosIdsSendingJob = app.ApplicationServices.GetRequiredService<IPhotosIdsSendingJob>();
        photosIdsSendingJob.ScheduleJob();
    }
}