namespace Schedules.WebClients.Requests
{
    /// <summary>
    ///The Schedule Details Request 
    /// <author>Jeremiah Liscum</author>
    /// </summary>
    public class ScheduleDetailsRequest
    {
        public int Pkey { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VendorPayableTransactions { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal EscrowAmount { get; set; }
        public string Notes { get; set; }
    }
}
