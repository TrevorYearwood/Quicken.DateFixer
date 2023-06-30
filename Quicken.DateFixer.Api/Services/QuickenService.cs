using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Quicken.DateFixer.Api.Services
{
    public class QuickenService : IQuickenService
    {
        public async Task<string> UpdateFile(string accountName, string filePath)
        {
            string fileText = File.ReadAllText(filePath);

            var splitText = fileText.Split('\n');

            string pattern = @"^[D][0-9]{2}[\/][0-9]{2}[\/][0-9]{4}";
            Regex regex = new(pattern);

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

            var header = new StringBuilder();
            header.AppendLine("!Account");
            header.AppendLine($"N{accountName} 123 Mini");
            header.AppendLine("TBank");
            header.AppendLine("^");

            fileText = header.ToString() + string.Join("\n", splitText);

            await File.WriteAllTextAsync($"E:\\Downloads\\{accountName}_Statement_NEW.qif", fileText);

            return "success";
        }
    }
}
