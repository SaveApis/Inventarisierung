using Backend.Domains.User.Application.Mediator.Commands.CreateUser;
using Backend.Domains.User.Domain.Dto;
using Backend.Domains.User.Domain.VO;
using SaveApis.Core.Common.Domains.Common.Domain.VO;

namespace Inventarisierung.Tests.Domains.User.Mediator.Commands;

public class CreateUserCommandTests : BaseTest
{
    [Test]
    public async Task Can_Create_User()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };

        // Act
        var result = await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Cannot_Create_User_With_Existing_Name()
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
            FirstName = Name.From("Test"),
            LastName = Name.From("Test"),
            Email = Email.From("test2@test.test"),
            UserName = Name.From("test2"),
        };

        // Act
        var result = await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);
        var result2 = await Mediator.Send(new CreateUserCommand(dto2)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result2.IsFailed).IsTrue();
        await Assert.That(result2.Errors).HasCount().EqualToOne();
        await Assert.That(result2.Errors[0].Message).IsEqualTo($"User with Name '{dto2.FirstName} {dto2.LastName}' already exists");
    }

    [Test]
    public async Task Cannot_Create_User_With_Existing_Email()
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
            Email = Email.From("test@test.test"),
            UserName = Name.From("test"),
        };

        // Act
        var result = await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);
        var result2 = await Mediator.Send(new CreateUserCommand(dto2)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result2.IsFailed).IsTrue();
        await Assert.That(result2.Errors).HasCount().EqualToOne();
        await Assert.That(result2.Errors[0].Message).IsEqualTo($"User with Email '{dto2.Email}' already exists");
    }

    [Test]
    public async Task Cannot_Create_User_With_Existing_UserName()
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
            Email = Email.From("test2@test.test"),
            UserName = Name.From("test"),
        };

        // Act
        var result = await Mediator.Send(new CreateUserCommand(dto)).ConfigureAwait(false);
        var result2 = await Mediator.Send(new CreateUserCommand(dto2)).ConfigureAwait(false);

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result2.IsFailed).IsTrue();
        await Assert.That(result2.Errors).HasCount().EqualToOne();
        await Assert.That(result2.Errors[0].Message).IsEqualTo($"User with UserName '{dto2.UserName}' already exists");
    }
}
