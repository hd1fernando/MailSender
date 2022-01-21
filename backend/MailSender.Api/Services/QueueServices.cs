using System.Text;
using MailSender.Api.Entities;
using RabbitMQ.Client;

namespace MailSender.Api.Services;

public class QueueServices : IQueueServices
{
    public void AddToQueue(MailEntity mailEntity)
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "123456" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "mail_sender", durable: false, exclusive: false, autoDelete: true, arguments: null);

        string message = "testing";

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

        Console.WriteLine(" [x] Sent message '{0}'", message);
    }
}
