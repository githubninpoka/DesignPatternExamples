using Azure.Storage;
using Azure.Storage.Blobs;
using AzureStorageDemo.options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AzureStorageDemo.DataAccess;

public class AzureBlobStorage : IAzureBlobStorage
{
    private readonly AzureBlobStorageOptions _azureBlobStorage;

    public AzureBlobStorage(IOptions<AzureBlobStorageOptions> azureBlobStorageOptions)
    {
        _azureBlobStorage = azureBlobStorageOptions.Value;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        BlobContainerClient containerClient = new(
            _azureBlobStorage.ConnectionString,
            _azureBlobStorage.ContainerName
            );
        await containerClient.CreateIfNotExistsAsync();
        await containerClient.SetAccessPolicyAsync(
            Azure.Storage.Blobs.Models.PublicAccessType.None);

        BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, true);
        return blobClient.Uri.ToString();
    }

    public async Task<FileContentResult> DownloadFileAsync(string url)
    {
        Uri blobUri = new(url);
        StorageSharedKeyCredential credentials = new( 
            accountName: _azureBlobStorage.StorageAccountName,
            accountKey: _azureBlobStorage.StorageAccountKey
            );

        BlobClient blobClient = new(blobUri: blobUri, credential: credentials);

        var downloadResponse = await blobClient.DownloadStreamingAsync();
        using var memoryStream = new MemoryStream();
        await downloadResponse.Value.Content.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        return new FileContentResult(
            fileContents: memoryStream.ToArray(),
            contentType: downloadResponse.Value.Details.ContentType)
        { FileDownloadName = blobUri.Segments.Last() };
    }
}
