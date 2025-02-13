using System.Linq.Expressions;
using Backend.Domains.User.Application.Hangfire.Events;
using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Domain.Types;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Domains.Logging.Domain.Types;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.CreateUser;

public class CreateUserCommandHandler(IMediator mediator, IUserDbContextFactory factory) : BaseCommandHandler<CreateUserCommand, Id>(mediator)
{
    public override async Task<Result<Id>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = UserEntity.Create(dto.FirstName, dto.LastName, dto.Email, dto.UserName, null, UserState.Active, request.IsInitialUser);

        await using var context = factory.Create();

        var result = await CheckExistsAsync(context, e => e.FirstName == dto.FirstName && e.LastName == dto.LastName, $"User with Name '{dto.FirstName} {dto.LastName}' already exists", cancellationToken).ConfigureAwait(false);
        result ??= await CheckExistsAsync(context, e => e.Email == dto.Email, $"User with Email '{dto.Email}' already exists", cancellationToken).ConfigureAwait(false);
        result ??= await CheckExistsAsync(context, e => e.UserName == dto.UserName, $"User with UserName '{dto.UserName}' already exists", cancellationToken).ConfigureAwait(false);

        if (result is not null)
        {
            return result;
        }

        context.Users.Add(entity);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        await PublishEvent(entity.Id).ConfigureAwait(false);
        await LogAsync(entity, LogType.Create, "User").ConfigureAwait(false);

        return entity.Id;
    }

    private static async Task<Result?> CheckExistsAsync(UserDbContext context, Expression<Func<UserEntity, bool>> func, string errorMessage, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.SingleOrDefaultAsync(func, cancellationToken).ConfigureAwait(false);

        return existingUser is not null
            ? Result.Fail(errorMessage)
            : null;
    }

    private async Task PublishEvent(Id id)
    {
        var @event = new UserCreatedEvent
        {
            Id = id,
        };

        await mediator.Publish(@event).ConfigureAwait(false);
    }
}
