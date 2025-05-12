using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.Abstractions;
using User.Core.CQRS.Commands.SignInByEmail;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.SignInByEmail;

public class SignInByEmailHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly SignByEmailHandler _handler;

    public SignInByEmailHandlerTests()
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

        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            new Mock<IHttpContextAccessor>().Object,
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<ApplicationUser>>().Object);
        
        _jwtServiceMock = new Mock<IJwtService>();
        
        _handler = new SignByEmailHandler(_userManagerMock.Object, _signInManagerMock.Object, _jwtServiceMock.Object,
            new Mock<IHttpContextAccessor>().Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenSignInIsSuccessful()
    {
        // Arrange
        var command = TestDataFactory.SignInByEmailCommand;
        var user = TestDataFactory.GetTestUser(command.Email);
        _userManagerMock.Setup(x => x.FindByNameAsync(command.Email))
            .ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, command.Password, false))
            .ReturnsAsync(SignInResult.Success);
        _jwtServiceMock.Setup(x => x.CreateAccessToken(user))
            .Returns("accessToken");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<SignInByEmailResponse>();
        result.Value.UserId.Should().Be(user.Id);
        result.Value.Email.Should().Be(user.Email);
        result.Value.Name.Should().Be(user.Name);
        result.Value.AccessToken.Should().Be("accessToken");

        _userManagerMock.Verify(x => x.FindByNameAsync(command.Email), Times.Once);
        _signInManagerMock.Verify(x => x.CheckPasswordSignInAsync(user, command.Password, false), Times.Once);
        _jwtServiceMock.Verify(x => x.CreateAccessToken(user), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserNotFound()
    {
        // Arrange
        var command = TestDataFactory.SignInByEmailCommand;
        _userManagerMock.Setup(x => x.FindByNameAsync(command.Email))
            .ReturnsAsync(null as ApplicationUser);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(x => x.FindByNameAsync(command.Email), Times.Once);
        _signInManagerMock.Verify(
            x => x.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenEmailNotConfirmed()
    {
        // Arrange
        var command = TestDataFactory.SignInByEmailCommand;
        var user = TestDataFactory.GetTestUser(command.Email, false);
        _userManagerMock.Setup(x => x.FindByNameAsync(command.Email))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(x => x.FindByNameAsync(command.Email), Times.Once);
        _signInManagerMock.Verify(
            x => x.CheckPasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenPasswordIncorrect()
    {
        // Arrange
        var command = TestDataFactory.SignInByEmailCommand;
        var user = TestDataFactory.GetTestUser(command.Email);
        _userManagerMock.Setup(x => x.FindByNameAsync(command.Email))
            .ReturnsAsync(user);
        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, command.Password, false))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(x => x.FindByNameAsync(command.Email), Times.Once);
        _signInManagerMock.Verify(x => x.CheckPasswordSignInAsync(user, command.Password, false), Times.Once);
    }
}