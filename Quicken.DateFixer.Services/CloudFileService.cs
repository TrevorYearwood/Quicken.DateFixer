using Microsoft.AspNetCore.Http;

using Quicken.DateFixer.Domain;
using Quicken.DateFixer.Services.Contracts;

namespace Quicken.DateFixer.Services
{
    public class CloudFileService : IFileService
    {
        public Task<string> CreateFileAsync(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task WriteFileAsync(Account accountName, string file)
        {
            throw new NotImplementedException();
        }
    }
}
