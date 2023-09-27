using Domain.Utility.Extensions;

using DomainName.Function.Middleware;

using MF.Libraries.Data.Lanes.Context;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace OTR.Serverless.Invoices.Function
{
    /// <summary>
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static async Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(bldr =>
                {
                    bldr.UseMiddleware<GlobalExceptionHandlerMiddleware>();
                })

                .ConfigureAppConfiguration(cfg =>
                {
                    cfg.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
                    cfg.AddEnvironmentVariables();
                    cfg.AddAzureAppConfiguration(options =>
                    {
                        options.Connect("<AzureAppConfiguration_ConnectionString>")
                                .UseFeatureFlags();
                    });
                    cfg.AddAzureKeyVault(Settings.KeyVaultUri, null);
                })

                .ConfigureServices(services =>
                {
                    // SCANS THE DOMAIN ASSEMBLIES FOR CLASSES DECORATED WITH THE [RegisterService]
                    // ATTRIBUTE AND ADDS THEM TO THE SERVICES COLLECTION FOR DI
                    services.ScanDomainAssemblies();

                    // AVOID NESTED SERIALIZATION ERRORS
                    services.Configure<JsonSerializerOptions>(jso =>
                    {
                        jso.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        jso.PropertyNameCaseInsensitive = true;
                    });

                    // USE DB CONTEXT POOLING FOR THE DOMAIN DB
                    services.AddDbContextPool<DomainDbContext>(options =>
                        options.UseSqlServer(Settings.DomainDbConnection));

                    // REGULAR SCOPED ADD FOR THE REPORTING DB
                    services.AddDbContext<TableauDbContext>(options =>
                        options.UseSqlServer(Settings.DataExtractConnection),
                        ServiceLifetime.Scoped);

                    // FACTORIES
                    services.AddDbContextFactory<DomainDbContext>(options =>
                        options.UseSqlServer(Settings.DomainDbConnection));

                    // NAMED HTTP CLIENT
                    services.AddHttpClient("APIM", httpClient =>
                    {
                        httpClient.BaseAddress = new Uri(Settings.BaseApimUrl);
                        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "DomainName.Function");
                        httpClient.DefaultRequestHeaders.Add("KeyName", "KeyValue");

                        // Explicitly disable gzip encoding
                        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "identity");
                    })
                     .SetHandlerLifetime(TimeSpan.FromMinutes(5)); // this helps mitigate SNAT exhaustion

                    services.AddLogging(logging =>
                    {
                        logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
                    });

                    // FUTURE SERVICE BUS USAGE
                    //services.AddAzureClients(options =>
                    //{
                    //    options.AddWebPubSubServiceClient(Settings.EditInvoicesPubSubEndpoint, Settings.EditInvoicesPubSubHubName);
                    //});
                })

                .Build();

            await host.RunAsync();
        }
    }
}