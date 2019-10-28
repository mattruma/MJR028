using System.Threading.Tasks;

namespace FunctionApp1.Domain
{
    public interface IAsyncHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(
            TRequest request);
    }
}
