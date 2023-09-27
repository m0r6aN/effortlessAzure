namespace Schedules.WebClients;

public interface ISchedulesUrlService
{
    string GetScheduleByPKeyUrl(int schedulePKey);

    string GetSchedulesByBatchUrl(string schedulePKeys);
}