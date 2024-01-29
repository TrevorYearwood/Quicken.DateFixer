using Microsoft.AspNetCore.Http;

using Quicken.DateFixer.Domain;

namespace Quicken.DateFixer.Services.Contracts
{
    public interface IFileService
    {
        Task<string> CreateFileAsync(IFormFile formFile);
        Task<string> ReadFileAsync(string filePath);
        Task WriteFileAsync(Account accountName, string file);
    }
}
