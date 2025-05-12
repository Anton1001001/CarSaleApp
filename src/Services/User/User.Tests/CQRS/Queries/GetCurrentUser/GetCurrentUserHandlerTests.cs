using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using User.Core.CQRS.Queries.GetCurrentUser;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Tests.CQRS.Queries.GetCurrentUser;

public class GetCurrentUserHandlerTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly GetCurrentUserHandler _handler;

    public GetCurrentUserHandlerTests()
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
        
        _handler = new GetCurrentUserHandler(_userManagerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var query = TestDataFactory.CreateGetCurrentUserQuery(TestDataFactory.CorrectUserId);
        var user = TestDataFactory.CreateTestUser(query.UserId!);
    
        _userManagerMock
            .Setup(m => m.FindByIdAsync(It.Is<string>(id => id == query.UserId)))
            .ReturnsAsync(user);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be("test@example.com");
        result.Value.Name.Should().Be("Test User");

        _userManagerMock.Verify(m => m.FindByIdAsync(query.UserId!), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserIdIsNull()
    {
        // Arrange
        var query = new GetCurrentUserQuery(null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<BadRequestError>();

        _userManagerMock.Verify(m => m.FindByIdAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var query = TestDataFactory.CreateGetCurrentUserQuery(TestDataFactory.IncorrectUserId);
    
        _userManagerMock.Setup(m => m.FindByIdAsync(It.Is<string>(id => id == query.UserId)))
            .ReturnsAsync((ApplicationUser?)null);
    
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Message.Should().Contain($"User with id: {TestDataFactory.IncorrectUserId} doesn't exist");

        _userManagerMock.Verify(m => m.FindByIdAsync(query.UserId!), Times.Once);
    }

}