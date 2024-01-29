namespace Quicken.DateFixer.Api.Services
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
    }
}
