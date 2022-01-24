using System.Net;
using System.Net.Mail;
using MailSender.Worker.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MailSender.Worker.Services;
public interface IEmailSender
{
    public Task SendAsync(MailViewModel mail);
}

public class EmailSender : IEmailSender
{
    private readonly MailSettingsOptions MailSettingsOptions;
    private readonly ILogger<EmailSender> Logger;

    public EmailSender(IOptions<MailSettingsOptions> mailSettingsOptions, ILogger<EmailSender> mailSender)
    {
        MailSettingsOptions = mailSettingsOptions.Value;
        Logger = mailSender;
    }

    public async Task SendAsync(MailViewModel email)
    {
        PreConditions(email);

        try
        {

            var mail = new MailMessage
            {
                From = new MailAddress(MailSettingsOptions.UserEmail ?? throw new Exception($"{nameof(MailSettingsOptions.UserEmail)} is required."), "Mail sender test")
            };

            mail.To.Add(new MailAddress(email.Destiny ?? throw new Exception($"{nameof(email.Destiny)} is required.")));
            mail.CC.Add(new MailAddress("fernando@fernando.test"));

            mail.Subject = email.Subject;
            mail.Body = email.Content;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;


            using var smtp = new SmtpClient(MailSettingsOptions.Domain, MailSettingsOptions.Port);
            smtp.Credentials = new NetworkCredential(MailSettingsOptions.UserEmail, MailSettingsOptions.UserPassword);
            smtp.EnableSsl = true;

            Logger.LogInformation($"Sending mail message to {email.Destiny}");
            await smtp.SendMailAsync(mail);
        }
        catch (Exception e)
        {
            Logger.LogError($"Something went wrong while trying send email for {email.Destiny}\n {e.Message}");
        }
    }

    private void PreConditions(MailViewModel mail)
    {
        ThrowIfNullOrEmpty(mail.Subject, $"{nameof(mail.Subject)} is required.");
        ThrowIfNullOrEmpty(mail.Content, $"{nameof(mail.Content)} is required.");
        ThrowIfNullOrEmpty(mail.Destiny, $"{nameof(mail.Destiny)} is required.");
    }

    private void ThrowIfNullOrEmpty(string? subject, string? errorMessage)
    {
        if (string.IsNullOrEmpty(subject)) throw new Exception(errorMessage);
    }
}