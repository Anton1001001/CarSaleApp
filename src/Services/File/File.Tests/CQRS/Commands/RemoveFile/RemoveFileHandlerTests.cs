using File.Core.Abstractions;
using File.Core.CQRS.Commands.RemoveFile;
using FluentAssertions;
using Moq;

namespace File.Tests.CQRS.Commands.RemoveFile;

public class RemoveFileHandlerTests
{
    private readonly Mock<IStorageService> _storageServiceMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly RemoveFileHandler _handler;

    public RemoveFileHandlerTests()
    {
        _handler = new RemoveFileHandler(_storageServiceMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenFilesRemovedAndSaved()
    {
        // Arrange
        var command = TestDataFactory.CreateRemoveFileCommand();
        var ids = command.Ids;

        _unitOfWorkMock.Setup(u => u.PhotoRepository.DeleteAsync(ids, It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);
        _storageServiceMock.Setup(s => s.RemoveFilesAsync(ids))
                           .ReturnsAsync(true);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();

        _unitOfWorkMock.Verify(u => u.PhotoRepository.DeleteAsync(ids, It.IsAny<CancellationToken>()), Times.Once);
        _storageServiceMock.Verify(s => s.RemoveFilesAsync(ids), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnInternalServerError_WhenSaveFails()
    {
        // Arrange
        var command = TestDataFactory.CreateRemoveFileCommandWithDifferentIds();
        var ids = command.Ids;

        _unitOfWorkMock.Setup(u => u.PhotoRepository.DeleteAsync(ids, It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);
        _storageServiceMock.Setup(s => s.RemoveFilesAsync(ids))
                           .ReturnsAsync(true);
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Failed to save data");

        _unitOfWorkMock.Verify(u => u.PhotoRepository.DeleteAsync(ids, It.IsAny<CancellationToken>()), Times.Once);
        _storageServiceMock.Verify(s => s.RemoveFilesAsync(ids), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
