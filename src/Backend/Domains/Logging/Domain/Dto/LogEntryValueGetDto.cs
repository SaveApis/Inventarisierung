using Backend.Domains.Common.Domain.VO;

namespace Backend.Domains.Logging.Domain.Dto;

public class LogEntryValueGetDto
{
    public Id Id { get; set; } = Id.Empty;
    public Name AttributeName { get; set; } = Name.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
