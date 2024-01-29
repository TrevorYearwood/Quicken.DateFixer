using Quicken.DateFixer.Api.DTOs;

namespace Quicken.DateFixer.Services.Contracts
{
    public interface IQuickenService
    {
        Task<string> ProcessFile(FileDto fileDto);
    }
}