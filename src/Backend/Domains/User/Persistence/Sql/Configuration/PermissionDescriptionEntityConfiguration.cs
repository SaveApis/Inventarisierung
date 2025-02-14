using Backend.Domains.Common.Domain.VO;
using Backend.Domains.User.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Persistence.Sql.Configuration;

public class PermissionDescriptionEntityConfiguration : IEntityTypeConfiguration<PermissionDescriptionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionDescriptionEntity> builder)
    {
        builder.ToTable("PermissionDescription");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired().HasConversion<Id.EfCoreValueConverter>();
        builder.Property(e => e.Description).IsRequired().HasConversion<Description.EfCoreValueConverter>();
        builder.Property(e => e.Locale).IsRequired().HasConversion<string>();
        builder.Property(e => e.PermissionId).IsRequired().HasConversion<Id.EfCoreValueConverter>();

        builder.HasIndex(e => e.PermissionId);
        builder.HasIndex(e => new { e.Locale, e.PermissionId }).IsUnique();
    }
}
