using Microsoft.AspNetCore.Mvc;
using Quicken.DateFixer.Api.DTOs;

namespace Quicken.DateFixer.Api.Controllers
{
    [Route("api/quicken")]
    [ApiController]
    public class QuickenController : ControllerBase
    {
        //private readonly IQuickenService _quickenService = quickenService;

        //[HttpPost]
        //public async Task<IActionResult> Upload([FromBody] FileDto fileDto)
        //{
        //    try
        //    {
        //        if (fileDto is null || string.IsNullOrEmpty(fileDto.Bytes))
        //        {
        //            return BadRequest("No file uploaded.");
        //        }

        //        var filePath = await _quickenService.CreateFile(fileDto!);

        //        var service = await _quickenService.UpdateFile(fileDto!.AccountName.ToString(), filePath);

        //        return Ok("File uploaded successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
