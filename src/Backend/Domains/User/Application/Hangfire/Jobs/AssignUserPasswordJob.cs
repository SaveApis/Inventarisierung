using Backend.Domains.User.Application.Hangfire.Events;
using Backend.Domains.User.Application.Mediator.Commands.AssignUserPassword;
using Backend.Domains.User.Application.Mediator.Queries.GetUserById;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Jobs;
using ILogger = Serilog.ILogger;

namespace Backend.Domains.User.Application.Hangfire.Jobs;

[HangfireQueue(HangfireQueue.High)]
public class AssignUserPasswordJob(ILogger logger, IMediator mediator) : BaseJob<UserCreatedEvent>(logger)
{
    public override async Task<bool> CheckSupportAsync(UserCreatedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await mediator.Send(new GetUserByIdQuery(@event.Id), cancellationToken).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return false;
        }

        return result.Value.Hash is null;
    }

    [HangfireJobName("{0}: Assign user password")]
    public override async Task RunAsync(UserCreatedEvent @event, PerformContext? performContext = null, CancellationToken cancellationToken = new CancellationToken())
    {
        performContext.WriteLine("Assigning user password...");
        var result = await mediator.Send(new AssignUserPasswordCommand(@event.Id), cancellationToken).ConfigureAwait(false);
        if (result.IsFailed)
        {
            performContext.SetTextColor(ConsoleTextColor.DarkRed);
            performContext.WriteLine("Failed to assign user password.");
            performContext.WriteLine(string.Join(", ", result.Errors.Select(x => x.Message)));
            performContext.ResetTextColor();

            return;
        }

        performContext.WriteLine("Password assigned successfully.");
    }
}
