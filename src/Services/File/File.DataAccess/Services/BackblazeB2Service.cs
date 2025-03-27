using Amazon.S3;
using Amazon.S3.Model;
using File.Core.Abstractions;
using File.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace File.DataAccess.Services;

public class BackblazeB2Service(IOptions<BackblazeB2Options> options, IAmazonS3 s3Client) : IStorageService
{ 
    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var config = options.Value;
        var request = new PutObjectRequest
        {
            BucketName = config.BucketName,
            Key = fileName,
            InputStream = fileStream
        };
        await s3Client.PutObjectAsync(request);
        return $"https://{config.BucketName}.{config.Endpoint}/{fileName}";
    }

    public async Task<bool> RemoveFilesAsync(List<int> ids)
    {
        var config = options.Value;
        var objectsToDelete = new List<KeyVersion>();

        foreach (var id in ids)
        {
            var listObjectsRequest = new ListObjectsV2Request
            {
                BucketName = config.BucketName,
                Prefix = $"{id}/"
            };

            var listObjectsResponse = await s3Client.ListObjectsV2Async(listObjectsRequest);

            if (listObjectsResponse.S3Objects.Count > 0)
            {
                var objectsForId = listObjectsResponse.S3Objects
                    .Select(s3Object => new KeyVersion { Key = s3Object.Key })
                    .ToList();

                objectsToDelete.AddRange(objectsForId);
            }
        }

        if (objectsToDelete.Count == 0)
            return false;

        var deleteRequest = new DeleteObjectsRequest
        {
            BucketName = config.BucketName,
            Objects = objectsToDelete,
            Quiet = true
        };

        var response = await s3Client.DeleteObjectsAsync(deleteRequest);
        return response.DeleteErrors.Count == 0;
    }

}