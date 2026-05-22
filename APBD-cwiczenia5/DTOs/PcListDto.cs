namespace Cwiczenia5.DTOs;

/// DTO zwracane przez API z podstawowymi informacjami o komputerze

public class PcListDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stock { get; set; }
}
