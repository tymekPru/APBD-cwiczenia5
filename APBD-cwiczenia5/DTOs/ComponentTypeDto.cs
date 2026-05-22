namespace APBD_Cwiczenia5.DTOs;

// DTO zwracane przez API z info o typie komponentu
public class ComponentTypeDto
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string Name { get; set; } = null!;
}
