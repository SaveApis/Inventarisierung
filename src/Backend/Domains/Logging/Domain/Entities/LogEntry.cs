using Backend.Domains.Common.Domain.VO;
using Backend.Domains.Logging.Domain.Types;

namespace Backend.Domains.Logging.Domain.Entities;

public class LogEntry
{
    private LogEntry(Id id, DateTime loggedAt, LogEntryType logType, Id affectedEntityId, Name affectedEntityName, LogState state, Id? loggedBy)
    {
        Id = id;
        LoggedAt = loggedAt;
        LogType = logType;
        AffectedEntityId = affectedEntityId;
        AffectedEntityName = affectedEntityName;
        State = state;
        LoggedBy = loggedBy;
    }

    public Id Id { get; }
    public DateTime LoggedAt { get; }
    public LogEntryType LogType { get; }

    public Id AffectedEntityId { get; }
    public Name AffectedEntityName { get; }
    public LogState State { get; private set; }

    public Id? LoggedBy { get; }

    public virtual List<LogEntryValue> Values { get; set; } = [];

    public LogEntry WithState(LogState state)
    {
        State = state;

        return this;
    }

    public LogEntry WithValue(Name attributeName, string? oldValue, string? newValue)
    {
        Values.Add(LogEntryValue.Create(attributeName, oldValue, newValue, Id));

        return this;
    }

    public static LogEntry Create(LogEntryType logType, Id affectedEntityId, Name affectedEntityName, Id? loggedBy = null)
    {
        return new LogEntry(Id.From(Guid.NewGuid()), DateTime.UtcNow, logType, affectedEntityId, affectedEntityName, LogState.Started, loggedBy);
    }
}
