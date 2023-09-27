namespace Schedules.WebClients
{
    public class BaseUrlService
    {
        private readonly ISchedulesWebClientConfig _webClientConfig;

        public BaseUrlService(ISchedulesWebClientConfig ledgerWebClientConfig)
        {
            _webClientConfig = ledgerWebClientConfig ?? throw new ArgumentNullException(nameof(ledgerWebClientConfig));
        }

        public string GetBaseUrl()
        {
            return _webClientConfig.GetSchedulesApiBaseUrl();
        }

        public string GetSchedulesServiceBusBaseEndPoint()
        {
            return _webClientConfig.GetSchedulesSbEndpointUrl();
        }
    }
}
