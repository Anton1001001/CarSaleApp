using File.Core.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace File.Core.Messaging;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable, IAsyncDisposable
{
    public IConnection Connection { get; private set; } = null!;
    private readonly RabbitMqOptions _options;

    public RabbitMqConnection(IOptions<RabbitMqOptions> options)
    {
        _options = options.Value;
        InitializeConnection().GetAwaiter().GetResult();
    }

    private async Task InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _options.Hostname,
        };
        Connection = await factory.CreateConnectionAsync();
    }

    public void Dispose()
    {
        Connection.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await Connection.DisposeAsync();
    }
}