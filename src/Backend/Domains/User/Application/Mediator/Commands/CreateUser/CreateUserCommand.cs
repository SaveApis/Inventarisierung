using Backend.Domains.User.Domain.Dto;
using SaveApis.Core.Common.Domains.Common.Domain.VO;
using SaveApis.Core.Common.Infrastructure.Mediator;

namespace Backend.Domains.User.Application.Mediator.Commands.CreateUser;

public record CreateUserCommand(UserCreateDto Dto, bool IsInitialUser = false) : ICommand<Id>;
