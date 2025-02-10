using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Types;

namespace Backend.Domains.Logging.Domain.Dto;

public class LogEntryGetDto
{
    public Id Id { get; set; } = Id.Empty;
    public DateTime LoggedAt { get; set; } = DateTime.MinValue;
    public LogEntryType LogType { get; set; } = LogEntryType.Create;

    public Id AffectedEntityId { get; set; } = Id.Empty;
    public Name AffectedEntityName { get; set; } = Name.Empty;
    public LogState State { get; set; } = LogState.Started;

    //TODO: Figure out how to use `UserGetDto` if user domain is implemented
    public Id? LoggedBy { get; set; }

    public List<LogEntryValueGetDto> Values { get; set; } = [];
}
