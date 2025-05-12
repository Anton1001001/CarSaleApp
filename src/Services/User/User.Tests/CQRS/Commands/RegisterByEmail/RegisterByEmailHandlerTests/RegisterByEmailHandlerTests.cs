using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.Abstractions;
using User.Core.CQRS.Commands.RegisterByEmail;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.RegisterByEmail.RegisterByEmailHandlerTests;

public class RegisterByEmailHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IUserEmailService> _userEmailServiceMock;
    private readonly RegisterByEmailHandler _handler;

    public RegisterByEmailHandlerTests()
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
        _handler = new RegisterByEmailHandler(_userManagerMock.Object, _userEmailServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserIsCreatedAndEmailSent()
    {
        // Arrange
        var command = TestDataFactory.RegisterByEmailCommand;
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                        .ReturnsAsync(IdentityResult.Success);
        _userEmailServiceMock.Setup(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()))
                             .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<RegisterByEmailResponse>();
        
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserCreationFails()
    {
        // Arrange
        var command = TestDataFactory.RegisterByEmailCommand;
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                        .ReturnsAsync(IdentityResult.Failed());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenEmailSendingFails()
    {
        // Arrange
        var command = TestDataFactory.RegisterByEmailCommand;
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                        .ReturnsAsync(IdentityResult.Success);
        _userEmailServiceMock.Setup(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()))
                             .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password), Times.Once);
        _userEmailServiceMock.Verify(x => x.SendConfirmationEmailAsync(It.IsAny<ApplicationUser>()), Times.Once);
    }
}