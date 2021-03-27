namespace Sedir.Server
{
    public interface ISedirHandler<TRequest, TResponse>
        where TResponse : HandlerResponse
    {
        public TResponse Accept(TRequest request);
    }
}