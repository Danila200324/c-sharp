using ConsoleApp1.Logger;
using ConsoleApp1.Parser;

public class Program
{
    public static void Main(string[] args)
    {
        string csvFile = null;
        string jsonFile = null;
        try
        {
            csvFile = args[0];
            jsonFile = args[1];
        }
        catch (Exception e)
        {
            LogFile.Log(e);
        }

        if (csvFile != null && jsonFile != null)
            Parser.Parse(csvFile, jsonFile);
    }
}