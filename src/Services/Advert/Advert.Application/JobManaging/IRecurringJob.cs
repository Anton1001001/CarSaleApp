namespace Advert.Application.JobManaging;

public interface IRecurringJob
{
    void ScheduleJob();
    Task ExecuteAsync(CancellationToken cancellationToken);
}
