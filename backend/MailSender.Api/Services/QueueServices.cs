using System.Text;
using System.Text.Json;
using MailSender.Api.Entities;
using MailSender.Api.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MailSender.Api.Services;

public class QueueServices : IQueueServices
{
    private readonly IOptions<RabbitMQOptions> RabbitMqOptions;
    private readonly ILogger<QueueServices> Logger;
    public QueueServices(IOptions<RabbitMQOptions> rabbitMqOptions, ILogger<QueueServices> logger)
    {
        RabbitMqOptions = rabbitMqOptions;
        Logger = logger;
    }

    public void AddToQueue(MailEntity mailEntity)
    {
        Logger.LogInformation("Connecting with: {0},{1},{2}", RabbitMqOptions.Value.HostName, RabbitMqOptions.Value.UserName, RabbitMqOptions.Value.Password);
        Thread.Sleep(TimeSpan.FromSeconds(5));

        Logger.LogInformation("Starting RabbitMQ connection.");
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMqOptions.Value.HostName,
            UserName = RabbitMqOptions.Value.UserName,
            Password = RabbitMqOptions.Value.Password,
            Port = 5672
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "mail_sender", durable: false, exclusive: false, autoDelete: false, arguments: null);

        string message = JsonSerializer.Serialize(mailEntity);
        var body = Encoding.UTF8.GetBytes(message);

        Logger.LogInformation("Publishing message.");
        channel.BasicPublish(exchange: "", routingKey: "mail_sender", basicProperties: null, body: body);

        Logger.LogInformation($" [{DateTime.UtcNow}] Sent {message}.");
    }
}
