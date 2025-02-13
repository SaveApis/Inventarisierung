using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Application.Mediator.Queries.GetInitialUser;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.VO;
using Hangfire.Console;
using Hangfire.Server;
using MediatR;
using SaveApis.Core.Common.Application.Hangfire.Events;
using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Jobs;
using ILogger = Serilog.ILogger;

namespace Backend.Domains.User.Application.Hangfire.Jobs;

[HangfireQueue(HangfireQueue.High)]
public class SeedInitialUserJob(ILogger logger, IMediator mediator, IConfiguration configuration) : BaseJob<MigrationCompletedEvent>(logger)
{
    public override async Task<bool> CheckSupportAsync(MigrationCompletedEvent @event, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await mediator.Send(new GetInitialUserQuery(), cancellationToken).ConfigureAwait(false);

        return result.IsFailed;
    }

    [HangfireJobName("Seed initial user")]
    public override async Task RunAsync(MigrationCompletedEvent @event, PerformContext? performContext = null, CancellationToken cancellationToken = new CancellationToken())
    {
        performContext.WriteLine("Read initial user values");
        var firstName = configuration["initial_user_firstname"] ?? string.Empty;
        var lastName = configuration["initial_user_lastname"] ?? string.Empty;
        var email = configuration["initial_user_email"] ?? string.Empty;
        var userName = configuration["initial_user_username"] ?? string.Empty;

        var dto = new UserCreateDto
        {
            FirstName = Name.From(firstName),
            LastName = Name.From(lastName),
            Email = Email.From(email),
            UserName = Name.From(userName),
        };

        performContext.WriteLine("Create initial user");
        var result = await mediator.Send(new CreateUserCommand(dto, true), cancellationToken).ConfigureAwait(false);
        if (result.IsFailed)
        {
            performContext.SetTextColor(ConsoleTextColor.DarkRed);
            performContext.WriteLine("Failed to create initial user");
            foreach (var error in result.Errors)
            {
                performContext.WriteLine($" - {error.Message}");
            }

            performContext.ResetTextColor();
        }
    }
}
