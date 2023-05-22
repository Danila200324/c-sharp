using System.Text;

namespace ConsoleApp1.Logger;

public static class LogFile
{
    private const string PathFile = "C:\\Users\\danka\\c-projects\\tutorial1_ja-Danven2003\\ParserProject\\ParserProject\\log.txt";
    public static void Log(Exception e)
    {
        using var writer = new StreamWriter(PathFile);
        writer.WriteLine("---"+ DateTime.Now +" " + e.Message +"\n", Encoding.UTF8);
    }
}