using Chat.Core.Abstractions;
using Chat.Core.CQRS.Queries.CheckDialogByAdvertId;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentAssertions;
using Moq;

namespace Chat.Tests.CQRS.Queries.CheckDialogByAdvertId;

public class CheckDialogByAdvertIdHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAdvertServiceClient> _advertServiceMock;
    private readonly CheckDialogByAdvertIdHandler _handler;

    public CheckDialogByAdvertIdHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _advertServiceMock = new Mock<IAdvertServiceClient>();
        _handler = new CheckDialogByAdvertIdHandler(_unitOfWorkMock.Object, _advertServiceMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenUserIdIsNull()
    {
        var query = TestDataFactory.QueryWithNullUserId();
        var response = await _handler.Handle(query, default);

        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<UnauthorizedError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenUserIdIsInvalid()
    {
        var query = TestDataFactory.QueryWithInvalidUserId();
        var response = await _handler.Handle(query, It.IsAny<CancellationToken>());

        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<BadRequestError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenAdvertIsMissing()
    {
        var query = TestDataFactory.ValidQuery();

        _advertServiceMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(TestDataFactory.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AdvertPreviewResponse?)null);

        var response = await _handler.Handle(query, default);

        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<NotFoundError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenUserIsSeller()
    {
        var query = TestDataFactory.ValidQuery();
        var advertInfo = TestDataFactory.CreateAdvertPreviewResponse(fromSeller: true);

        _advertServiceMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(TestDataFactory.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertInfo);

        var response = await _handler.Handle(query, default);

        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<BadRequestError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnResponseWithDialogId_WhenDialogExists()
    {
        var query = TestDataFactory.ValidQuery();
        var advertInfo = TestDataFactory.CreateAdvertPreviewResponse();
        var dialog = TestDataFactory.CreateDialog();

        _advertServiceMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(TestDataFactory.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertInfo);

        _unitOfWorkMock
            .Setup(x => x.DialogRepository.GetDialogAsync(
                TestDataFactory.AdvertId,
                TestDataFactory.SellerId,
                TestDataFactory.BuyerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(dialog);

        var response = await _handler.Handle(query, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Id.Should().Be(dialog.Id);
        response.Value.AdvertInfo.Should().BeEquivalentTo(advertInfo);
    }

    [Fact]
    public async Task Handle_ShouldReturnResponseWithoutDialogId_WhenDialogDoesNotExist()
    {
        var query = TestDataFactory.ValidQuery();
        var advertInfo = TestDataFactory.CreateAdvertPreviewResponse();

        _advertServiceMock
            .Setup(x => x.GetAdvertPreviewByIdAsync(TestDataFactory.AdvertId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(advertInfo);

        _unitOfWorkMock
            .Setup(x => x.DialogRepository.GetDialogAsync(
                TestDataFactory.AdvertId,
                TestDataFactory.SellerId,
                TestDataFactory.BuyerId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Dialog?)null);

        var response = await _handler.Handle(query, default);

        response.IsSuccess.Should().BeTrue();
        response.Value.Id.Should().BeNull();
        response.Value.AdvertInfo.Should().BeEquivalentTo(advertInfo);
    }
}
