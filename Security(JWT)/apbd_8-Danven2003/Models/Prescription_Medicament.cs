using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zadanie8.Models;

public class Prescription_Medicament
{
    [Key, Column(Order = 0)]
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }
    [Key, Column(Order = 1)]
    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }
    public int? Dose { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }

    public Prescription Prescription { get; set; }

    public Medicament Medicament { get; set; }


}