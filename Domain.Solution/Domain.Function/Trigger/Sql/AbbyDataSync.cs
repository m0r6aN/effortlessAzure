using MF.Libraries.Data.Lanes.Entity;

namespace DomainName.Function.Triggers.Sql
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/azure/azure-functions/functions-bindings-azure-sql-trigger?tabs=in-process%2Cportal&pivots=programming-language-csharp </summary>
    public static class AbbyDataSync
    {
        [Function(nameof(AbbyDataSync))]
        public static async Task SyncAbbyDataAsync(
            [SqlTrigger("dbo.RateConInfo", "DATA_EXTRACT_CONNECTION")] IReadOnlyList<SqlChange<RateConInfo>> changes,
            [Sql("dbo.ExtractAbbyRateConData", "DATA_EXTRACT_CONNECTION")] IEnumerable<AbbyyLoadLocations> infos,
            [Sql("dbo.AbbyyLoadLocations", "LOCAL_DB")] IAsyncCollector<AbbyyLoadLocations> ratecons)

        {
            infos.ForEach(async i => await ratecons.AddAsync(i));
        }
    }
}