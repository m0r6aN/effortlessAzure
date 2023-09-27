namespace Schedules.WebClients.DTO
{
    public class AzureScheduleQueue
    {
        public DateTime QueueSubmittedTime { get; set; }

        public List<AzureScheduleQueueSchedule> Schedules { get; set; } = new List<AzureScheduleQueueSchedule>();
    }

    public class AzureScheduleQueueSchedule
    {
        public int Pkey { get; set; }

        public string ScheduleId { get; set; }        

        public string Status { get; set; }

        public int? AgentPkey { get; set; }

        public bool IsNobuySchedule { get; set; }

        public List<AzureScheduleQueueInvoice> Invoices { get; set; } = new List<AzureScheduleQueueInvoice>();

        public AzureScheduleQueueClient Client { get; set; }
    }

    public class AzureScheduleQueueInvoice
    {
        public int Pkey { get; set; }

        public int StatusId { get; set; }

        public decimal InvoiceAmount { get; set; }

        public DateTime? SubmittedDate { get; set; }
    }

    public class AzureScheduleQueueClient
    {
        public int Pkey { get; set; }

        public string Name { get; set; }

        public int StatusId { get; set; }

        public int? AccountRep { get; set; }

        public DateTime? TemporaryAdjustmentDate { get; set; }

        public int? TemporaryAdjustmentPKey { get; set; }

        public int? AdjustmentPKey { get; set; }

        public TimeSpan CutoffTime { get; set; }
    }
}
