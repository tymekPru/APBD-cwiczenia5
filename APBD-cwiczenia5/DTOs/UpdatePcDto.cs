using System.ComponentModel.DataAnnotations;

namespace Cwiczenia5.DTOs;

/// DTO danych przychodzących dla operacji aktualizacji komputera
public class UpdatePcDto
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
