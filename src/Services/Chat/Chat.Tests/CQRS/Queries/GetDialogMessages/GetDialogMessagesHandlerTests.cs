using Chat.Core.Abstractions;
using Chat.Core.CQRS.Queries.GetDialogMessages;
using Chat.Core.Errors.Base;
using FluentAssertions;
using Moq;

namespace Chat.Tests.CQRS.Queries.GetDialogMessages;

public class GetDialogMessagesHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly GetDialogMessagesHandler _handler;
    
    public GetDialogMessagesHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetDialogMessagesHandler(_unitOfWorkMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnMessagesWithCorrectIsIncoming_WhenValid()
    {
        // Arrange
        var query = TestDataFactory.CreateValidQuery();
        var messages = TestDataFactory.CreateMessageList();

        _unitOfWorkMock.Setup(u => u.MessageRepository
                .GetByDialogIdAsync(TestDataFactory.DialogId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(messages);

        // Act
        var response = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.Value.Should().HaveCount(2);

        response.Value[0].IsIncoming.Should().BeTrue();
        response.Value[1].IsIncoming.Should().BeFalse();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenUserIdIsNull()
    {
        // Arrange
        var query = TestDataFactory.CreateQueryWithNullUserId();

        // Act
        var response = await _handler.Handle(query, CancellationToken.None);

        // Assert
        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<UnauthorizedError>();
    }

}