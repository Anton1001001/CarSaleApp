using AutoMapper;
using File.Core.Abstractions;
using File.Core.Common.Models;
using File.Core.CQRS.Queries.GetFilesByIds;
using File.Core.Errors;
using File.Core.Models;
using FluentAssertions;
using Moq;

namespace File.Tests.CQRS.Queries.GetFilesByIds;

public class GetFilesByIdsTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly GetFilesByIdsHandler _handler;

    public GetFilesByIdsTests()
    {
        _handler = new GetFilesByIdsHandler(_unitOfWorkMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnPhotoResponses_WhenPhotosExist()
    {
        // Arrange
        var query = TestDataFactory.CreateGetFilesByIdsQuery(TestDataFactory.CorrectIds);
        var photos = TestDataFactory.CreatePhotos();
        var photosResponse = TestDataFactory.CreatePhotoResponses();
        
        _unitOfWorkMock.Setup(u => u.PhotoRepository
                .GetByIdsAsync(query.Ids, It.IsAny<CancellationToken>()))
                .ReturnsAsync(photos);
        _mapperMock.Setup(m => m.Map<List<PhotoResponse>>(photos)).Returns(photosResponse);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(photosResponse);

        // Verify
        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.PhotoRepository
            .GetByIdsAsync(query.Ids, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<List<PhotoResponse>>(photos), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFileNotFoundError_WhenNoPhotosFound()
    {
        // Arrange
        var query = TestDataFactory.CreateGetFilesByIdsQuery(TestDataFactory.IncorrectIds);
        
        _unitOfWorkMock.Setup(unitOfWork => unitOfWork.PhotoRepository.GetByIdsAsync(query.Ids, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        
        _mapperMock.Setup(mapper => mapper.Map<List<PhotoResponse>>(It.IsAny<List<Photo>>()))
            .Returns([]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors[0].Should().BeOfType<FileNotFoundError>();

        _unitOfWorkMock.Verify(unitOfWork => unitOfWork.PhotoRepository
            .GetByIdsAsync(query.Ids, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(mapper => mapper
            .Map<List<PhotoResponse>>(new List<Photo>()), Times.Once);
    }
}