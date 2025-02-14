using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Application.Mediator.Queries.GetUserById;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Inventarisierung.Tests.Domains.User.Mediator.Queries;

public class GetUserByIdTests : BaseTest
{
    [Test]
    public async Task Can_Get_User_By_Id()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };

        var commandResult = await Mediator.Send(new CreateUserCommand(dto, true)).ConfigureAwait(false);

        // Act
        var queryResult = await Mediator.Send(new GetUserByIdQuery(commandResult.Value)).ConfigureAwait(false);

        // Assert
        await Assert.That(queryResult.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Cannot_Get_User_By_Id_Not_Found()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var queryResult = await Mediator.Send(new GetUserByIdQuery(Id.From(id))).ConfigureAwait(false);

        // Assert
        await Assert.That(queryResult.IsFailed).IsTrue();
        await Assert.That(queryResult.Errors).HasCount().EqualToOne();
        await Assert.That(queryResult.Errors[0].Message).IsEqualTo($"User with id '{id}' not found");
    }
}
