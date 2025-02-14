using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Domain.VO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Persistence.Sql.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).IsRequired().HasConversion<Id.EfCoreValueConverter>();
        builder.Property(e => e.FirstName).IsRequired().HasConversion<Name.EfCoreValueConverter>();
        builder.Property(e => e.LastName).IsRequired().HasConversion<Name.EfCoreValueConverter>();
        builder.Property(e => e.Email).IsRequired().HasConversion<Email.EfCoreValueConverter>();

        builder.Property(e => e.UserName).IsRequired().HasConversion<Name.EfCoreValueConverter>();
        builder.Property(e => e.Hash).IsRequired(false).HasConversion<Hash.EfCoreValueConverter>();
        builder.Property(e => e.State).IsRequired().HasConversion<string>();
        builder.Property(e => e.IsInitialUser).IsRequired();

        const string filter = $"{nameof(UserEntity.State)} <> 'Deleted'";
        builder.HasIndex(e => new { e.FirstName, e.LastName }).HasFilter(filter).IsUnique();
        builder.HasIndex(e => e.Email).HasFilter(filter).IsUnique();
        builder.HasIndex(e => e.UserName).HasFilter(filter).IsUnique();

        builder.HasMany(e => e.Permissions).WithMany(e => e.Users);
    }
}
