using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zadanie8.Models;

namespace Zadanie8.Dto;

public class PrescriptionDtoResponse
{
        public int IdPrescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public DoctorDtoResponse Doctor { get; set; }
        [Required]
        public PatientDtoResponse Patient { get; set; }

        public ICollection<MedicamentDtoResponse> Medicaments { get; set; }
}