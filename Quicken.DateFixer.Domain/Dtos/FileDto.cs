using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Quicken.DateFixer.Domain;


namespace Quicken.DateFixer.Api.DTOs
{
    public class FileDto {
        public IFormFile? FormFile { get; init; } 

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public Account? AccountName {  get; init; } 
    }
}
