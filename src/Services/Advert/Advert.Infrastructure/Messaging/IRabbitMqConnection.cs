using RabbitMQ.Client;

namespace Advert.Infrastructure.Messaging;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}