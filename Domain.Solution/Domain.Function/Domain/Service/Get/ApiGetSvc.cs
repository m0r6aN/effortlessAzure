﻿namespace DomainName.Function.Services.Get.API
{
    [RegisterService]
    public sealed class ApiGetSvc
    {
        [InjectService]
        public ApiRepository ApiRepo { get; private set; }

        [InjectService]
        public HttpHelpers HttpHelpers { get; private set; }

        public ApiGetSvc(ApiRepository apiRepository, HttpHelpers httpHelpers)
        {
            ApiRepo = apiRepository ?? throw new ArgumentNullException(nameof(apiRepository));
            HttpHelpers = HttpHelpers ?? throw new ArgumentNullException(nameof(httpHelpers));

            HttpHelpers.BaseUrl = Settings.BaseApimUrl;
        }

        public async Task<string> ExecuteAsync(IGetRequest request, IFunctionResponse response, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // The smart request objects handles it's own business and returns a json string
            string json = await request.FanOutAndInAsync(ct);

            // The response object ensures that the response is valid before it's returned to the trigger
            if (response.TryDeserialize(json))
            {
                return json;
            }

            throw new FailedRequestException("Failed to deserialize the response from the API");
        }
    }
}