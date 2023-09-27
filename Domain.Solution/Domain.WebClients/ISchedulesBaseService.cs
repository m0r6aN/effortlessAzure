namespace Schedules.WebClients;

public interface ISchedulesBaseService
{
    string BuildUrl(string baseUrl, IDictionary<string, string> keyValuePairs);

    Task<string> HttpGetAsync(string url);
}