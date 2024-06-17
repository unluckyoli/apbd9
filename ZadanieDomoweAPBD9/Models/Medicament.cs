using System.ComponentModel.DataAnnotations;

namespace ZadanieDomoweAPBD9.Models;

public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
    public ICollection<PrescriptionMedicament> PrescriptionsMedicaments { get; set; } = new HashSet<PrescriptionMedicament>();

}
