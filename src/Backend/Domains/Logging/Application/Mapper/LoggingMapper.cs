using Backend.Domains.Logging.Domain.Dto;
using Backend.Domains.Logging.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Backend.Domains.Logging.Application.Mapper;

[Mapper]
public partial class LoggingMapper
{
    public partial LogEntryGetDto ToDto(LogEntry logEntry);
    public partial IEnumerable<LogEntryGetDto> ToDto(IEnumerable<LogEntry> logEntries);

    [MapperIgnoreSource(nameof(logEntryValue.LogEntryId))]
    public partial LogEntryValueGetDto ToDto(LogEntryValue logEntryValue);
    public partial IEnumerable<LogEntryValueGetDto> ToDto(IEnumerable<LogEntryValue> logEntryValues);
}
