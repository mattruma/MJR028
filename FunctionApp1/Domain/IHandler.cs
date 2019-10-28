namespace FunctionApp1.Domain
{
    public interface IHandler<TRequest, TResponse>
    {
        TResponse Handle(
            TRequest request);
    }
}
