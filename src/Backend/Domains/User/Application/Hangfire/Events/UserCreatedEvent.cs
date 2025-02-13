using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Hangfire.Events;

namespace Backend.Domains.User.Application.Hangfire.Events;

public class UserCreatedEvent : IEvent
{
    public required Id Id { get; init; }
}
