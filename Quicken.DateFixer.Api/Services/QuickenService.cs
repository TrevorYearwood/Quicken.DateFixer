using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace Quicken.DateFixer.Api.Services
{
    public class QuickenService : IQuickenService
    {
        public async Task<string> UpdateFile(string fileName, string filePath)
        {
            //Getting AccountName
            var fileSplit = fileName.Split(".");
            var accountName = fileSplit[0].Split("_")[1];

            //Read File - static File class
            string fileText = File.ReadAllText(filePath);

            //Date Matching
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

            //Create File Header
            var header = new StringBuilder();
            header.AppendLine("!Account");
            header.AppendLine($"N{accountName} 123 Mini");
            header.AppendLine("TBank");
            header.AppendLine("^");

            fileText = header.ToString() + string.Join("\n", splitText);

            //Write File
            await File.WriteAllTextAsync($"E:\\Downloads\\{accountName}_Statement_NEW.qif", fileText);

            return "success";
        }
    }
}
