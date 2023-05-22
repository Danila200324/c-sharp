using System.Linq.Expressions;
using ConsoleApp1.Entity;
using LINQtoCSV;
using System.Text.Json;
using ConsoleApp1.Logger;


namespace ConsoleApp1.Parser;

public static class Parser
{
     private static readonly CsvFileDescription CsvFileDescription = new CsvFileDescription
     {
         FirstLineHasColumnNames = true,
         IgnoreUnknownColumns = true,
         SeparatorChar = ';',
         UseFieldIndexForReadingData = false
     };
     private static readonly CsvContext CsvContext = new();
     private static readonly List<Student> Students = new();
     
     public static void Parse(string csvFile, string jsonFile)
    {
        try
        {
            foreach (var student in CsvContext.Read<Student>(csvFile, CsvFileDescription).ToHashSet())
            {
                var s = Students.Find(s => s.StudentNumber == student.StudentNumber);
                try
                {
                    if (s != null && !s.StudyDirection.Contains(student.StudyDirection)) {
                        s.StudyDirection = s.StudyDirection + ",  " + student.StudyDirection; }
                    else if (s != null)
                    {
                        throw new Exception("The nodes about student with id "
                                            + student.StudentNumber + " have different necessary values:\n" +
                                            "First node about student: " + s + "\nSecond node about student: " + student);
                    }
                    else
                    {

                        if (student.StudentNumber[0] != 's')
                        {
                            throw new Exception("The student number should start with 's'!");
                        }
                        Students.Add(student);
                    }
                    
                }
                catch (Exception e)
                {
                    LogFile.Log(e);
                    Console.WriteLine(e.Message);
                }
            }
            WriteJson(jsonFile);
            
        }
        catch (Exception e)
        {
            LogFile.Log(e);
        }
    }

     private static void WriteJson(string jsonFile)
     {
         var json = JsonSerializer.Serialize(Students);
         File.WriteAllText(jsonFile, json);
     }
    
}