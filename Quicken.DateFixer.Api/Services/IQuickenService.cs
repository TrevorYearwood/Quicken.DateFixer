using Quicken.DateFixer.Api.DTOs;

namespace Quicken.DateFixer.Api.Services
{
    public interface IQuickenService
    {
        Task<string> UpdateFile(string accountName, string filePath);

        Task<string> CreateFile(FileDto fileDto);
    }
}