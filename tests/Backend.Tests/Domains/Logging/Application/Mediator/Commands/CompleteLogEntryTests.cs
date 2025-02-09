using Autofac;
using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Application.Mediator.Commands.CompleteLogEntry;
using Backend.Domains.Logging.Domain.Types;
using Backend.Domains.Logging.Persistence.Sql;
using MediatR;

namespace Backend.Tests.Domains.Logging.Application.Mediator.Commands;

public class CompleteLogEntryTests : BaseLoggingTest
{
    [Test]
    public async Task Can_Complete_Started_LogEntry()
    {
        var container = CreateContainer(nameof(Can_Complete_Started_LogEntry));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container).ConfigureAwait(false);

        // Act
        var result = await mediator.Send(new CompleteLogEntryCommand(entries.First().Id)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
    }
    [Test]
    public async Task Can_Complete_In_Progress_LogEntry()
    {
        var container = CreateContainer(nameof(Can_Complete_In_Progress_LogEntry));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container, 1, entry => entry.WithState(LogState.InProgress)).ConfigureAwait(false);

        // Act
        var result = await mediator.Send(new CompleteLogEntryCommand(entries.First().Id)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
    }

    [Test]
    public async Task Cannot_Complete_LogEntry_Not_Found()
    {
        var container = CreateContainer(nameof(Cannot_Complete_LogEntry_Not_Found));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Act
        var uuid = Guid.NewGuid();
        var result = await mediator.Send(new CompleteLogEntryCommand(Id.From(uuid))).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsFailed).IsEqualTo(true);
        await Assert.That(result.Errors[0].Message).IsEqualTo($"Log entry with id '{uuid}' not found!");
    }

    [Test]
    public async Task Cannot_Complete_LogEntry_Already_Completed()
    {
        var container = CreateContainer(nameof(Cannot_Complete_LogEntry_Already_Completed));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container, 1, entry => entry.WithState(LogState.Completed)).ConfigureAwait(false);
        var selectedEntry = entries.First();

        // Act
        var result = await mediator.Send(new CompleteLogEntryCommand(selectedEntry.Id)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsFailed).IsTrue();
        await Assert.That(result.Errors[0].Message).IsEqualTo($"Log entry with id '{selectedEntry.Id}' is already completed!");
    }
}
