using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZadanieDomoweAPBD9.Models;

[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; }
    public int IdPrescription { get; set; }
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
    public int? Dose { get; set; }
    [MaxLength(100)]
    public string Details { get; set; }
}
