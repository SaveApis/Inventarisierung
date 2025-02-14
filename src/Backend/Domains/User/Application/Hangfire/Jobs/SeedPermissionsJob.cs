using Backend.Domains.User.Application.Mediator.Commands.CreatePermission;
using Backend.Domains.User.Infrastructure;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using SaveApis.Core.Common.Application.Hangfire.Events;
using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Jobs;
using ILogger = Serilog.ILogger;

namespace Backend.Domains.User.Application.Hangfire.Jobs;

[HangfireQueue(HangfireQueue.High)]
public class SeedPermissionsJob(ILogger logger, IMediator mediator, IEnumerable<IPermission> permissions) : BaseJob<MigrationCompletedEvent>(logger)
{
    [HangfireJobName("Seed permissions")]
    public override async Task RunAsync(MigrationCompletedEvent @event, PerformContext? performContext = null, CancellationToken cancellationToken = new CancellationToken())
    {
        var list = permissions.ToList();
        performContext.WriteLine($"Seeding {list} permission...");

        var bar = performContext.WriteProgressBar("Seeding permissions", list.Count);
        foreach (var permission in list.WithProgress(bar))
        {
            performContext.WriteLine($"Seeding permission {permission.Key}...");
            var result = await mediator.Send(new CreatePermissionCommand(permission), cancellationToken).ConfigureAwait(false);
            if (!result.IsFailed)
            {
                continue;
            }

            performContext.SetTextColor(ConsoleTextColor.DarkYellow);
            performContext.WriteLine("Permission already exists");
            performContext.ResetTextColor();
        }
    }
}
