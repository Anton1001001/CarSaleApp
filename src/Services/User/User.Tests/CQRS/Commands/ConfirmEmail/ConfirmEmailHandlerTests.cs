using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.CQRS.Commands.ConfirmEmail;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ConfirmEmail;

public class ConfirmEmailHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly ConfirmEmailHandler _handler;

    public ConfirmEmailHandlerTests()
    {
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object, 
            Array.Empty<IUserValidator<ApplicationUser>>(), 
            Array.Empty<IPasswordValidator<ApplicationUser>>(),
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
        
        _handler = new ConfirmEmailHandler(_userManagerMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenTokenIsValid()
    {
        var command = TestDataFactory.CreateValidCommand();
        var user = TestDataFactory.CreateUser(command.Email);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(m => m.ConfirmEmailAsync(user, TestDataFactory.ValidToken))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _userManagerMock.Verify(m => m.ConfirmEmailAsync(user, TestDataFactory.ValidToken), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserNotFound()
    {
        var command = TestDataFactory.CreateInvalidEmailCommand();

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email))
            .ReturnsAsync((ApplicationUser?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(m => m.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailAlreadyConfirmed()
    {
        var command = TestDataFactory.CreateAlreadyConfirmedCommand();
        var user = TestDataFactory.CreateUser(command.Email, emailConfirmed: true);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(m => m.ConfirmEmailAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenTokenIsInvalid()
    {
        var command = TestDataFactory.CreateInvalidTokenCommand();
        var user = TestDataFactory.CreateUser(command.Email);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);

        _userManagerMock.Setup(m => m.ConfirmEmailAsync(user, TestDataFactory.InvalidToken))
            .ReturnsAsync(IdentityResult.Failed());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        result.Errors[0].Message.Should().Contain("токен недействителен");

        _userManagerMock.Verify(m => m.ConfirmEmailAsync(user, TestDataFactory.InvalidToken), Times.Once);
    }
}