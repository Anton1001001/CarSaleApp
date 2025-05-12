using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.Abstractions;
using User.Core.CQRS.Commands.ForgotPasswordByEmail;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ForgotPasswordByEmail;

public class ForgotPasswordByEmailHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<IUserEmailService> _emailServiceMock;
    private readonly ForgotPasswordByEmailHandler _handler;

    public ForgotPasswordByEmailHandlerTests()
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

        _emailServiceMock = new Mock<IUserEmailService>();
        _handler = new ForgotPasswordByEmailHandler(_userManagerMock.Object, _emailServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenEmailIsValidAndConfirmed()
    {
        var command = TestDataFactory.CreateValidCommand();
        var user = TestDataFactory.CreateUser(command.Email, emailConfirmed: true);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email)).ReturnsAsync(user);
        _emailServiceMock.Setup(m => m.SendForgotPasswordEmailAsync(user)).ReturnsAsync(true);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _emailServiceMock.Verify(s => s.SendForgotPasswordEmailAsync(user), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailIsEmpty()
    {
        var command = TestDataFactory.CreateEmptyEmailCommand();

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
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
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailIsNotConfirmed()
    {
        var command = TestDataFactory.CreateUnconfirmedEmailCommand();
        var user = TestDataFactory.CreateUser(command.Email, emailConfirmed: false);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email))
            .ReturnsAsync(user);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailSendingFails()
    {
        var command = TestDataFactory.CreateValidCommand();
        var user = TestDataFactory.CreateUser(command.Email, emailConfirmed: true);

        _userManagerMock.Setup(m => m.FindByEmailAsync(command.Email)).ReturnsAsync(user);
        _emailServiceMock.Setup(m => m.SendForgotPasswordEmailAsync(user)).ReturnsAsync(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();
    }
}