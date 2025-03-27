using System.Text;
using File.Core.CQRS.Commands.RemoveFile;
using File.Core.CQRS.Queries.GetUnusedFilesIds;
using File.Core.Options;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace File.Core.Messaging;

public class RabbitMqBackgroundWorker : BackgroundService
{
    private readonly IChannel _channel;
    private readonly IConnection _connection;
    private readonly MessageQueueOptions _options;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMqBackgroundWorker(
        IRabbitMqConnection connection,
        IOptions<MessageQueueOptions> options,
        ILogger<RabbitMqBackgroundWorker> logger, 
        IServiceScopeFactory scopeFactory)
    {
        _options = options.Value;
        _scopeFactory = scopeFactory;
        _connection = connection.Connection;
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
        _channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null).GetAwaiter().GetResult();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += ConsumeAsync;
        _channel.BasicConsumeAsync(_options.QueueName, false, consumer, cancellationToken: stoppingToken);
        return Task.CompletedTask;
    }

    private async Task ConsumeAsync(object sender, BasicDeliverEventArgs @event)
    {
        var body = @event.Body.ToArray();
        var messageJson = Encoding.UTF8.GetString(body);
        var ids = JsonConvert.DeserializeObject<List<int>>(messageJson);

        if (ids is not null)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediatorSender = scope.ServiceProvider.GetRequiredService<ISender>();
            var inactiveFilesResponse = await mediatorSender
                .Send(new GetUnusedFilesIdsQuery(ids), CancellationToken.None);
            await mediatorSender.Send(new RemoveFileCommand(inactiveFilesResponse.Ids), CancellationToken.None);
        }

        await _channel.BasicAckAsync(@event.DeliveryTag, false);
    }

    public override void Dispose()
    {
        _connection.Dispose();
        _channel.Dispose();
        base.Dispose();
    }
}