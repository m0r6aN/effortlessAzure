namespace DomainName.Function.Domain.Value.Request
{
    public sealed class GetFuelCostRequest : RequestBase, IGetRequest
    {
        public bool isDistanceCalculated = false;

        public int fuelPriceReadings = default;

        public string dropOffZip = default;

        public string equipmentType = default;

        public string pickupZip = default;

        //public string date = default;
        //public string dropOffMarket = default;
        private decimal avgMilesPerGallon = 6.0m;

        private decimal distance = default;
        private decimal fuelCost = default;
        private decimal fuelCostStdDev = default;
        private FuelCostsByMarketResponse fuelCostsByMarketData = new();
        private string dropOffCity = default;
        private string dropOffState = default;
        private string pickupCity = default;
        private string pickupMarket = default;
        private string pickupState = default;
        public FuelCostResponse response { get; set; } = new();

        public GetFuelCostRequest(string PickupZip, string DropOffZip, string EquipmentType) : base()
        {
            pickupZip = PickupZip;
            dropOffZip = DropOffZip;
            equipmentType = EquipmentType;
        }

        /// <summary>
        /// Use Parallel.Invoke() if you need to ensure the order of execution
        /// </summary>
        /// <param name="ct"> </param>
        /// <returns> </returns>
        public async Task<string> FanOutAndInAsync(CancellationToken ct)
        {
            // parallel http requests
            List<Task> tasks = new List<Task>
            {
                GetPickupInsightsAsync(ct),
                GetDropOffInsightsAsync(ct),
                GetLanesInsightsAsync(ct),
                GetMarketLanesInsightsAsync(ct),
                GetMarketFuelCostsInsights(ct)
            };

            await Task.WhenAll(tasks);

            fuelCost = fuelCostsByMarketData.FuelCost;
            fuelCostStdDev = fuelCostsByMarketData.FuelCostStdDev;
            fuelPriceReadings = fuelCostsByMarketData.FuelPriceReadings;

            // Construct expected rate response
            response.ExpectedFuelCost = fuelCost * distance / avgMilesPerGallon;
            response.FuelPriceReadings = fuelPriceReadings;
            response.FuelPriceStdDev = fuelCostStdDev;
            response.ResponseStatus = HttpStatusCode.OK;

            // Return the json
            return JsonSerializer.Serialize(response);
        }

        /// <summary>
        /// Self-validation to ensure that the receiving client can parse the json
        /// </summary>
        /// <returns> </returns>
        public bool CanSerialize()
        {
            return this.ToJson().Length > 0;
        }

        public async Task GetLanesInsightsAsync(CancellationToken ct)
        {
            var parms = new Dictionary<string, string>
            {
                { "fromCity", pickupCity },
                { "fromState", pickupState},
                { "toCity", dropOffCity},
                { "toState", dropOffState},
            };

            ApiRepo.ActionUrl = HttpHelpers.BuildHttpGetUri("insights/locations", parms);
            string json = await ApiRepo.GetAsync(ct);

            var lanesData = JsonSerializer.Deserialize<LanesResponse>(json);

            if (lanesData.ResponseStatus != HttpStatusCode.OK)
            {
                throw new Exception("Invalid pickup location response");
            }

            distance = lanesData.Distance;
            isDistanceCalculated = true;
        }

        public async Task GetPickupInsightsAsync(CancellationToken ct)
        {
            // REDACTED
        }

        public async Task GetDropOffInsightsAsync(CancellationToken ct)
        {
            // REDACTED
        }

        public Task<Uri> ToUri()
        {
            UriBuilder builder = new UriBuilder(Settings.BaseApimUrl);
            builder.Port = 443;
            builder.Scheme = "https";
            builder.Path = "insights/fuelcosts";
            builder.Query = $"pickupZip={pickupZip},pickupCity{pickupCity}";
            return Task.FromResult(builder.Uri);
        }

        private async Task GetMarketLanesInsightsAsync(CancellationToken ct)
        {
            // REDACTED
        }

        private async Task GetMarketFuelCostsInsights(CancellationToken ct)
        {
            // REDACTED
        }
    }
}