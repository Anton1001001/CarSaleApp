/*using AutoMapper;
using File.Core.Abstractions;
using File.Core.CQRS.Commands.UploadFile;
using File.Core.Errors;
using File.Core.Errors.Base;
using File.Core.Models;
using File.Core.Options;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;

namespace File.Tests.CQRS.Commands.UploadFile;

public class UploadFileHandlerTests
{
    private readonly Mock<IStorageService> _storageServiceMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly IOptions<ImageSizeOptions> _options = Options.Create(TestDataFactory.CreateImageSizeOptions());
    private readonly UploadFileHandler _handler;

    public UploadFileHandlerTests()
    {
        _handler = new UploadFileHandler(
            _storageServiceMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _options);
    }

    [Fact]
    public async Task Handle_ShouldReturnBadRequest_WhenFormatIsNotSupported()
    {
        // Arrange
        var file = TestDataFactory.CreateMockFormFile("application/pdf");
        var command = new UploadFileCommand(file);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<FileBadRequestError>();
    }

    [Fact]
    public async Task Handle_ShouldReturnInternalServerError_WhenSaveFailsInitially()
    {
        // Arrange
        var file = TestDataFactory.CreateMockFormFile("image/png");
        var command = new UploadFileCommand(file);

        _unitOfWorkMock.Setup(u => u.PhotoRepository.AddAsync(It.IsAny<Photo>(), It.IsAny<CancellationToken>()));
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors[0].Should().BeOfType<InternalServerError>();
    }
}*/