namespace DomainName.Function.Domain.Value.Request
{
    public sealed class GetRateRequest : RequestBase, IGetRequest
    {
        private readonly RateResponse _rateResponse = new RateResponse();
        private readonly LocationsResponse _pickupLocationsData;
        private readonly FuelCostsByMarketResponse _fuelCostsByMarketData = new();

        private readonly int _invoiceCount = 0;
        private readonly decimal _distance = 0.0m;
        private readonly decimal _ratePerMile = 0.0m;
        private readonly decimal _rateStdDev = 0.0m;
        private readonly string _pickupZip = default;
        private readonly string _dropOffZip = default;
        private readonly string _equipmentType = default;
        private readonly bool _isDistanceCalculated = false;

        private string _pickupCity = default;
        private string _pickupState = default;
        private string _pickupMarket = default;
        private string _dropOffCity = default;
        private string _dropOffState = default;
        private string _dropOffMarket = default;
        private string _pickupLocationResponseStr = default;
        private DateOnly _date;

        public FuelCostResponse fuelCostResponse { get; set; } = new();

        public GetRateRequest(string PickupZip, string DropOffZip, string EquipmentType, DateOnly date) : base()
        {
            _pickupZip = PickupZip ?? throw new ArgumentNullException(nameof(PickupZip));
            _dropOffZip = DropOffZip ?? throw new ArgumentNullException(nameof(DropOffZip));
            _equipmentType = EquipmentType ?? throw new ArgumentNullException(nameof(EquipmentType));
            _date = date;
        }

        /// <summary>
        /// Use Parallel.Invoke() if you need to ensure the order of execution
        /// </summary>
        /// <param name="ct"> </param>
        /// <returns> </returns>
        public async Task<string> FanOutAndInAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // parallel http requests
            List<Task> tasks = new List<Task>
            {
                GetLocationInsightsAsync(_dropOffZip, ct),
                GetPickupInsightsAsync(ct),
                GetDropOffInsightsAsync(ct),
                GetMarketLanesResponse(_pickupMarket, _dropOffMarket, _date.ToString(), _equipmentType, ct),
                GetLaneRate(_pickupZip, _pickupMarket, _dropOffZip, _dropOffMarket, _date.ToString(), _equipmentType, ct)
            };

            await Task.WhenAll(tasks);

            var fuelCost = _fuelCostsByMarketData.FuelCost;
            var fuelCostStdDev = _fuelCostsByMarketData.FuelCostStdDev;
            var fuelPriceReadings = _fuelCostsByMarketData.FuelPriceReadings;

            // Construct expected rate response
            _rateResponse.ExpectedRate = fuelCost * _distance / _ratePerMile;
            _rateResponse.RateStdDev = fuelCostStdDev;
            _rateResponse.ResponseStatus = HttpStatusCode.OK;

            return JsonSerializer.Serialize(_rateResponse);
        }

        public bool CanSerialize()
        {
            return this.ToJson().Length > 0;
        }

        public async Task GetLocationInsightsAsync(string zipCode, CancellationToken ct)
        {
            var parms = new Dictionary<string, string>
            {
                { "zipCode", zipCode }
            };

            ApiRepo.ActionUrl = HttpHelpers.BuildHttpGetUri("insights/location", parms);
            _pickupLocationResponseStr = await ApiRepo.GetAsync(ct);
        }

        public async Task GetPickupInsightsAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var parms = new Dictionary<string, string>
            {
                { "zipCode", _pickupZip }
            };

            ApiRepo.ActionUrl = HttpHelpers.BuildHttpGetUri("insights/locations", parms);
            string json = await ApiRepo.GetAsync(ct);

            var pickupData = JsonSerializer.Deserialize<LocationsResponse>(json);

            if (pickupData.ResponseStatus != HttpStatusCode.OK)
            {
                throw new Exception("Invalid pickup location response");
            }

            _pickupCity = pickupData.city;
            _pickupState = pickupData.state;
            _pickupMarket = pickupData.market;
        }

        public async Task GetDropOffInsightsAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var parms = new Dictionary<string, string>
            {
                { "zipCode", _dropOffZip }
            };

            ApiRepo.ActionUrl = HttpHelpers.BuildHttpGetUri("insights/locations", parms);
            string json = await ApiRepo.GetAsync(ct);

            var dropOffData = JsonSerializer.Deserialize<LocationsResponse>(json);

            if (dropOffData.ResponseStatus != HttpStatusCode.OK)
            {
                throw new Exception("Invalid pickup location response");
            }

            _dropOffCity = dropOffData.city;
            _dropOffState = dropOffData.state;
            _dropOffMarket = dropOffData.market;
        }

        public Task<Uri> ToUri()
        {
            throw new NotImplementedException();
        }

        private async Task<MarketLanesResponse> Get3ZipLanesResponse(string pickupZip, string dropOffZip, string date,
            string equipmentType, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var pickup3Zip = Convert.ToInt32(pickupZip[..3]);
            var dropOff3Zip = Convert.ToInt32(dropOffZip[..3]);
            var responseStr = "redacted";
            var result = JsonConvert.DeserializeObject<MarketLanesResponse>(responseStr);
            return result;
        }

        private async Task GetMarketLanesInsightsAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            //REDACTED
            return;
        }

        private async Task<MarketLanesResponse> GetMarketLanesResponse(string pickupMarket, string dropOffMarket,
            string date, string equipmentType, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            //REDACTED
            return new MarketLanesResponse();
        }

        private async Task<MarketLanesResponse> GetLaneRate(string pickupZip, string pickupMarket, string dropOffZip,
            string dropOffMarket, string date, string equipmentType, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            //REDACTED
            return new MarketLanesResponse();
        }
    }
}