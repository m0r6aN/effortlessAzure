namespace DomainName.Interface.Service.Get
{
    public interface IGetService
    {
        public Task<IFunctionResponse> ExecuteAsync(IGetRequest request, CancellationToken token);
    }
}