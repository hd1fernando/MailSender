using MailSender.Api.Entities;
using MailSender.Api.Repository;

namespace MailSender.Api.Services;

public class MailSenderService : IMailSenderService
{
    private readonly IMailSenderRepository MailSenderRepository;
    private readonly IQueueServices QueueServices;
    public MailSenderService(IMailSenderRepository mailSenderRepository, IQueueServices queueServices)
    {
        MailSenderRepository = mailSenderRepository;
        QueueServices = queueServices;
    }

    public async Task SendMailAsync(MailEntity mailEntity, CancellationToken cancellationToken = default)
    {
        await MailSenderRepository.SaveAsync(mailEntity, cancellationToken);
        QueueServices.AddToQueue(mailEntity);
    }

}