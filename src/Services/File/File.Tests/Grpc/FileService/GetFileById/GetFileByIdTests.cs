using AutoMapper;
using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFileById;
using File.GrpcService;
using FluentAssertions;
using FluentResults;
using Grpc.Core;
using Grpc.Core.Testing;
using MediatR;
using Moq;

namespace File.Tests.Grpc.FileService.GetFileById;

public class GetFileByIdTests
{
    private readonly Mock<ISender> _senderMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GrpcService.Services.FileService _service;
    private readonly ServerCallContext _context;

    public GetFileByIdTests()
    {
        _senderMock = new Mock<ISender>();
        _mapperMock = new Mock<IMapper>();
        _service = new GrpcService.Services.FileService(_senderMock.Object, _mapperMock.Object);
        _context = TestServerCallContext.Create(
            method: "File/GetFileById",
            host: "localhost",
            deadline: DateTime.UtcNow.AddMinutes(1),
            requestHeaders: new Metadata(),
            cancellationToken: CancellationToken.None,
            peer: "localhost",
            authContext: new AuthContext("localhost", new Dictionary<string, List<AuthProperty>>()),
            contextPropagationToken: null,
            writeHeadersFunc: headers => Task.CompletedTask,
            writeOptionsGetter: () => new WriteOptions(),
            writeOptionsSetter: _ => { }
        );
    }

    [Fact]
    public async Task GetFileById_ShouldReturnMappedGrpcFile_WhenQuerySucceeds()
    {
        // Arrange
        var request = new GetFileByIdRequest { Id = TestDataFactory.FileId };
        var domainPhoto = TestDataFactory.CreateDomainPhoto();
        var grpcPhoto = TestDataFactory.CreateGrpcFile();

        _senderMock
            .Setup(s => s.Send(It.Is<GetFileByIdQuery>(q => q.Id == request.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok(domainPhoto));

        _mapperMock
            .Setup(m => m.Map<FileResponse>(domainPhoto))
            .Returns(grpcPhoto);

        // Act
        var result = await _service.GetFileById(request, _context);

        // Assert
        result.Should().NotBeNull();
        result.File.Should().BeEquivalentTo(grpcPhoto);

        _senderMock.Verify(s => s.Send(It.Is<GetFileByIdQuery>(q => q.Id == request.Id), It.IsAny<CancellationToken>()),
            Times.Once);
        _mapperMock.Verify(m => m.Map<FileResponse>(domainPhoto), Times.Once);
    }

    [Fact]
    public async Task GetFileById_ShouldThrowRpcException_WhenQueryFails()
    {
        // Arrange
        var request = new GetFileByIdRequest { Id = TestDataFactory.FileId };

        _senderMock
            .Setup(s => s.Send(It.Is<GetFileByIdQuery>(q => q.Id == request.Id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<PhotoResponse>(TestDataFactory.Error));

        // Assert
        var ex = await Assert.ThrowsAsync<RpcException>(Act);
        ex.StatusCode.Should().Be(StatusCode.NotFound);
        ex.Status.Detail.Should().Be(TestDataFactory.Error);

        _senderMock.Verify(s => s.Send(It.Is<GetFileByIdQuery>(q => q.Id == request.Id), It.IsAny<CancellationToken>()),
            Times.Once);
        _mapperMock.Verify(m => m.Map<FileResponse>(It.IsAny<PhotoResponse>()), Times.Never);
        return;

        // Act
        async Task<GetFileByIdResponse> Act() => await _service.GetFileById(request, _context);
    }
}