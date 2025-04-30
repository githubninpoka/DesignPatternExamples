using AzureStorageDemo.DataAccess;
using AzureStorageDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureStorageDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAzureBlobStorage _azureBlobs;

        public HomeController( IAzureBlobStorage azureBlobs)
        {
            _azureBlobs = azureBlobs;
        }

        public IActionResult Index()
        {
            return View(new IndexModel());
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
            IndexModel model = new();
            if (formFile is null || formFile.Length == 0)
            {
                model.PageErrors.Add("File missing. upload file");
                return View("Index", model);
            }
            string blobUri = await _azureBlobs.UploadFileAsync(formFile);
            model.FileUrl= blobUri;
            return View("Index",model);

        }

        [HttpPost]
        public async Task<IActionResult> GetFile(string fileUrl)
        {
            IndexModel model = new();
            if (string.IsNullOrWhiteSpace(fileUrl))
            {
                model.PageErrors.Add("Url missing. Provide file url");
                return View("Index", model);
            }
            var blob=await _azureBlobs.DownloadFileAsync(fileUrl);
            return blob;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
