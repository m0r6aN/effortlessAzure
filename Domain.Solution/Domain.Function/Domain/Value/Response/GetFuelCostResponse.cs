namespace DomainName.Function.Domain.Value.Response
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class GetFuelCostResponse : ResponseBase, IFunctionResponse
    {
        /// <summary>
        /// Validate that the json returned from the repository can be deserialized into this type
        /// instead of returning it as a POCO that will have to be serialized before returning to
        /// the client.
        /// </summary>
        /// <param name="json"> </param>
        /// <returns> </returns>
        public bool TryDeserialize(string json)
        {
            return json.CanDeserialize(typeof(FuelCostResponse));
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class FuelCostResponse
    {
        public decimal ExpectedFuelCost;
        public int FuelPriceReadings;
        public decimal FuelPriceStdDev;
        public HttpStatusCode ResponseStatus;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class LocationsResponse
    {
        public string city;
        public string state;
        public string market;
        public HttpStatusCode ResponseStatus;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class LanesResponse
    {
        public decimal Distance;
        public HttpStatusCode ResponseStatus;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class MarketLanesResponse
    {
        public decimal RatePerMile;
        public decimal RateStdDev;
        public decimal Distance;
        public int NumInvoices;
        public string EquipmentType;
        public decimal Rate;
        public HttpStatusCode ResponseStatus;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class FuelCostsByMarketResponse
    {
        public decimal FuelCost;
        public decimal FuelCostStdDev;
        public int FuelPriceReadings;
        public HttpStatusCode ResponseStatus;

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}