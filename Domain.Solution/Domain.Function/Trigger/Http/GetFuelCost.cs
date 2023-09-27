namespace DomainName.Function.Triggers.Http
{
    public sealed class GetFuelCost
    {
        private string _message;

        [InjectService]
        public ApiGetSvc ApiGetSvc { get; private set; }

        [InjectService]
        public AppInsightsLogger InsightsLogger { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="apiGetSvc"> </param>
        public GetFuelCost(ApiGetSvc apiGetSvc, AppInsightsLogger insightsLogger)
        {
            ApiGetSvc = apiGetSvc ?? throw new ArgumentNullException(nameof(apiGetSvc));
            InsightsLogger = insightsLogger ?? throw new ArgumentNullException(nameof(insightsLogger));
        }

        // Use the class name to ensure the function name is unique
        [Function(nameof(GetFuelCost))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-fuel-cost")] HttpRequestData req,
              FunctionContext executionContext,
              string pickupZip,
              string dropoffZip,
              string equipmentType,
              CancellationToken hostCancellationToken = default)
        {
            // Create and Validate the request
            IGetRequest request = new GetFuelCostRequest(pickupZip, dropoffZip, equipmentType);

            if (!request.IsValid())
            {
                throw new FailedRequestException("Invalid Request");
            }

            // Async functions receive 2 cancellation tokens. One from the calling client and one
            // from the host (APIM). Get a linked ct source that will throw an
            // OperationCanceledException if it receives a cancellation request from either.
            var lts = CancellationTokenSource
                .CreateLinkedTokenSource(hostCancellationToken, executionContext.CancellationToken);
            int timeoutMS = 20000;
            lts.CancelAfter(timeoutMS);
            var ct = lts.Token;

            // throw and catch an exception if cancellation is requested
            ct.ThrowIfCancellationRequested();

            var functionResponse = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                // For passing in an instance of the response object
                GetFuelCostResponse response = new();

                // Get the result back as a self-validated json string
                var result = await ApiGetSvc.ExecuteAsync(request, response, ct);
                await functionResponse.WriteAsJsonAsync(result);

                // Return the json
                return functionResponse;
            }
            finally
            {
                lts.Dispose();
            }
        }
    }
}