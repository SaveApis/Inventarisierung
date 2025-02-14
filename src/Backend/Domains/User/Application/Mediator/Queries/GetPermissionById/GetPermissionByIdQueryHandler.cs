using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetPermissionById;

public class GetPermissionByIdQueryHandler(IUserDbContextFactory factory) : IQueryHandler<GetPermissionByIdQuery, PermissionEntity>
{
    public async Task<Result<PermissionEntity>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var permission = await context.Permissions
            .Include(e => e.LocalizedNames)
            .Include(e => e.LocalizedDescriptions)
            .SingleOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);
        if (permission is null)
        {
            return Result.Fail($"Permission with id '{request.Id}' not found!");
        }

        return permission;
    }
}
