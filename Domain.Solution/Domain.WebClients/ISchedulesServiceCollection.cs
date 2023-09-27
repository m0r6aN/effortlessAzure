using Microsoft.Extensions.DependencyInjection;

namespace Schedules.WebClients
{
    public static class SchedulesServiceCollection
    {
        public static void RegisterSchedules(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<ISchedulesUrlService, SchedulesUrlService>();
            services.AddTransient<ISchedulesBaseService, SchedulesBaseService>();
            services.AddTransient<ISchedulesWebClient, SchedulesWebClient>();
            services.AddTransient<ISchedulesWebClientConfig, SchedulesWebClientConfig>();
        }
    }
}
