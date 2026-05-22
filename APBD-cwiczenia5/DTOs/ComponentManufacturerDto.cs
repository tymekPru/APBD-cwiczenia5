namespace Cwiczenia5.DTOs;

// DTO zwracane przez API z info o producencie
public class ComponentManufacturerDto
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateOnly FoundationDate { get; set; }
}
