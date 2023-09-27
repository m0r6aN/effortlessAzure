namespace MF.DomainName.Health.Database
{
    /// <summary>
    /// Checks for DbContext connectivity and degraded response times
    /// </summary>
    /// <typeparam name="TContext"> </typeparam>
    public sealed class DbContextHealthCheck<TContext> where TContext : DbContext, IHealthCheck
    {
        private readonly DomainDbContext _invoicesDbContext;

        public DbContextHealthCheck(DbContext dbContext)
        {
            _invoicesDbContext = (DomainDbContext)dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                bool canQuery = _invoicesDbContext.RateConInfo.Any(i => i.Pkey != 0);

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    return HealthCheckResult.Degraded();
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}