using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Application.Mediator.Queries.GetUsers;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Inventarisierung.Tests.Domains.User.Mediator.Queries;

public class GetUsersTests : BaseTest
{
    [Test]
    public async Task Can_Get_No_Users()
    {
        // Act
        var result = await Mediator.Send(new GetUsersQuery()).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsNotNull();
        await Assert.That(result.Value).HasCount().EqualToZero();
    }

    [Test]
    public async Task Can_Get_One_User()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };

        await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);

        // Act
        var result = await Mediator.Send(new GetUsersQuery()).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsNotNull();
        await Assert.That(result.Value).HasCount().EqualToOne();

        await Assert.That(result.Value.ToList()[0].FirstName).IsEqualTo(dto.FirstName);
        await Assert.That(result.Value.ToList()[0].LastName).IsEqualTo(dto.LastName);
        await Assert.That(result.Value.ToList()[0].Email).IsEqualTo(dto.Email);
        await Assert.That(result.Value.ToList()[0].UserName).IsEqualTo(dto.UserName);
    }

    [Test]
    public async Task Can_Get_Multiple_Users()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };
        var dto2 = new UserCreateDto
        {
            FirstName = Name.From("Test2"),
            LastName = Name.From("Test2"),
            Email = Email.From("test@test.test2"),
            UserName = Name.From("test2"),
        };

        await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);
        await Mediator.Send(new CreateUserCommand(dto2)).ConfigureAwait(false);

        // Act
        var result = await Mediator.Send(new GetUsersQuery()).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsNotNull();
        await Assert.That(result.Value).HasCount().EqualTo(2);
    }
}
