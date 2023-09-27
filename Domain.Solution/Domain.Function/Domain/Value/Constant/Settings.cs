namespace DomainName.Domain.Value.Constant
{
    internal static class Settings
    {
        public static string DomainDbConnection
           = Environment.GetEnvironmentVariable("DOMAIN_DB_CONNECTION");

        public static string DataExtractConnection
            = Environment.GetEnvironmentVariable("DATA_EXTRACT_CONNECTION");

        public static string DataWriteConnection
            = Environment.GetEnvironmentVariable("DATA_WRITE_CONNECTION");

        public static string BaseApimUrl
            = Environment.GetEnvironmentVariable("BASE_APIM_URL");

        public static Uri KeyVaultUri
            = new Uri(Environment.GetEnvironmentVariable("KEY_VAULT_URI"));
    }
}