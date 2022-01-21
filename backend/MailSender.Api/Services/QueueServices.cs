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

    public QueueServices(IOptions<RabbitMQOptions> rabbitMqOptions)
    {
        RabbitMqOptions = rabbitMqOptions;
    }

    public void AddToQueue(MailEntity mailEntity)
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMqOptions.Value.HostName,
            UserName = RabbitMqOptions.Value.UserName,
            Password = RabbitMqOptions.Value.Password
        };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "mail_sender", durable: false, exclusive: false, autoDelete: true, arguments: null);

        string message = JsonSerializer.Serialize(mailEntity);

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

        Console.WriteLine(" [x] Sent message '{0}'", message);
    }
}
