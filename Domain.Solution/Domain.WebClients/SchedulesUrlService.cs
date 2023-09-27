namespace Schedules.WebClients;

public class SchedulesUrlService : BaseUrlService, ISchedulesUrlService
{
    public ISchedulesBaseService _shedulesBaseService;
    public ISchedulesWebClientConfig _config;

    public SchedulesUrlService(ISchedulesWebClientConfig config,
        ISchedulesBaseService schedulesBaseService) : base(config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _shedulesBaseService = schedulesBaseService ?? throw new ArgumentNullException(nameof(schedulesBaseService));
    }

    public string GetScheduleByPKeyUrl(int schedulePKey)
    {
        var url = $"{GetBaseUrl()}/schedule-by-pkey";
        var kvp = new Dictionary<string, string>
        {
            { nameof(schedulePKey), schedulePKey.ToString() }
        };
        return _shedulesBaseService.BuildUrl(url, kvp);
    }

    public string GetSchedulesByBatchUrl(string schedulePKeys)
    {
        var kvp = new Dictionary<string, string>
        {
            { nameof(schedulePKeys), schedulePKeys }
        };
        var url = $"{GetBaseUrl()}/schedules-by-batch";
        return _shedulesBaseService.BuildUrl(url, kvp);
    }
}