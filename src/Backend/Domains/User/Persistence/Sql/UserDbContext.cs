using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql.Configuration;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Persistence.Sql;

namespace Backend.Domains.User.Persistence.Sql;

public class UserDbContext(DbContextOptions options) : BaseDbContext(options)
{
    protected override string Schema => "User";

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PermissionEntity> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionNameEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionDescriptionEntityConfiguration());
    }
}
