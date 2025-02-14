using Backend.Domains.User.Application.Hangfire.Events;
using Backend.Domains.User.Application.Mediator.Commands.AssignUserPermission;
using Backend.Domains.User.Application.Mediator.Queries.GetInitialUser;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Jobs;
using ILogger = Serilog.ILogger;

namespace Backend.Domains.User.Application.Hangfire.Jobs;

[HangfireQueue(HangfireQueue.High)]
public class AssignPermissionToInitialUserJob(ILogger logger, IMediator mediator) : BaseJob<PermissionCreatedEvent>(logger)
{
    [HangfireJobName("{0}: Assign permission to initial user")]
    public override async Task RunAsync(PermissionCreatedEvent @event, PerformContext? performContext = null, CancellationToken cancellationToken = new CancellationToken())
    {
        performContext.WriteLine("Try to load initial user...");
        var userResult = await mediator.Send(new GetInitialUserQuery(), cancellationToken).ConfigureAwait(false);
        if (userResult.IsFailed)
        {
            performContext.SetTextColor(ConsoleTextColor.DarkRed);
            performContext.WriteLine("Initial user not found!");
            performContext.WriteLine(string.Join(", ", userResult.Errors.Select(it => it.Message)));
            performContext.ResetTextColor();

            throw new Exception("Initial user not found!");
        }

        performContext.WriteLine("Assign permission to initial user...");
        var assignResult = await mediator.Send(new AssignUserPermissionCommand(userResult.Value.Id, @event.Id), cancellationToken).ConfigureAwait(false);
        if (assignResult.IsFailed)
        {
            performContext.SetTextColor(ConsoleTextColor.DarkRed);
            performContext.WriteLine("Assign permission to initial user failed!");
            performContext.WriteLine(string.Join(", ", assignResult.Errors.Select(it => it.Message)));
            performContext.ResetTextColor();
        }
    }
}
