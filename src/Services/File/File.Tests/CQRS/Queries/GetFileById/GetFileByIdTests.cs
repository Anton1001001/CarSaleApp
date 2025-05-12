using AutoMapper;
using File.Core.Abstractions;
using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFileById;
using File.Core.Errors;
using File.Core.Models;
using FluentAssertions;
using Moq;

namespace File.Tests.CQRS.Queries.GetFileById;

public class GetFileByIdTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly GetFileByIdHandler _handler;

    public GetFileByIdTests()
    {
        _handler = new GetFileByIdHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPhotoResponse_WhenPhotoExists()
    {
        // Arrange
        var query = TestDataFactory.CreateGetFileByIdQuery(TestDataFactory.CorrectPhotoId);
        var photo = TestDataFactory.CreatePhoto();
        var photoResponse = TestDataFactory.CreatePhotoResponse();

        _unitOfWorkMock.Setup(unitOfWork =>
                unitOfWork.PhotoRepository.GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(photo);
        _mapperMock.Setup(mapper => mapper.Map<PhotoResponse>(photo)).Returns(photoResponse);
        
        // Act
        var response = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        response.IsSuccess.Should().BeTrue();
        response.Value.Should().BeEquivalentTo(photoResponse);
        
        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.PhotoRepository
            .GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);
        
        _mapperMock.Verify(mapper => mapper.Map<PhotoResponse>(photo), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFileNotFoundError_WhenPhotoNotExists()
    {
        // Arrange
        var query = TestDataFactory.CreateGetFileByIdQuery(TestDataFactory.IncorrectPhotoId);
        
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.PhotoRepository
                .GetByIdAsync(query.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(null as Photo);
        
        // Act
        var response = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        response.IsSuccess.Should().BeFalse();
        response.Errors[0].Should().BeOfType<FileNotFoundError>();
        
        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.PhotoRepository
            .GetByIdAsync(query.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<PhotoResponse>(It.IsAny<Photo?>()), Times.Never);

    }

}