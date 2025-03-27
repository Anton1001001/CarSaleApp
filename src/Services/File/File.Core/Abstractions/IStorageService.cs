namespace File.Core.Abstractions;

public interface IStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName);
    Task<bool> RemoveFilesAsync(List<int> id);
    
}