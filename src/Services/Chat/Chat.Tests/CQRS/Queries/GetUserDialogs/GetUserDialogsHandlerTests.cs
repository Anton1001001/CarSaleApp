using Chat.Core.Abstractions;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentAssertions;
using Moq;

namespace Chat.Tests.CQRS.Queries.GetUserDialogs;

public class GetUserDialogsHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAdvertServiceClient> _advertServiceClientMock;
    private readonly GetUserDialogsHandler _handler;
    
    public GetUserDialogsHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _advertServiceClientMock = new Mock<IAdvertServiceClient>();
        _handler = new GetUserDialogsHandler(_unitOfWorkMock.Object, _advertServiceClientMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnDialogs_WhenValidRequest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userIdStr = userId.ToString();
        var query = new GetUserDialogsQuery(userIdStr);
        var dialog = TestDataFactory.CreateDialog(id: 1, userId, advertId: 100);

        var dialogs = new List<Dialog> { dialog };

        var advertPreview = TestDataFactory.CreateAdvertPreview(dialog.AdvertId, dialog.SellerId.ToString());
        
        _unitOfWorkMock.Setup(x => x.DialogRepository
                .GetDialogsByUserIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(dialogs);
        
        _advertServiceClientMock.Setup(x => x
                .GetAdvertsPreviewsByIdsAsync(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([advertPreview]);
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
        result.Value[0].DialogId.Should().Be(dialog.Id);
        result.Value[0].IsAdvertOwner.Should().Be(dialog.SellerId == userId);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenUserIdIsNull()
    {
        // Arrange
        var query = new GetUserDialogsQuery(null);

        // Act
        var response = await _handler.Handle(query, It.IsAny<CancellationToken>());

        // Assert
        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<UnauthorizedError>();
    }
}