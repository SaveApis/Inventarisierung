using Backend.Domains.Logging.Domain.Dto;
using Backend.Domains.Logging.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Backend.Domains.Logging.Application.Mapper;

[Mapper]
public partial class LoggingMapper
{
    public partial LogEntryGetDto ToDto(LogEntryEntity logEntry);
    public partial IEnumerable<LogEntryGetDto> ToDto(IEnumerable<LogEntryEntity> logEntries);

    [MapperIgnoreSource(nameof(logEntryValue.LogEntryId))]
    public partial LogEntryValueGetDto ToDto(LogEntryValueEntity logEntryValue);
    public partial IEnumerable<LogEntryValueGetDto> ToDto(IEnumerable<LogEntryValueEntity> logEntryValues);
}
