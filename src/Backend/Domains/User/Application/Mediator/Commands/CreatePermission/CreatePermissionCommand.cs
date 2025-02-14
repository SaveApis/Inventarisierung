using Backend.Domains.User.Infrastructure;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.CreatePermission;

public record CreatePermissionCommand(IPermission Permission) : ICommand<Id>;
