using Autofac;
using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Application.Mediator.Commands.ProgressLogEntry;
using Backend.Domains.Logging.Domain.Types;
using Backend.Domains.Logging.Persistence.Sql;
using MediatR;

namespace Backend.Tests.Domains.Logging.Application.Mediator.Commands;

public class ProgressLogEntryTests : BaseLoggingTest
{
    [Test]
    public async Task Can_Process_Started_Entry()
    {
        var container = CreateContainer(nameof(Can_Process_Started_Entry));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container).ConfigureAwait(false);
        var command = new ProgressLogEntryCommand(entries.First().Id, Name.From("TestAttribute"), null, "Test1");

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
    }

    [Test]
    public async Task Can_Process_In_Progress_Entry()
    {
        var container = CreateContainer(nameof(Can_Process_In_Progress_Entry));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container, 1, entry => entry.WithState(LogState.InProgress)).ConfigureAwait(false);
        var command = new ProgressLogEntryCommand(entries.First().Id, Name.From("TestAttribute"), null, "Test1");

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
    }

    [Test]
    public async Task Cannot_Progress_Entry_Not_Found()
    {
        var container = CreateContainer(nameof(Cannot_Progress_Entry_Not_Found));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var guid = Guid.NewGuid();
        var command = new ProgressLogEntryCommand(Id.From(guid), Name.From("TestAttribute"), null, "Test1");

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(false);
        await Assert.That(result.Errors[0].Message).IsEqualTo($"Log entry with id '{guid}' not found!");
    }

    [Test]
    public async Task Cannot_Progress_Entry_Already_Completed()
    {
        var container = CreateContainer(nameof(Cannot_Progress_Entry_Already_Completed));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container, 1, entry => entry.WithState(LogState.Completed)).ConfigureAwait(false);
        var selectedEntry = entries.First();
        var command = new ProgressLogEntryCommand(selectedEntry.Id, Name.From("TestAttribute"), null, "Test1");

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(false);
        await Assert.That(result.Errors[0].Message).IsEqualTo($"Log entry with id '{selectedEntry.Id}' is already completed!");
    }

    [Test]
    public async Task Cannot_Progress_Entry_Property_Already_Set()
    {
        var container = CreateContainer(nameof(Cannot_Progress_Entry_Property_Already_Set));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var attributeName = Name.From("TestAttribute");
        var entries = await SeedAsync(container, 1, entry => entry.WithState(LogState.InProgress).WithValue(attributeName, null, "Test1")).ConfigureAwait(false);
        var selectedEntry = entries.First();
        var command = new ProgressLogEntryCommand(selectedEntry.Id, attributeName, null, "Test1");

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(false);
        await Assert.That(result.Errors[0].Message).IsEqualTo($"Log entry with id '{selectedEntry.Id}' already contains value for attribute '{attributeName}'!");
    }
}
