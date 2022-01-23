using System.Text;
using MailSender.Worker.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class QueueClient
{

    private readonly RabbitMQOptions RabbitMQOptions;
    private readonly ILogger<QueueClient> Logger;

    public QueueClient(IOptions<RabbitMQOptions> rabbitMq, ILogger<QueueClient> logger)
    {
        ArgumentNullException.ThrowIfNull(rabbitMq, nameof(rabbitMq));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        Logger = logger;
        RabbitMQOptions = rabbitMq.Value;
    }

    public void Run(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQOptions.HostName,
            UserName = RabbitMQOptions.UserName,
            Password = RabbitMQOptions.Password
        };

        Logger.LogInformation("Starting connection with RabbitMQ");
        using var connection = factory.CreateConnection();

        Logger.LogInformation("Creating model.");

        using var channel = connection.CreateModel();

        Logger.LogInformation("Declaring queue.");
        channel.QueueDeclare(queue: "mail_sender", durable: false, exclusive: false, autoDelete: false, arguments: null);

        Logger.LogInformation("Waiting for messages.");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Logger.LogInformation(" [[{0}] Received {1}", DateTime.Now, message);
        };
        channel.BasicConsume(queue: "mail_sender", autoAck: true, consumer: consumer);

        Logger.LogInformation(" Press [enter] to exit.");
        Console.ReadLine();
    }
}