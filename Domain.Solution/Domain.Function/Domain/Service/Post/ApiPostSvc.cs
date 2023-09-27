namespace DomainName.Function.Services.Post.API
{
    // this will be picked up by ScanCurrentAssembly in Program.cs
    [RegisterService]
    public sealed class ApiPostSvc
    {
        [InjectService]
        public DbRepository DbRepo { get; private set; }

        // use dependency injection to get a reference to the httpClientFactory and create an
        // instance of our named HttpClient that was defined in Program.cs
        public ApiPostSvc(DbRepository dbRepository) =>
            DbRepo = dbRepository
                ?? throw new ArgumentNullException(nameof(dbRepository));

        // in almost all circumstances each service should have a single ExecuteAsync method
        public async Task<string> ExecuteAsync(string jsonRequest, IPostRequest request, IFunctionResponse response,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            dynamic entity = request.ToEntity(jsonRequest);
            string jsonResponse = await DbRepo.UpsertEntityAsync(entity, ct);

            return response.TryDeserialize(jsonResponse) ? jsonResponse :
                throw new FailedRequestException("Failed to deserialize the response from the API");
        }
    }
}