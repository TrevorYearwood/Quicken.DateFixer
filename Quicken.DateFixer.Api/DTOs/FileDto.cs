using System.Text.Json.Serialization;

namespace Quicken.DateFixer.Api.DTOs
{
    public class FileDto {
        public string? Bytes { get; init; } 

        [JsonConverter(typeof(JsonStringEnumConverter))] 
        public Account AccountName {  get; init; } 
    }
}
