namespace Domain.Utility.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScanDomainAssemblies(this IServiceCollection services,
            ServiceDescriptorMergeStrategy mergeStrategy = ServiceDescriptorMergeStrategy.TryAdd)
        {
            // Scan Domain.Health assembly
            services.ScanAssembly(Assembly.Load("Domain.Function"), (Type _) => true, mergeStrategy);

            // Scan Domain.Interface assembly
            services.ScanAssembly(Assembly.Load("Domain.WebClients"), (Type _) => true, mergeStrategy);

            // Scan Domain.Utility assembly
            services.ScanAssembly(Assembly.Load("Domain.ConfigContext"), (Type _) => true, mergeStrategy);

            return services;
        }
    }
}