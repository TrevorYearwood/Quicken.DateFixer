using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Quicken.DateFixer.Api.DTOs;
using Quicken.DateFixer.Services.Contracts;

namespace Quicken.DateFixer.MinApi.EndpointHandlers
{
    public static class QuickenEndpoints
    {
        public static async Task<Created> ProcessQuickenFileAsync(IQuickenService quickenService, [FromForm] FileDto fileDto)
        {
            await quickenService.ProcessFile(fileDto);

            return TypedResults.Created("File uploaded successfully.");
        }
    }
}
