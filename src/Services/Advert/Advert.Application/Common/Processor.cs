namespace Advert.Application.Common;

public abstract class Processor<TRequest, TResponse>
{
    private Processor<TRequest, TResponse>? _next;

    public Processor<TRequest, TResponse> SetNext(Processor<TRequest, TResponse> next)
    {
        _next = next;
        
        return next;
    }

    public async Task<TResponse?> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (CanHandle(request))
        {
            return await ProcessAsync(request, cancellationToken);
        }

        return _next != null ? await _next.HandleAsync(request, cancellationToken) : default;
    }

    protected abstract bool CanHandle(TRequest request);
    protected abstract Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);
}