using MailSender.Api.Entities;
using MailSender.Api.Repository;

namespace MailSender.Api.Services;

public class MailSenderService : IMailSenderService
{
    private  readonly IMailSenderRepository MailSenderRepository;

    public MailSenderService(IMailSenderRepository mailSenderRepository)
    {
        MailSenderRepository = mailSenderRepository;
    }

    public async Task SendMailAsync(MailEntity mailEntity)
    {
        await MailSenderRepository.SaveAsync(mailEntity);
    }
}