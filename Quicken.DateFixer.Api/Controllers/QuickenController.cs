using Microsoft.AspNetCore.Mvc;
using Quicken.DateFixer.Api.Services;

namespace Quicken.DateFixer.Api.Controllers
{
    [Route("api/quicken")]
    [ApiController]
    public class QuickenController : ControllerBase
    {
        private readonly IQuickenService _quickenService;

        public QuickenController(IQuickenService quickenService)
        {
            _quickenService = quickenService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile fileUpload)
        {
            try
            {
                if (fileUpload is null || fileUpload.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var filePath = Path.GetTempFileName();

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                var service = await _quickenService.UpdateFile(fileUpload.FileName, filePath);

                return Ok("File uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
