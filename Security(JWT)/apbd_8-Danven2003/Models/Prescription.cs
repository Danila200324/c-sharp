using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zadanie8.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [ForeignKey("Doctor")]
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }

    [ForeignKey("Patient")]
    public int IdPatient { get; set; }
    public Patient Patient { get; set; }

    public ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; }

}