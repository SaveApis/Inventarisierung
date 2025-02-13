using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Backend.Domains.User.Domain.Dto;

public class UserCreateDto
{
    public required Name FirstName { get; init; }
    public required Name LastName { get; init; }
    public required Email Email { get; init; }
    public required Name UserName { get; init; }
}
