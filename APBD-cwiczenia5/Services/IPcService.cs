using APBD_Cwiczenia5.DTOs;

namespace APBD_Cwiczenia5.Services;

public interface IPcService
{
    Task<IEnumerable<PcListDto>> GetAllAsync();

    Task<PcWithComponentsDto?> GetComponentsAsync(int pcId);

    Task<PcListDto> CreateAsync(CreatePcDto dto);

    Task<PcListDto?> UpdateAsync(int id, UpdatePcDto dto);

    Task<bool> DeleteAsync(int id);
}
