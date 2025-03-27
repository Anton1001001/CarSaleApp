using File.Core.Common.Models;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace File.Core.CQRS.Commands.UploadFile;

public record UploadFileCommand(IFormFile File) : IRequest<Result<PhotoResponse>>;

    
