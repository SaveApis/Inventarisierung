using Backend.Domains.User.Domain.Entity;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Queries.GetPermissionById;

public record GetPermissionByIdQuery(Id Id) : IQuery<PermissionEntity>;
