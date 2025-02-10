using Autofac;
using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Application.Mediator.Commands.StartLogEntry;
using Backend.Domains.Logging.Domain.Types;
using Backend.Domains.Logging.Persistence.Sql;
using MediatR;

namespace Backend.Tests.Domains.Logging.Application.Mediator.Commands;

public class StartLogEntryTests : BaseTest
{
    [Test]
    public async Task Can_Start_LogEntry()
    {
        var container = CreateContainer(nameof(Can_Start_LogEntry));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var command = new StartLogEntryCommand(LogEntryType.Create, Id.From(Guid.NewGuid()), Name.From("Test"));

        // Act
        var result = await mediator.Send(command).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
    }
}
