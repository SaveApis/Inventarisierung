using Backend.Domains.User.Domain.Entity;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetUsers;

public record GetUsersQuery : IQuery<IEnumerable<UserEntity>>;
