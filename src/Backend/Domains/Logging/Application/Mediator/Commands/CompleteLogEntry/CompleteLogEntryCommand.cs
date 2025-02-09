using Backend.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Commands.CompleteLogEntry;

public record CompleteLogEntryCommand(Id Id) : ICommand<Id>;
