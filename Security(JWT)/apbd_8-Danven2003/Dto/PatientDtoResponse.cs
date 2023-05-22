using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Dto;

public class PatientDtoResponse
{
        public int IdPatient { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
}