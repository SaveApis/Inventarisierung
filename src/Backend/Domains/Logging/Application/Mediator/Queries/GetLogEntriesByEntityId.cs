using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Entities;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.Logging.Application.Mediator.Queries;

public record GetLogEntriesByEntityId(Id Id) : IQuery<IEnumerable<LogEntryEntity>>;
