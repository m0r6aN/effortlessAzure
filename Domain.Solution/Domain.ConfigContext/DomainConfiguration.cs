namespace Domain.ConfigContext
{
    public sealed class DomainConfiguration
    {
        public DatabaseSettings DatabaseSettings { get; private set; } = new();
        public Apim KucoinApi { get; private set; } = new();
        public LoggingOptions LoggingOptions { get; private set; } = new();
        public TransientFaultHandlingOptions TransientFaultOptions { get; private set; } = new();
        public TimerSettings TimerSettings { get; private set; } = new();
    }

    public sealed class TransientFaultHandlingOptions
    {
        public bool Enabled { get; set; }
        public TimeSpan AutoRetryDelay { get; set; }
    }

    public class Apim
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiPassphrase { get; set; }
        public string ApiBaseUrl { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }

    public sealed class LoggingOptions
    {
        public LogLevelOptions LogLevel { get; set; }
    }

    public sealed class LogLevelOptions
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }
        public string MicrosoftHostingLifetime { get; set; }
    }

    public sealed class TimerSettings
    {
        public int KucoinStreamInterval { get; set; }
        public int TokenMetricsPricesInterval { get; set; }
        public int TokenMetricsGradesInterval { get; set; }
        public int TokenMetricsDaysBack { get; set; }
    }
}