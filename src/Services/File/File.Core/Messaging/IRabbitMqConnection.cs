using RabbitMQ.Client;

namespace File.Core.Messaging;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}