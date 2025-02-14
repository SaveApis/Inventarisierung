using Backend.Domains.User.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Persistence.Sql.Configuration;

public class PermissionNameEntityConfiguration : IEntityTypeConfiguration<PermissionNameEntity>
{
    public void Configure(EntityTypeBuilder<PermissionNameEntity> builder)
    {
        builder.ToTable("PermissionName");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired().HasConversion<Id.EfCoreValueConverter>();
        builder.Property(e => e.Name).IsRequired().HasConversion<Name.EfCoreValueConverter>();
        builder.Property(e => e.Locale).IsRequired().HasConversion<string>();
        builder.Property(e => e.PermissionId).IsRequired().HasConversion<Id.EfCoreValueConverter>();

        builder.HasIndex(e => e.PermissionId);
        builder.HasIndex(e => new { e.Locale, e.PermissionId }).IsUnique();
    }
}
