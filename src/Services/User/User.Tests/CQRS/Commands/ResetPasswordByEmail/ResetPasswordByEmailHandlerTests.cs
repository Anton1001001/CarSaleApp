using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.CQRS.Commands.ResetPasswordByEmail;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ResetPasswordByEmail;

public class ResetPasswordByEmailHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly ResetPasswordByEmailHandler _handler;

    public ResetPasswordByEmailHandlerTests()
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
        _handler = new ResetPasswordByEmailHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPasswordResetIsSuccessful()
    {
        // Arrange
        var command = TestDataFactory.ResetPasswordByEmailCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new ApplicationUser { Email = command.Email });
        _userManagerMock.Setup(x =>
                x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), command.NewPassword))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<ResetPasswordByEmailResponse>();

        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userManagerMock.Verify(
            x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), command.NewPassword),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserNotFound()
    {
        // Arrange
        var command = TestDataFactory.ResetPasswordByEmailCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userManagerMock.Verify(
            x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), command.NewPassword),
            Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenResetPasswordFails()
    {
        // Arrange
        var command = TestDataFactory.ResetPasswordByEmailCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new ApplicationUser { Email = command.Email });
        _userManagerMock.Setup(x =>
                x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), command.NewPassword))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userManagerMock.Verify(
            x => x.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), command.NewPassword),
            Times.Once);
    }
}