using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetInitialUser;

public class GetInitialUserQueryHandler(IUserDbContextFactory factory) : IQueryHandler<GetInitialUserQuery, UserEntity>
{
    public async Task<Result<UserEntity>> Handle(GetInitialUserQuery request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var user = await context.Users.SingleOrDefaultAsync(it => it.IsInitialUser, cancellationToken: cancellationToken).ConfigureAwait(false);

        return user ?? (Result<UserEntity>)Result.Fail("Initial user not found");
    }
}
