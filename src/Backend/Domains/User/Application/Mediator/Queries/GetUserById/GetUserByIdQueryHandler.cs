using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserDbContextFactory factory) : IQueryHandler<GetUserByIdQuery, UserEntity>
{
    public async Task<Result<UserEntity>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var existingUser = await context.Users.FindAsync([request.Id], cancellationToken).ConfigureAwait(false);

        return existingUser is null
            ? Result.Fail<UserEntity>($"User with id '{request.Id}' not found")
            : Result.Ok(existingUser);
    }
}
