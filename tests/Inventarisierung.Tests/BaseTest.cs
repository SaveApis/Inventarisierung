using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SaveApis.Core.Common.Application.DI;
using SaveApis.Core.Common.Application.Helpers;
using SaveApis.Core.Common.Application.Types;
using SaveApis.Core.Common.Infrastructure.Extension;
using SaveApis.Core.Common.Infrastructure.Helpers;
using SaveApis.Core.Common.Infrastructure.Persistence.Sql;
using SaveApis.Core.Web.Application.DI;
using SaveApis.Core.Web.Infrastructure.DI;
using SaveApis.Core.Web.Infrastructure.Extensions;

namespace Inventarisierung.Tests;

public abstract class BaseTest
{
    private IContainer? Container { get; set; }
    protected IMediator Mediator => Resolve<IMediator>();

    [Before(Test)]
    public async Task SetupTest()
    {
        var assemblyHelper = new AssemblyHelper();
        assemblyHelper.RegisterAssembly(typeof(Program).Assembly);
        assemblyHelper.RegisterAssembly(typeof(BaseWebModule).Assembly);

        var configuration = ContainerFixture.LoadConfiguration();

        var builder = new ContainerBuilder();
        builder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();
        builder.RegisterInstance(assemblyHelper).As<IAssemblyHelper>().SingleInstance();
        builder.WithCommonModule<SerilogModule>();
        builder.WithCommonModule<MediatorModule>(assemblyHelper.GetAssemblies());
        builder.WithCommonModule<EfCoreModule>(assemblyHelper.GetAssemblies());
        builder.WithCommonModule<HangfireModule>(configuration, ServerType.Backend, assemblyHelper.GetAssemblies());
        builder.WithWebModule<CorrelationModule>();

        Container = builder.Build();

        foreach (var factory in Container.Resolve<IEnumerable<IDesignTimeDbContextFactory<BaseDbContext>>>())
        {
            await using var context = factory.CreateDbContext([]);
            await context.Database.MigrateAsync().ConfigureAwait(false);
        }
    }

    [Before(Assembly)]
    public static void SetupAssembly()
    {
        ContainerFixture.Start();
    }

    [After(Assembly)]
    public static void TeardownAssembly()
    {
        ContainerFixture.Stop();
    }

    protected TValue Resolve<TValue>() where TValue : notnull
    {
        if (Container is null)
        {
            throw new InvalidOperationException("Container is not initialized");
        }

        return Container.Resolve<TValue>();
    }

    protected async Task MigrateAsync<TContext>() where TContext : BaseDbContext
    {
        var factory = Resolve<IDesignTimeDbContextFactory<TContext>>();
        await using var context = factory.CreateDbContext([]);

        context.Database.EnsureDeletedAsync().Wait();
        context.Database.MigrateAsync().Wait();
    }
}
