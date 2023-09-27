namespace DomainName.Function.Domain.Value.Response
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class ShippingRateResponse : ResponseBase, IFunctionResponse
    {
        public bool TryDeserialize(string json)
        {
            return json.CanDeserialize(typeof(RateResponse));
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class RateResponse
    {
        public decimal ExpectedRate { get; set; }
        public int InvoiceCount { get; set; }
        public decimal RateStdDev { get; set; }
        public decimal Distance { get; set; }
        public HttpStatusCode ResponseStatus { get; set; }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}