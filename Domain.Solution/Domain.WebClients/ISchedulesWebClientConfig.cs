namespace Schedules.WebClients;

public interface ISchedulesWebClientConfig
{
    string GetSchedulesApiBaseUrl();
    string GetApimSubscriptionKey();
    string GetSchedulesSbEndpointUrl();
    string GetSchedulesF2SyncQueueName();
}