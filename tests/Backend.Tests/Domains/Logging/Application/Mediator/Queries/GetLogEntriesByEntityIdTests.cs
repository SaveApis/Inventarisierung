using Autofac;
using Backend.Domains.Logging.Application.Mediator.Queries;
using Backend.Domains.Logging.Persistence.Sql;
using MediatR;

namespace Backend.Tests.Domains.Logging.Application.Mediator.Queries;

public class GetLogEntriesByEntityIdTests : BaseLoggingTest
{
    [Test]
    public async Task Can_Get_Log_Entry_By_Entity_Id()
    {
        var container = CreateContainer(nameof(Can_Get_Log_Entry_By_Entity_Id));
        var mediator = container.Resolve<IMediator>();
        await MigrateAsync<LoggingDbContext>(container).ConfigureAwait(false);

        // Arrange
        var entries = await SeedAsync(container, 10).ConfigureAwait(false);
        var selectedEntry = entries.First();

        // Act
        var result = await mediator.Send(new GetLogEntriesByEntityId(selectedEntry.AffectedEntityId)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsEqualTo(true);
        await Assert.That(result.Value).IsNotNull();
        await Assert.That(result.Value.Count()).IsEqualTo(1);
    }
}
