using Chat.Core.Abstractions;
using Chat.Core.CQRS.Commands.CreateMessage;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentAssertions;
using Moq;

namespace Chat.Tests.CQRS.Commands.CreateMessage;

public class CreateMessageHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateMessageHandler _handler;

    public CreateMessageHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateMessageHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnResponse_WhenDataIsValid()
    {
        // Arrange
        var command = TestDataFactory.CreateCreateMessageCommand();
        var senderId = Guid.Parse(command.UserId!);
        var message = TestDataFactory.CreateMessage(command.DialogId, senderId, command.Text);
        var dialog = TestDataFactory.CreateDialog(command.DialogId, message.Text, message.SentAt);

        _unitOfWorkMock.Setup(x => x.DialogRepository.GetByIdAsync(command.DialogId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(dialog);

        _unitOfWorkMock.Setup(x => x.MessageRepository.CreateAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
            .Callback<Message, CancellationToken>((msg, _) => msg.Id = message.Id)
            .ReturnsAsync(message);

        _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var response = result.Value;
        response.Should().NotBeNull();
        response.MessageId.Should().Be(message.Id);
        response.Text.Should().Be(command.Text);
        response.SenderId.Should().Be(senderId);

        _unitOfWorkMock.Verify(x => x.MessageRepository.CreateAsync(It
            .IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(x => x
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDialogDoesNotExist()
    {
        // Arrange
        var command = TestDataFactory.CreateCreateMessageCommand();

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(x => x.DialogRepository.GetByIdAsync(command.DialogId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Dialog?)null);

        var handler = new CreateMessageHandler(unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<NotFoundError>();

        unitOfWorkMock.Verify(x => x.MessageRepository.CreateAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()),
            Times.Never);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}