using WebApplication2.Entity;

namespace WebApplication2.Services.IService;

public interface IStudentService
{
    public List<Student> GetAll();
    public Student GetStudentById(string id);
    public Student AddStudent(Student student);
    public Student UpdateStudent(string id, Student student);
    public void DeleteStudent(string id);

}