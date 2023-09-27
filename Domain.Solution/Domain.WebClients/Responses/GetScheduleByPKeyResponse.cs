namespace Schedules.WebClients.Responses
{
    public class ScheduleByPKeyResponse
    {
        public Guid Id { get; set; }

        public int SchedulePKey { get; set; }

        public int? AgentPKey { get; set; }

        public int? AccountRep { get; set; }

        public string ScheduleId { get; set; }

        public int ClientPKey { get; set; }

        public string ClientName { get; set; }

        public int ClientStatusId { get; set; }

        public int StatusId { get; set; }

        public string PriorityPayType { get; set; }

        public int TotalApproved { get; set; }

        public int TotalNotApproved { get; set; }

        public decimal? OffsetAmount { get; set; }

        public decimal? PendingAmount { get; set; }

        public decimal? VendorDebtAmount { get; set; }

        public decimal? TotalDebtAmount { get; set; }

        public TimeSpan ClientCutoffTime { get; set; }

        public DateTime? ClientTemporaryAdjustmentDate { get; set; }

        public int? ClientTemporaryAdjustmentPKey { get; set; }

        public int? ClientAdjustmentPKey { get; set; }

        public DateTime UpdatedDateUTC { get; set; }

        public string Checksum { get; set; }

        public string Assignee { get; set; }

        public ScheduleStatuses Status { get; set; }
    }

    public class ScheduleStatuses
    {
        public Guid Id { get; set; }

        public int StatusId { get; set; }

        public string Description { get; set; }
    }
}
