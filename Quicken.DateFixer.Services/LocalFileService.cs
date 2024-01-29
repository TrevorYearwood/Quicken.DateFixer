using Microsoft.AspNetCore.Http;

using Quicken.DateFixer.Domain;
using Quicken.DateFixer.Services.Contracts;

namespace Quicken.DateFixer.Services
{
    public class LocalFileService : IFileService
    {
        public async Task<string> CreateFileAsync(IFormFile formFile)
        {
            var filePath = Path.GetTempFileName();

            using (var stream = File.Create(filePath))
            {
                await formFile.CopyToAsync(stream);
            }

            return filePath;
        }

        public async Task<string> ReadFileAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task WriteFileAsync(Account accountName, string file)
        {
            await File.WriteAllTextAsync($"E:\\Downloads\\{accountName}_Statement_NEW.qif", file);
        }
    }
}
