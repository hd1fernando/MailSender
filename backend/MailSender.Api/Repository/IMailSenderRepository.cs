using MailSender.Api.Entities;

namespace MailSender.Api.Repository;

public interface IMailSenderRepository
{
    public Task SaveAsync(MailEntity mailEntity, CancellationToken cancellationToken = default);
}
