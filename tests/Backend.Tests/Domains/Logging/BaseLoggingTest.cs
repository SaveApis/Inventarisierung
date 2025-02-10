using Autofac;
using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Entities;
using Backend.Domains.Logging.Domain.Types;
using Backend.Domains.Logging.Persistence.Sql;

namespace Backend.Tests.Domains.Logging;

public abstract class BaseLoggingTest : BaseTest
{
    protected static async Task<ICollection<LogEntry>> SeedAsync(IContainer container, int amount = 1, Action<LogEntry>? action = null)
    {
        var entries = new List<LogEntry>();
        for (int i = 0; i < amount; i++)
        {
            var entry = LogEntry.Create(LogEntryType.Create, Id.From(Guid.NewGuid()), Name.From("Test"));
            action?.Invoke(entry);
            entries.Add(entry);
        }

        var factory = container.Resolve<ILoggingDbContextFactory>();
        await using var context = factory.Create();

        await context.LogEntries.AddRangeAsync(entries).ConfigureAwait(false);
        await context.SaveChangesAsync().ConfigureAwait(false);

        return entries;
    }
}
