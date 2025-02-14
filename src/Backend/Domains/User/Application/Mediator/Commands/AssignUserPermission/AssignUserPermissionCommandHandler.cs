using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.AssignUserPermission;

public class AssignUserPermissionCommandHandler(IUserDbContextFactory factory) : ICommandHandler<AssignUserPermissionCommand, Id>
{
    public async Task<Result<Id>> Handle(AssignUserPermissionCommand request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var user = await context.Users
            .Include(e => e.Permissions)
            .SingleAsync(e => e.Id == request.UserId, cancellationToken)
            .ConfigureAwait(false);
        var permission = await context.Permissions
            .Include(e => e.LocalizedNames)
            .Include(e => e.LocalizedDescriptions)
            .SingleAsync(e => e.Id == request.PermissionId, cancellationToken)
            .ConfigureAwait(false);

        user.Permissions.Add(permission);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return user.Id;
    }
}
