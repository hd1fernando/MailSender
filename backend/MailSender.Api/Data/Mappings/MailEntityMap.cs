using MailSender.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailSender.Api.Data.Mappings;
public class MailEntityMap : IEntityTypeConfiguration<MailEntity>
{
    public void Configure(EntityTypeBuilder<MailEntity> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Subject)
            .HasMaxLength(30);
        builder.Property(m => m.Content)
            .HasMaxLength(5000);
        builder.Property(m => m.Destiny);

        builder.ToTable("MailSender");
    }
}