using MailSender.Api.Entities;

namespace MailSender.Api.Services;
public interface IMailSenderService
{
    public Task SendMailAsync(MailEntity mail);
}
