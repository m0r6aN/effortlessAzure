using Newtonsoft.Json;

namespace Schedules.WebClients.Requests
{
    /// <summary>
    /// HTTP Request Object used during a BlockInvoicesRequest
    /// </summary>
    [JsonObject]
    public class SchedulesByAgentsSearchRequest
    {
        /// <summary>
        /// The Header Button Filters
        /// </summary>
        [JsonProperty("ScheduleHeaderFilter")]
        public ScheduleHeaderFilter ScheduleHeaderFilter { get; set; }

        /// <summary>
        /// List of Agent PKeys
        /// </summary>
        [JsonProperty("agentPkeys")]
        public List<string> AgentPkeys { get; set; }

        /// <summary>
        /// Search expression
        /// </summary>
        [JsonProperty("search")]
        public string Search { get; set; }

        /// <summary>
        /// Sort expression
        /// </summary>
        [JsonProperty("sort")]
        public IListSort[] Sort { get; set; }

        /// <summary>
        /// Filter expression
        /// </summary>
        [JsonProperty("filter")]
        public IListFilter[] Filter { get; set; }

        /// <summary>
        /// List of Client Statuses
        /// </summary>
        [JsonProperty("clientStatuses")]
        public List<int> ClientStatuses { get; set; }

        /// <summary>
        /// List of Statuses
        /// </summary>
        [JsonProperty("statuses")]
        public List<int> Statuses { get; set; }
    }

    /// <summary>
    /// HTTP Request Object
    /// </summary>
    [JsonObject]
    public class IListFilter
    {
        /// <summary>
        /// Filter Field
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// Filter Operator
        /// </summary>
        [JsonProperty("operator")]
        public string Operator { get; set; }

        /// <summary>
        /// Filter Value
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// HTTP Request Object
    /// </summary>
    [JsonObject]
    public class IListSort
    {
        /// <summary>
        /// Sort Field
        /// </summary>
        [JsonProperty("field")]
        public string Field { get; set; }

        /// <summary>
        /// Sort Order
        /// </summary>
        [JsonProperty("order")]
        public string Order { get; set; }
    }
}
