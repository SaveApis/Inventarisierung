using SaveApis.Core.Common.Application.Hangfire.Types;
using SaveApis.Core.Common.Infrastructure.Hangfire.Attributes;
using SaveApis.Core.Common.Infrastructure.Hangfire.Events;

namespace Backend.Domains.User.Application.Hangfire.Events.Recurring;

[HangfireRecurringEvent("user:check:missing-password", "*/15 * * * *", HangfireQueue.High)]
public class CheckMissingUserPasswordRecurringEvent : IEvent;
