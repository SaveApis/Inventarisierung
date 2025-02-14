using Backend.Domains.User.Application.Hangfire.Events;
using Backend.Domains.User.Domain.Entity;
using Backend.Domains.User.Persistence.Sql;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.CreatePermission;

public class CreatePermissionCommandHandler(IUserDbContextFactory factory, IMediator mediator) : ICommandHandler<CreatePermissionCommand, Id>
{
    public async Task<Result<Id>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        await using var context = factory.Create();

        var existingPermission = await context.Permissions.SingleOrDefaultAsync(it => it.Key == request.Permission.Key, cancellationToken).ConfigureAwait(false);
        if (existingPermission is not null)
        {
            return Result.Fail("Permission already exists");
        }

        var entity = PermissionEntity.Create(request.Permission.Key);
        foreach (var name in request.Permission.LocalizedNames)
        {
            entity.AddLocalizedName(name.Item1, name.Item2);
        }

        foreach (var description in request.Permission.LocalizedDescriptions)
        {
            entity.AddLocalizedDescription(description.Item1, description.Item2);
        }

        context.Permissions.Add(entity);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        await PublishEvent(entity.Id).ConfigureAwait(false);

        return entity.Id;
    }

    private async Task PublishEvent(Id id)
    {
        var @event = new PermissionCreatedEvent
        {
            Id = id,
        };
        await mediator.Publish(@event).ConfigureAwait(false);
    }
}
