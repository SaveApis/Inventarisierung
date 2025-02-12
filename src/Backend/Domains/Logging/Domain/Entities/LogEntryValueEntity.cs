using Backend.Domains.Common.Domain.VO;

namespace Backend.Domains.Logging.Domain.Entities;

public class LogEntryValueEntity
{
    private LogEntryValueEntity(Id id, Name attributeName, string? oldValue, string? newValue, Id logEntryId)
    {
        Id = id;
        AttributeName = attributeName;
        OldValue = oldValue;
        NewValue = newValue;
        LogEntryId = logEntryId;
    }

    public Id Id { get; }
    public Name AttributeName { get; }
    public string? OldValue { get; }
    public string? NewValue { get; }

    public Id LogEntryId { get; }

    public static LogEntryValueEntity Create(Name attributeName, string? oldValue, string? newValue, Id logEntryId)
    {
        return new LogEntryValueEntity(Id.From(Guid.NewGuid()), attributeName, oldValue, newValue, logEntryId);
    }
}
