using Backend.Domains.Common.Domain.VO;
using Backend.Domains.User.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Persistence.Sql.Configuration;

public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable("Permission");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired().HasConversion<Id.EfCoreValueConverter>();
        builder.Property(e => e.Key).IsRequired().HasConversion<Key.EfCoreValueConverter>();

        builder.HasMany(e => e.LocalizedNames).WithOne().HasForeignKey(e => e.PermissionId);
        builder.HasMany(e => e.LocalizedDescriptions).WithOne().HasForeignKey(e => e.PermissionId);
        builder.HasMany(e => e.Users).WithMany(e => e.Permissions);
    }
}
