using AutoMapper;
using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFilesByIds;
using File.GrpcService;
using FluentAssertions;
using FluentResults;
using Grpc.Core;
using Grpc.Core.Testing;
using MediatR;
using Moq;

namespace File.Tests.Grpc.FileService.GetFilesByIds;

public class GetFilesByIdsTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GrpcService.Services.FileService _service;
    private readonly ServerCallContext _context;

    public GetFilesByIdsTests()
    {
        _senderMock = new Mock<ISender>();
        _mapperMock = new Mock<IMapper>();
        _service = new GrpcService.Services.FileService(_senderMock.Object, _mapperMock.Object);
        _context = TestServerCallContext.Create(
            method: "File/GetFileById",
            host: "localhost",
            deadline: DateTime.UtcNow.AddMinutes(1),
            requestHeaders: [],
            cancellationToken: CancellationToken.None,
            peer: "localhost",
            authContext: new AuthContext("localhost", new Dictionary<string, List<AuthProperty>>()),
            contextPropagationToken: null,
            writeHeadersFunc: _ => Task.CompletedTask,
            writeOptionsGetter: () => new WriteOptions(),
            writeOptionsSetter: _ => { }
        );
    }
  [Fact]
    public async Task GetFilesByIds_Should_ReturnFiles_WhenFound()
    {
        // Arrange
        var request = TestDataFactory.CreateGetFilesByIdsRequest();
        var photos = TestDataFactory.CreatePhotoResponses();
        var fileResponses = TestDataFactory.CreateFileResponses();

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetFilesByIdsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(photos));

        _mapperMock
            .Setup(m => m.Map<List<FileResponse>>(photos))
            .Returns(fileResponses);
        

        // Act
        var result = await _service.GetFilesByIds(request, _context);

        // Assert
        result.Files.Should().NotBeNull();
        result.Files.Count.Should().Be(3);
        result.Files[0].Id.Should().Be(1);

        _senderMock.Verify(s => s.Send(It.Is<GetFilesByIdsQuery>(q => q.Ids.SequenceEqual(new[] { 1, 2, 3 })), It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<List<FileResponse>>(photos), Times.Once);
    }

    [Fact]
    public async Task GetFilesByIds_Should_ThrowRpcException_WhenFailed()
    {
        // Arrange
        var request = TestDataFactory.CreateGetFilesByIdsRequest();

        _senderMock
            .Setup(s => s.Send(It.IsAny<GetFilesByIdsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<List<PhotoResponse>>("Not found"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RpcException>(() =>
            _service.GetFilesByIds(request, _context));

        exception.StatusCode.Should().Be(StatusCode.NotFound);
        exception.Status.Detail.Should().Be("Not found");

        _senderMock.Verify(s => s.Send(It.IsAny<GetFilesByIdsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<List<FileResponse>>(It.IsAny<List<PhotoResponse>>()), Times.Never);
    }
}