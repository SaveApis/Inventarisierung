using Backend.Domains.User.Application.Hangfire.Events.Recurring;
using Backend.Domains.User.Application.Mediator.Commands.AssignUserPassword;
using Backend.Domains.User.Application.Mediator.Queries.GetUsers;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Jobs;
using ILogger = Serilog.ILogger;

namespace Backend.Domains.User.Application.Hangfire.Jobs;

[HangfireQueue(HangfireQueue.High)]
public class CheckMissingUserPasswordJob(ILogger logger, IMediator mediator) : BaseJob<CheckMissingUserPasswordRecurringEvent>(logger)
{
    [HangfireJobName("Check missing user passwords")]
    public override async Task RunAsync(CheckMissingUserPasswordRecurringEvent @event, PerformContext? performContext = null, CancellationToken cancellationToken = new CancellationToken())
    {
        performContext.WriteLine("Read users...");
        var result = await mediator.Send(new GetUsersQuery(), cancellationToken).ConfigureAwait(false);
        if (result.IsFailed)
        {
            performContext?.WriteLine("Failed to get users.");
            performContext?.WriteLine(string.Join(", ", result.Errors.Select(x => x.Message)));

            return;
        }

        performContext.WriteLine("Check for missing passwords...");
        var users = result.Value.Where(x => x.Hash is null).ToList();
        if (users.Count == 0)
        {
            performContext.WriteLine("No missing passwords found.");

            return;
        }

        var bar = performContext.WriteProgressBar("Assigning passwords...");
        performContext.WriteLine($"Found {users.Count} missing passwords.");
        foreach (var user in users.WithProgress(bar))
        {
            performContext.WriteLine($"Assigning password to user {user.Id}...");
            var assignResult = await mediator.Send(new AssignUserPasswordCommand(user.Id), cancellationToken).ConfigureAwait(false);
            if (assignResult.IsFailed)
            {
                performContext.SetTextColor(ConsoleTextColor.DarkRed);
                performContext.WriteLine($"Failed to assign password to user {user.Id}.");
                performContext.WriteLine(string.Join(", ", assignResult.Errors.Select(x => x.Message)));
                performContext.ResetTextColor();

                continue;
            }

            performContext.WriteLine($"Password assigned to user {user.Id} successfully.");
        }
    }
}
