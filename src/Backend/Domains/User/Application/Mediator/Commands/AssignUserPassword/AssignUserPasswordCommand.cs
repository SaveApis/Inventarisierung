using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.AssignUserPassword;

public record AssignUserPasswordCommand(Id Id, Id? TriggeredBy = null) : ICommand<Id>;
