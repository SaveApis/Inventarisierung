using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Domains.Logging.Persistence.Sql.Configurations;

public class LogEntryValueConfiguration : IEntityTypeConfiguration<LogEntryValue>
{
    public void Configure(EntityTypeBuilder<LogEntryValue> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired().HasConversion<Id.EfCoreValueConverter>();
        builder.Property(e => e.AttributeName).IsRequired().HasConversion<Name.EfCoreValueConverter>();
        builder.Property(e => e.OldValue).IsRequired(false).HasMaxLength(500);
        builder.Property(e => e.NewValue).IsRequired(false).HasMaxLength(500);

        builder.Property(e => e.LogEntryId).IsRequired().HasConversion<Id.EfCoreValueConverter>();

        builder.HasIndex(e => new { e.LogEntryId, e.AttributeName }).IsUnique();
    }
}
