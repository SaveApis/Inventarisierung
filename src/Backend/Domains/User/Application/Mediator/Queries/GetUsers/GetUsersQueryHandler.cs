using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetUsers;

public class GetUsersQueryHandler(IUserDbContextFactory factory) : IQueryHandler<GetUsersQuery, IEnumerable<UserEntity>>
{
    public async Task<Result<IEnumerable<UserEntity>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        return await context.Users.ToListAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
