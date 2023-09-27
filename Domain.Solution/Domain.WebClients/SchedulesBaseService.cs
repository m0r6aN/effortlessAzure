using Microsoft.AspNetCore.WebUtilities;

namespace Schedules.WebClients
{
    public class SchedulesBaseService :ISchedulesBaseService
    {
        private readonly ISchedulesWebClientConfig _schedulesWebClientConfig;
        private readonly HttpClient _httpClient;
        public SchedulesBaseService(ISchedulesWebClientConfig schedulesWebClientConfig,
            IHttpClientFactory clientFactory)
        {
            _schedulesWebClientConfig = schedulesWebClientConfig ?? throw new ArgumentNullException(nameof(schedulesWebClientConfig));
            _httpClient = clientFactory.CreateClient("SCHEDULE") ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public string BuildUrl(string baseUrl, IDictionary<string, string> keyValuePairs)
        {
            if (keyValuePairs == null || keyValuePairs.Count < 1)
                return baseUrl;

            var uri = new Uri(QueryHelpers.AddQueryString(baseUrl, keyValuePairs));
            return uri.OriginalString;
        }

        public async Task<string> HttpGetAsync(string url)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add(ConfigSettings.ApimSubscriptionKey, _schedulesWebClientConfig.GetApimSubscriptionKey());
            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }
    }
}
