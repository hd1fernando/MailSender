using MailSender.Api.Data.Context;
using MailSender.Api.Models;
using MailSender.Api.Repository;
using MailSender.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;


builder.Services.AddDbContext<MailSenderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IMailSenderRepository, MailSenderRepository>();
builder.Services.AddTransient<IMailSenderService, MailSenderService>();
builder.Services.AddTransient<IQueueServices, QueueServices>();

builder.Services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using var scope =  app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<MailSenderDbContext>();
context.Database.Migrate();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();