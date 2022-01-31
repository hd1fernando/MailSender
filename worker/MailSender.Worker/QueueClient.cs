using System.Text;
using System.Text.Json;
using MailSender.Worker.Models;
using MailSender.Worker.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class QueueClient
{

    static AutoResetEvent AutoEvent = new AutoResetEvent(false);
    private readonly RabbitMQOptions RabbitMQOptions;
    private readonly ILogger<QueueClient> Logger;
    private readonly IEmailSender MailSender;

    public QueueClient(IOptions<RabbitMQOptions> rabbitMq, ILogger<QueueClient> logger, IEmailSender mailSender = null)
    {
        ArgumentNullException.ThrowIfNull(rabbitMq, nameof(rabbitMq));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        Logger = logger;
        RabbitMQOptions = rabbitMq.Value;
        MailSender = mailSender;
    }

    public void Run(string[] args)
    {
        Logger.LogInformation("{0},{1},{2}", RabbitMQOptions.HostName, RabbitMQOptions.UserName, RabbitMQOptions.Password);
        Thread.Sleep(TimeSpan.FromSeconds(5));

        var factory = new ConnectionFactory()
        {
            HostName = RabbitMQOptions.HostName,
            UserName = RabbitMQOptions.UserName,
            Password = RabbitMQOptions.Password,
            Port = 5672
        };

        Logger.LogInformation("Starting connection with RabbitMQ");
        using var connection = factory.CreateConnection();

        Logger.LogInformation("Creating model.");

        using var channel = connection.CreateModel();

        Logger.LogInformation("Declaring queue.");
        channel.QueueDeclare(queue: "mail_sender", durable: false, exclusive: false, autoDelete: false, arguments: null);

        Logger.LogInformation("Waiting for messages.");
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Logger.LogInformation(" [[{0}] Received {1}", DateTime.UtcNow, message);

            var email = JsonSerializer.Deserialize<MailViewModel>(message) ?? throw new Exception($"{nameof(MailViewModel)} can't be null in the deseralization.");

            await MailSender.SendAsync(email ?? throw new Exception($"{nameof(email)} can't be null"));
        };
        channel.BasicConsume(queue: "mail_sender", autoAck: true, consumer: consumer);

        Logger.LogInformation(" Awating for messages.");
        AutoEvent.WaitOne();
    }
}