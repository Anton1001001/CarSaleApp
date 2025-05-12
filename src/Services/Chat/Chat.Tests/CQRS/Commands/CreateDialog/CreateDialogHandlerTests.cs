using Chat.Core.Abstractions;
using Chat.Core.CQRS.Commands.CreateDialog;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentAssertions;
using Moq;

namespace Chat.Tests.CQRS.Commands.CreateDialog;

public class CreateDialogHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDialogRepository> _dialogRepositoryMock;
    private readonly Mock<IMessageRepository> _messageRepositoryMock;
    private readonly Mock<IAdvertServiceClient> _advertServiceClientMock;
    private readonly CreateDialogHandler _handler;
    
    public CreateDialogHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _dialogRepositoryMock = new Mock<IDialogRepository>();
        _messageRepositoryMock = new Mock<IMessageRepository>();
        _advertServiceClientMock = new Mock<IAdvertServiceClient>();

        _unitOfWorkMock.SetupGet(x => x.DialogRepository)
            .Returns(_dialogRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.MessageRepository)
            .Returns(_messageRepositoryMock.Object);

        _handler = new CreateDialogHandler(
            _unitOfWorkMock.Object,
            _advertServiceClientMock.Object
        );
    }
    
    [Fact]
    public async Task Handle_ShouldReturnCreateDialogResponse_WhenDataIsValid()
    {
        // Arrange
        var command = TestDataFactory.CreateValidDialogCommand();
        var advertPreview = TestDataFactory.CreateAdvertPreviewResponse();
        var dialog = TestDataFactory.CreateDialog();
        var message = TestDataFactory.CreateMessage(dialog);

        _advertServiceClientMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(command.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertPreview);

        _dialogRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Dialog>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dialog);

        _messageRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(message);

        _unitOfWorkMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.AdvertInfo.Id.Should().Be(command.AdvertId);
        result.Value.Name.Should().Be(command.Name);
        
        _advertServiceClientMock.Verify(
            x => x.GetAdvertPreviewByIdAsync(command.AdvertId, It.IsAny<CancellationToken>()),
            Times.Once);

        _dialogRepositoryMock.Verify(
            x => x.CreateAsync(It.Is<Dialog>(d =>
                d.AdvertId == advertPreview.Id &&
                d.BuyerId.ToString() == command.UserId &&
                d.SellerId.ToString() == advertPreview.SellerId &&
                d.Name == command.Name &&
                d.LastMessage == command.Text
            ), It.IsAny<CancellationToken>()), Times.Once);

        _messageRepositoryMock.Verify(
            x => x.CreateAsync(It.Is<Message>(m =>
                m.Text == command.Text &&
                m.SenderId.ToString() == command.UserId
            ), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnBadRequestError_WhenUserIsSeller()
    {
        // Arrange
        var command = TestDataFactory.CreateValidDialogCommand();
        var advertPreview = TestDataFactory.CreateAdvertPreviewResponse();

        advertPreview = advertPreview with { SellerId = command.UserId };

        _advertServiceClientMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(command.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertPreview);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Should().BeOfType<BadRequestError>();
        
        _dialogRepositoryMock.Verify(x => x
            .CreateAsync(It.IsAny<Dialog>(), It.IsAny<CancellationToken>()), Times.Never);
        _messageRepositoryMock.Verify(x => x
            .CreateAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(x => x
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnNotFoundError_WhenAdvertPreviewIsNull()
    {
        // Arrange
        var command = TestDataFactory.CreateValidDialogCommand();

        _advertServiceClientMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(command.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as AdvertPreviewResponse);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();
        result.Errors[0].Should().BeOfType<NotFoundError>();

        _dialogRepositoryMock.Verify(x => x
            .CreateAsync(It.IsAny<Dialog>(), It.IsAny<CancellationToken>()), Times.Never);
        _messageRepositoryMock.Verify(x => x
            .CreateAsync(It.IsAny<Message>(), It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(x => x
            .SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}