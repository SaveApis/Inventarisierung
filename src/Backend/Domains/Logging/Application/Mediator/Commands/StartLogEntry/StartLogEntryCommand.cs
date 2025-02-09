using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Types;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Commands.StartLogEntry;

public record StartLogEntryCommand(LogEntryType Type, Id AffectedEntityId, Name AffectedEntityName, Id? LoggedBy = null) : ICommand<Id>;
