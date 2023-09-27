namespace DomainName.Function.Domain.Repository.API
{
    [RegisterService]
    public sealed class ApiRepository : IApiRepository
    {
        private readonly HttpClient _httpClient;
        private HttpStatusCode statusCode = HttpStatusCode.OK;

        /// <summary>
        /// Supplied by the IRequest object in the calling service
        /// </summary>
        public Uri ActionUrl { get; set; }

        public ApiRepository(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory.CreateClient("APIM")
                ?? throw new ArgumentNullException(nameof(httpClientFactory));

        /// <summary>
        /// </summary>
        /// <param name="ct"> </param>
        /// <returns> </returns>
        /// <exception cref="FailedRequestException"> </exception>
        public async Task<string> GetAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead;
            using HttpRequestMessage request = new(HttpMethod.Get, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, completionOption, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(ct) ?? string.Empty;
        }

        public async Task<string> PostAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync() ?? string.Empty;
        }
    }
}