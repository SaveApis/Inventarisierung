using Backend.Domains.User.Domain.Entity;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetUserById;

public record GetUserByIdQuery(Id Id) : IQuery<UserEntity>;
