using System.Text;
using Advert.Application.Abstractions;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Advert.Infrastructure.Messaging;

public class RabbitMqMessagePublisher(IRabbitMqConnection connection) : IMessagePublisher
{
    public async Task PublishAsync<T>(string queueName, T message, CancellationToken cancellationToken = default)
    {
        await using var channel = await connection.Connection.CreateChannelAsync(cancellationToken: cancellationToken);   
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);
        
        var messageJson = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(messageJson);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, 
            routingKey: queueName, 
            body: body, 
            cancellationToken: cancellationToken);
    }
}