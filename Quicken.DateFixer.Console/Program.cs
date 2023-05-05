using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

try
{
    var accountName = "Izzy";
    var filePath = @"E:\Downloads\Statement.qif";

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

    File.WriteAllText(@"E:\Downloads\Statement{accountName}NEW.qif", fileText);

    Console.WriteLine(fileText);
}
catch (Exception ex)
{
    Debug.WriteLine(ex.Message);
}

