using Backend.Domains.User.Domain.VO;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using MediatR;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Logging.Domain.Types;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.AssignUserPassword;

public class AssignUserPasswordCommandHandler(IMediator mediator, IUserDbContextFactory factory) : BaseCommandHandler<AssignUserPasswordCommand, Id>(mediator)
{
    public override async Task<Result<Id>> Handle(AssignUserPasswordCommand request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var user = await context.Users.FindAsync([request.Id], cancellationToken).ConfigureAwait(false);
        if (user is null)
        {
            return Result.Fail($"User with id '{request.Id}' not found.");
        }

        user.WithHash(Password.Generate().ToHash());

        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        await LogAsync(user, LogType.Update, "User", request.TriggeredBy).ConfigureAwait(false);

        return user.Id;
    }
}
