using System.Text;

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Quicken.DateFixer.Domain;
using Quicken.DateFixer.Services.Contracts;

namespace Quicken.DateFixer.Services
{
    public class CloudFileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly IConfiguration _configuration;

        public CloudFileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _blobServiceClient = new BlobServiceClient(_configuration["AzureStorage:ConnectionString"]);
            _containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureStorage:ContainerName"]);
        }

        public async Task<string> CreateFileAsync(IFormFile formFile)
        {
            BlobContentInfo response;

            try
            {
                response = await _containerClient.UploadBlobAsync(formFile.FileName, formFile.OpenReadStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return formFile.FileName;
        }

        public async Task<string> ReadFileAsync(string fileName)
        {
            var blobClient = _containerClient.GetBlobClient(fileName);

            if (!await blobClient.ExistsAsync())
            {
                throw new FileNotFoundException(fileName);
            }

            var blobDownloadInfo = await blobClient.DownloadContentAsync();
            return blobDownloadInfo.Value.Content.ToString();
        }

        public async Task WriteFileAsync(Account accountName, string file)
        {
            var blobClient = _containerClient.GetBlobClient($"{accountName}_Statement_NEW.qif");

            using var contentStream = new MemoryStream(Encoding.UTF8.GetBytes(file));
            await blobClient.UploadAsync(contentStream, true);
        }
    }
}
