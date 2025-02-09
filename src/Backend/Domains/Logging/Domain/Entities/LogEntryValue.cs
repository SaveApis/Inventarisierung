using Backend.Domains.Common.Domain.VO;

namespace Backend.Domains.Logging.Domain.Entities;

public class LogEntryValue
{
    private LogEntryValue(Id id, Name attributeName, string? oldValue, string? newValue, Id logEntryId)
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

    public static LogEntryValue Create(Name attributeName, string? oldValue, string? newValue, Id logEntryId)
    {
        return new LogEntryValue(Id.From(Guid.NewGuid()), attributeName, oldValue, newValue, logEntryId);
    }
}
