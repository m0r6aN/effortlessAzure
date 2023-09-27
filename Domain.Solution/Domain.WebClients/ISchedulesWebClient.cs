using Schedules.WebClients.Responses;

namespace Schedules.WebClients;

public interface ISchedulesWebClient
{
    Task<ScheduleByPKeyResponse> GetScheduleByPKey(int schedulePKey);
    Task<List<ScheduleByPKeyResponse>> GetScheduleByBatch(List<int> schedulePKeys);
}