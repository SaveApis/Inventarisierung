using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Entities;
using Backend.Domains.Logging.Persistence.Sql;
using FluentResults;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Commands.StartLogEntry;

public class StartLogEntryCommandHandler(ILoggingDbContextFactory factory) : ICommandHandler<StartLogEntryCommand, Id>
{
    public async Task<Result<Id>> Handle(StartLogEntryCommand request, CancellationToken cancellationToken)
    {
        var entry = LogEntryEntity.Create(request.Type, request.AffectedEntityId, request.AffectedEntityName, request.LoggedBy);

        await using var context = factory.Create();

        await context.LogEntries.AddAsync(entry, cancellationToken).ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return entry.Id;
    }
}
