using MailSender.Api.Data.Context;
using MailSender.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Api.Repository;

public class MailSenderRepository : IMailSenderRepository
{
    private MailSenderDbContext Context;
    private DbSet<MailEntity> Set;
    
    public MailSenderRepository(MailSenderDbContext dbContext)
    {
        Context = dbContext;
        Set = dbContext.Set<MailEntity>();
    }

    public async Task SaveAsync(MailEntity mailEntity)
    {
        await Set.AddAsync(mailEntity);
        await Context.SaveChangesAsync();
    }
}