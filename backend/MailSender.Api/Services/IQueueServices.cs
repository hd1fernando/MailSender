using MailSender.Api.Entities;

namespace MailSender.Api.Services;

public interface IQueueServices
{
    public void AddToQueue(MailEntity mailEntity);
}
