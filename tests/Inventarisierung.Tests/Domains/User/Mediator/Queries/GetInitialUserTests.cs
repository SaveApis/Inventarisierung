using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Application.Mediator.Queries.GetInitialUser;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.Types;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Inventarisierung.Tests.Domains.User.Mediator.Queries;

public class GetInitialUserTests : BaseTest
{
    [Test]
    public async Task Can_Read_InitialUser()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };
        await Mediator.Send(new CreateUserCommand(dto, true)).ConfigureAwait(false);

        // Act
        var result = await Mediator.Send(new GetInitialUserQuery()).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsNotNull();
        await Assert.That(result.Value.FirstName).IsEqualTo(dto.FirstName);
        await Assert.That(result.Value.LastName).IsEqualTo(dto.LastName);
        await Assert.That(result.Value.Email).IsEqualTo(dto.Email);
        await Assert.That(result.Value.UserName).IsEqualTo(dto.UserName);
        await Assert.That(result.Value.Hash).IsNull();
        await Assert.That(result.Value.State).IsEqualTo(UserState.Active);
        await Assert.That(result.Value.IsInitialUser).IsTrue();
    }

    [Test]
    public async Task Cannot_Read_InitialUser_When_Not_Exists()
    {
        // Act
        var result = await Mediator.Send(new GetInitialUserQuery()).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsFailed).IsTrue();
    }
}
