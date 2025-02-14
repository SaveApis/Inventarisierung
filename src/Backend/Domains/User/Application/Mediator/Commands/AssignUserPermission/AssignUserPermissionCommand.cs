using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.AssignUserPermission;

public record AssignUserPermissionCommand(Id UserId, Id PermissionId) : ICommand<Id>;
