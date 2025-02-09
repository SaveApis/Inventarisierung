using Backend.Domains.Logging.Domain.Entities;
using Backend.Domains.Logging.Persistence.Sql;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Queries;

public class GetLogEntriesByEntityIdQueryHandler(ILoggingDbContextFactory factory) : IQueryHandler<GetLogEntriesByEntityId, IEnumerable<LogEntry>>
{
    public async Task<Result<IEnumerable<LogEntry>>> Handle(GetLogEntriesByEntityId request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        return await context
            .LogEntries
            .Include(e => e.Values)
            .Where(it => it.AffectedEntityId == request.Id)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
