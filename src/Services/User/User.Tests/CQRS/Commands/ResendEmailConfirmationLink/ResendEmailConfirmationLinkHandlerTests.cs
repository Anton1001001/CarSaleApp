using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.Abstractions;
using User.Core.CQRS.Commands.ResendEmailConfirmationLink;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ResendEmailConfirmationLink;

public class ResendEmailConfirmationLinkHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IUserEmailService> _userEmailServiceMock;
    private readonly ResendEmailConfirmationLinkHandler _handler;

    public ResendEmailConfirmationLinkHandlerTests()
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
        _userEmailServiceMock = new Mock<IUserEmailService>();
        _handler = new ResendEmailConfirmationLinkHandler(_userManagerMock.Object, _userEmailServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenEmailIsValidAndEmailSent()
    {
        // Arrange
        var command = TestDataFactory.ResendEmailConfirmationLinkCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new ApplicationUser { Email = command.Email, EmailConfirmed = false });
        _userEmailServiceMock.Setup(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<ResendEmailConfirmationLinkResponse>();

        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenEmailIsEmpty()
    {
        // Arrange
        var command = new ResendEmailConfirmationLinkCommand(string.Empty);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(x => x.FindByEmailAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserNotFound()
    {
        // Arrange
        var command = TestDataFactory.ResendEmailConfirmationLinkCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenEmailAlreadyConfirmed()
    {
        // Arrange
        var command = TestDataFactory.ResendEmailConfirmationLinkCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new ApplicationUser { Email = command.Email, EmailConfirmed = true });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenEmailSendingFails()
    {
        // Arrange
        var command = TestDataFactory.ResendEmailConfirmationLinkCommand;
        _userManagerMock.Setup(x => x.FindByEmailAsync(command.Email))
            .ReturnsAsync(new ApplicationUser { Email = command.Email, EmailConfirmed = false });
        _userEmailServiceMock.Setup(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        _userManagerMock.Verify(x => x.FindByEmailAsync(command.Email), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }
}