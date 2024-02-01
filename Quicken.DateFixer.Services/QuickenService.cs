using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using Quicken.DateFixer.Services.Contracts;
using Quicken.DateFixer.Api.DTOs;
using Quicken.DateFixer.Domain;

namespace Quicken.DateFixer.Services
{
    public class QuickenService(IFileService fileService) : IQuickenService
    {
        private readonly IFileService _fileService = fileService;

        public async Task<string> ProcessFile(FileDto fileDto)
        {
            ArgumentNullException.ThrowIfNull(fileDto.File);
            ArgumentNullException.ThrowIfNull(fileDto.AccountName);

            var filePath = await _fileService.CreateFileAsync(fileDto.File);
            var accountName = (Account)fileDto.AccountName;

            string fileText = await _fileService.ReadFileAsync(filePath);

            string[] splitText = ConvertDateFormat(fileText);

            fileText = CreateFileHeader(accountName, splitText);

            await _fileService.WriteFileAsync(accountName, fileText);

            return "success";
        }

        private static string[] ConvertDateFormat(string fileText)
        {
            var splitText = fileText.Split('\n');
            Regex regex = new(@"^[D][0-9]{2}[\/][0-9]{2}[\/][0-9]{4}");

            foreach (var item in splitText)
            {
                if (regex.IsMatch(item))
                {
                    var date = item.Replace("D", "");
                    DateTime convertedDate = DateTime.Parse(date);
                    var usDate = convertedDate.ToString("MM/dd/yyyy", new CultureInfo("en-US"));
                    splitText[Array.IndexOf(splitText, item)] = $"D{usDate}";
                }
            }

            return splitText;
        }

        private static string CreateFileHeader(Account accountName, string[] splitText)
        {
            var header = new StringBuilder();
            header.AppendLine("!Account");
            header.AppendLine($"N{accountName} 123 Mini");
            header.AppendLine("TBank");
            header.AppendLine("^");

            return header.ToString() + string.Join("\n", splitText);
        }
    }
}
