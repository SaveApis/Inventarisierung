using Backend.Domains.User.Application.Mediator.Commands.AssignUserPassword;
using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Application.Mediator.Queries.GetUserById;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Inventarisierung.Tests.Domains.User.Mediator.Commands;

public class AssignUserPasswordCommandTests : BaseTest
{
    [Test]
    public async Task Can_Assign_User_Password()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };

        var createResult = await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);

        // Act
        var result = await Mediator.Send(new AssignUserPasswordCommand(createResult.Value)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();

        var queryResult = await Mediator.Send(new GetUserByIdQuery(createResult.Value)).ConfigureAwait(false);
        await Assert.That(queryResult.Value.Hash).IsNotNull();
    }

    [Test]
    public async Task Can_Not_Assign_User_Password_When_User_Not_Found()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await Mediator.Send(new AssignUserPasswordCommand(Id.From(id))).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsFailed).IsTrue();
        await Assert.That(result.Errors).HasCount().EqualToOne();
        await Assert.That(result.Errors[0].Message).IsEqualTo($"User with id '{id}' not found.");
    }
}
