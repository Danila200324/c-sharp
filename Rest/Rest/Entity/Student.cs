using System.ComponentModel.DataAnnotations;
using CsvHelper.TypeConversion;
using LINQtoCSV;
using Newtonsoft.Json;

namespace WebApplication2.Entity;

public class Student
{
        [CsvColumn(Name = "Name", FieldIndex = 2)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string Name { get; set; }

        [CsvColumn(Name = "Surname", FieldIndex = 3)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string Surname { get; set; }

        [CsvColumn(Name = "StudyDirection", FieldIndex = 4)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string StudyDirection { get; set; }

        [CsvColumn(Name = "StudyMode", FieldIndex = 5)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string StudyMode { get; set; }

        [CsvColumn(Name = "StudentNumber", FieldIndex = 1)]
        [RegularExpression(@"^s\d+$")]
        public string StudentNumber { get; set; }

        [CsvColumn(Name = "DateOfBirth", FieldIndex = 6)]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime DateOfBirth { get; set; }

        [CsvColumn(Name = "Email", FieldIndex = 7)]
        [EmailAddress]
        public string Email { get; set; }

        [CsvColumn(Name = "MotherName", FieldIndex = 8)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string MotherName { get; set; }

        [CsvColumn(Name = "FatherName", FieldIndex = 9)]
        [RegularExpression(@"^[A-Za-z -]+$")]
        public string FatherName { get; set; }

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
            return obj.GetType() == this.GetType() && Equals((Student)obj);
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