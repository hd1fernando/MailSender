using MailSender.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MailSender.Api.Data.Context;
public class MailSenderDbContext : DbContext
{
    public MailSenderDbContext(DbContextOptions<MailSenderDbContext> options) : base(options)
    {

    }

    public DbSet<MailEntity> Mails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MailSenderDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}