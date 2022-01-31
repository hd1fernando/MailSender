using MailSender.Worker.Models;

namespace MailSender.Worker.Services;
public interface IEmailSender
{
    public Task SendAsync(MailViewModel mail);
}