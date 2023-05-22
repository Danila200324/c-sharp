using CsvHelper;
using WebApplication2.Entity;
using WebApplication2.Services.IService;

namespace WebApplication2.Services.ServiceImpl;

public class StudentServiceImpl : IStudentService
{
    //I don't know why, but rider doesn't see the file if I choose the path from the content root
    //or just the name of the file. The problem is not in the system of localisation for different OS
    //(like differences of Mac or Windows). I have spent many time for searching the solution but unfortunetelly
    //for now I don't have it. That's why I fill the full path. P.S: in other ide's like idea, pycharm 
    //everything is working without any problems
    private const string Path = "C:\\Users\\danka\\c-projects\\tutorial1_ja-Danven2003\\Rest\\Rest\\Data\\students.csv";
    public List<Student> GetAll()
    {
        using var reader = new StreamReader(Path);
        using var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture);
        {
            var students = csv.GetRecords<Student>().ToList();
            return students;
        }
    }

    public Student GetStudentById(string id)
    {
        var student = GetAll().Find(x => x.StudentNumber.Equals(id));
        if (student != null) return student;
        throw new Exception("The student with id " + id + " does not exist");
    }

    public Student AddStudent(Student student)
    {
        var checkStudent = GetAll().Find(x => x.StudentNumber.Equals(student.StudentNumber));
        if (checkStudent is not null) throw new Exception("The element with such id already exists");
        using var writer = new StreamWriter(Path, true);
        using var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture);
        {
            csv.WriteRecord(student);
        }
        return student;
    }

    public Student UpdateStudent(string id, Student student)
    {
        var students = GetAll();
        var oldStudent = students.Find(x => x.StudentNumber.Equals(id));
        if (oldStudent is null) throw new Exception("The student with id + " + id + "does not exist");
        oldStudent.Email = student.Email;
        oldStudent.Name = student.Name;
        oldStudent.Surname = student.Surname;
        oldStudent.FatherName = student.FatherName;
        oldStudent.MotherName = student.MotherName;
        oldStudent.StudyDirection = student.StudyDirection;
        oldStudent.StudyMode = student.StudyMode;
        oldStudent.DateOfBirth = student.DateOfBirth;
        using var writer = new StreamWriter(Path);
        using var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture);
        {
            csv.WriteRecords(students);
        }
        return oldStudent;
    }

    public void DeleteStudent(string id)
    {
        var students = GetAll();
        var student = students.Find(x => x.StudentNumber.Equals(id));
        if (student is null) throw new Exception("There is no student with id " + id);
        students.Remove(student);
        using var writer = new StreamWriter(Path);
        using var csv = new CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture);
        {
            csv.WriteRecords(students);
        }
    }
    
}