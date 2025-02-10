using Backend.Domains.Logging.Domain.Entities;
using Backend.Domains.Logging.Persistence.Sql.Configurations;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Persistence.Sql;

namespace Backend.Domains.Logging.Persistence.Sql;

public class LoggingDbContext(DbContextOptions options) : BaseDbContext(options)
{
    protected override string Schema => "Logging";

    public DbSet<LogEntry> LogEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new LogEntryConfiguration());
        modelBuilder.ApplyConfiguration(new LogEntryValueConfiguration());
    }
}
