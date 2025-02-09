using Autofac;
using Backend.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SaveApis.Core.Common.Application.Types;
using SaveApis.Core.Common.Infrastructure.Extension;
using SaveApis.Core.Common.Infrastructure.Persistence.Sql;

namespace Backend.Tests;

public abstract class BaseTest
{
    private static ContainerFixture Container { get; } = new ContainerFixture();

    [Before(Assembly)]
    public static async Task SetupAssembly()
    {
        await Container.StartAsync().ConfigureAwait(false);
    }

    [After(Assembly)]
    public static async Task TeardownAssembly()
    {
        await Container.StopAsync().ConfigureAwait(false);
    }

    protected static IContainer CreateContainer(string name)
    {
        var assembly = typeof(Program).Assembly;
        var configuration = Container.LoadConfiguration(name);

        var builder = new ContainerBuilder();

        builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
        builder.RegisterCommonModules(ServerType.Server, [assembly], new ConfigurationBuilder().AddInMemoryCollection().Build());

        return builder.Build();
    }

    protected static async Task MigrateAsync<TContext>(IContainer container) where TContext : BaseDbContext
    {
        var factory = container.Resolve<IDesignTimeDbContextFactory<TContext>>();
        await using var context = factory.CreateDbContext([]);

        await context.Database.EnsureDeletedAsync().ConfigureAwait(false);
        await context.Database.MigrateAsync().ConfigureAwait(false);
    }
}
