using System.ComponentModel.DataAnnotations;

namespace APBD_Cwiczenia5.DTOs;

// DTO przychodzące dla tworzenia komputera
public class CreatePcDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Range(0.0, double.MaxValue)]
    public double Weight { get; set; }

    [Range(0, int.MaxValue)]
    public int Warranty { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
