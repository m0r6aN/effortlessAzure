using Microsoft.Extensions.Configuration;
using Quickwire.Attributes;

namespace Domain.ConfigContext
{
    [RegisterService]
    public sealed class ConfigContext
    {
        private readonly IConfiguration ConfigurationRoot;
        public DomainConfiguration DomainConfiguration { get; private set; }

        public ConfigContext(IConfiguration configuration)
        {
            ConfigurationRoot = configuration;

            // Bind DomainConfiguration
            DomainConfiguration = new DomainConfiguration();

            ConfigurationRoot.Bind(configuration);

            ConfigurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                .Bind(DomainConfiguration.TransientFaultOptions);

            ConfigurationRoot.GetSection(nameof(Apim))
                .Bind(DomainConfiguration.KucoinApi);

            ConfigurationRoot.GetSection(nameof(TimerSettings))
                .Bind(DomainConfiguration.TimerSettings);

            ConfigurationRoot.GetSection(nameof(LoggingOptions))
                .Bind(DomainConfiguration.LoggingOptions);

            ConfigurationRoot.GetSection(nameof(DatabaseSettings))
                .Bind(DomainConfiguration.DatabaseSettings);
        }
    }
}