
using MailSender.Worker.Models;
using MailSender.Worker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var services = new ServiceCollection();
ConfigureServices(services);

var run = services?.BuildServiceProvider()
    ?.GetService<QueueClient>();

ArgumentNullException.ThrowIfNull(run, nameof(run));
run?.Run(args);


void ConfigureServices(IServiceCollection services)
{
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.AddDebug();
    });

    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

    services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));
    services.Configure<MailSettingsOptions>(configuration.GetSection(nameof(MailSettingsOptions)));
    
    services.AddTransient<QueueClient>();
    services.AddTransient<IEmailSender,EmailSender>();
}
