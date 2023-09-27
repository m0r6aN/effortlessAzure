using Newtonsoft.Json;
using Schedules.WebClients.Responses;

namespace Schedules.WebClients;

public class SchedulesWebClient : ISchedulesWebClient
{
    private readonly ISchedulesUrlService _schedulesUrlService;
    public readonly ISchedulesBaseService SchedulesBaseService;
    public SchedulesWebClient(ISchedulesUrlService schedulesUrlService, ISchedulesBaseService schedulesBaseService)
    {
        _schedulesUrlService = schedulesUrlService ?? throw new ArgumentNullException(nameof(schedulesUrlService));
        SchedulesBaseService = schedulesBaseService ?? throw new ArgumentNullException(nameof(schedulesBaseService));
    }

    /// <summary>
    /// Get schedule by schedule pkey from azure schedules db
    /// </summary>
    /// <param name="schedulePKey"></param>
    /// <returns></returns>
    public async Task<ScheduleByPKeyResponse> GetScheduleByPKey(int schedulePKey)
    {
        var url = _schedulesUrlService.GetScheduleByPKeyUrl(schedulePKey);
        var response = await SchedulesBaseService.HttpGetAsync(url);
        var model = JsonConvert.DeserializeObject<ScheduleByPKeyResponse>(response);
        return model;
    }

    public async Task<List<ScheduleByPKeyResponse>> GetScheduleByBatch(List<int> schedulePKeys)
    {
        if (schedulePKeys != null && !schedulePKeys.Any())
            return new List<ScheduleByPKeyResponse>();

        var pKeys = string.Join(",", schedulePKeys.Select(n => n.ToString()).ToArray());
        var url = _schedulesUrlService.GetSchedulesByBatchUrl(pKeys);
        var response = await SchedulesBaseService.HttpGetAsync(url);
        var model = JsonConvert.DeserializeObject<List<ScheduleByPKeyResponse>>(response);
        return model;
    }
}