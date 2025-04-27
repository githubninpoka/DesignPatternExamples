using Microsoft.AspNetCore.Mvc;

namespace AzureStorageDemo.DataAccess
{
    public interface IAzureBlobStorage
    {
        Task<FileContentResult> DownloadFileAsync(string url);
        Task<string> UploadFileAsync(IFormFile file);
    }
}