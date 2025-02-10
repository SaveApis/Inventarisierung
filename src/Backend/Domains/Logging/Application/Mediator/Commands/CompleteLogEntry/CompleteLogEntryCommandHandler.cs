using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Types;
using Backend.Domains.Logging.Persistence.Sql;
using FluentResults;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Commands.CompleteLogEntry;

public class CompleteLogEntryCommandHandler(ILoggingDbContextFactory factory) : ICommandHandler<CompleteLogEntryCommand, Id>
{
    public async Task<Result<Id>> Handle(CompleteLogEntryCommand request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var existingEntry = await context.LogEntries.FindAsync([request.Id], cancellationToken).ConfigureAwait(false);
        if (existingEntry is null)
        {
            return Result.Fail($"Log entry with id '{request.Id}' not found!");
        }

        if (existingEntry.State == LogState.Completed)
        {
            return Result.Fail($"Log entry with id '{request.Id}' is already completed!");
        }

        existingEntry.WithState(LogState.Completed);

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return existingEntry.Id;
    }
}
