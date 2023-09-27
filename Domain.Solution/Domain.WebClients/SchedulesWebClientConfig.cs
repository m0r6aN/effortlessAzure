using Microsoft.Extensions.Configuration;

namespace Schedules.WebClients;

public class SchedulesWebClientConfig : ISchedulesWebClientConfig
{
    private readonly IConfiguration _configuration;

    public SchedulesWebClientConfig(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string GetSchedulesApiBaseUrl() => _configuration.GetValue<string>(ConfigSettings.SchedulesBaseUrlString) ?? throw new InvalidOperationException(nameof(ConfigSettings.SchedulesBaseUrlString));

    public string GetApimSubscriptionKey() => _configuration.GetValue<string>(ConfigSettings.ApimSubscriptionKey) ?? throw new InvalidOperationException(nameof(ConfigSettings.ApimSubscriptionKey));

    public string GetSchedulesSbEndpointUrl() =>
        _configuration.GetValue<string>(ConfigSettings.SchedulesServiceBusEndpointUrl) ?? throw new InvalidOperationException(nameof(ConfigSettings.SchedulesServiceBusEndpointUrl));

    public string GetSchedulesF2SyncQueueName() => _configuration.GetValue<string>(ConfigSettings.SchedulesF2SyncQueueName) ?? throw new InvalidOperationException(nameof(ConfigSettings.SchedulesF2SyncQueueName));
}