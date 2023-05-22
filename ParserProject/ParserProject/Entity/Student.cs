using LINQtoCSV;

namespace ConsoleApp1.Entity;

public class Student
{
        [CsvColumn(Name = "Name", FieldIndex = 2)]
        public string Name { get; init; }
        [CsvColumn(Name = "Surname", FieldIndex = 3)]
        public string Surname { get; init; }
        [CsvColumn(Name = "StudyDirection", FieldIndex = 4)]
        public string StudyDirection { get; set; }
        [CsvColumn(Name = "StudyMode", FieldIndex = 5)]
        public string StudyMode { get; init; }
        [CsvColumn(Name = "StudentNumber", FieldIndex = 1)]
        public string StudentNumber { get; set; }
        [CsvColumn(Name = "DateOfBirth", FieldIndex = 6)]
        public DateTime DateOfBirth { get; init; }
        [CsvColumn(Name = "Email", FieldIndex = 7)]
        public string Email { get; init; }
        [CsvColumn(Name = "MotherName", FieldIndex = 8)]
        public string MotherName { get; init; }
        [CsvColumn(Name = "FatherName", FieldIndex = 9)]
        public string FatherName { get; init; }
        public Student(string name, string surname, string studyDirection, string studyMode, string studentNumber, DateTime dateOfBirth, string email, string motherName, string fatherName)
        {
            Name = name;
            Surname = surname;
            StudyDirection = studyDirection;
            StudyMode = studyMode;
            StudentNumber = studentNumber;
            DateOfBirth = dateOfBirth;
            Email = email;
            MotherName = motherName;
            FatherName = fatherName;
        }
        
        public Student()
        {
            
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Surname)}: {Surname}, {nameof(StudyDirection)}: {StudyDirection}, {nameof(StudyMode)}: {StudyMode}, {nameof(StudentNumber)}: {StudentNumber}, {nameof(DateOfBirth)}: {DateOfBirth}, {nameof(Email)}: {Email}, {nameof(MotherName)}: {MotherName}, {nameof(FatherName)}: {FatherName}";
        }

        protected bool Equals(Student other)
        {
            return Name == other.Name && Surname == other.Surname && StudyDirection == other.StudyDirection && StudyMode == other.StudyMode && StudentNumber == other.StudentNumber && DateOfBirth.Equals(other.DateOfBirth) && Email == other.Email && MotherName == other.MotherName && FatherName == other.FatherName;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(Surname);
            hashCode.Add(StudyDirection);
            hashCode.Add(StudyMode);
            hashCode.Add(StudentNumber);
            hashCode.Add(DateOfBirth);
            hashCode.Add(Email);
            hashCode.Add(MotherName);
            hashCode.Add(FatherName);
            return hashCode.ToHashCode();
        }
}