using APBD_Cwiczenia5.Database;
using APBD_Cwiczenia5.DTOs;
using APBD_Cwiczenia5.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Cwiczenia5.Services;

public class PcService(AppDbContext context) : IPcService
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PcListDto>> GetAllAsync()
    {
        return await _context.PCs
            .AsNoTracking()
            .Select(p => new PcListDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            })
            .ToListAsync();
    }

    public async Task<PcWithComponentsDto?> GetComponentsAsync(int pcId)
    {
        var pc = await _context.PCs
            .AsNoTracking()
            .Include(p => p.PCComponents)
                .ThenInclude(pcc => pcc.Component)
                    .ThenInclude(c => c.Manufacturer)
            .Include(p => p.PCComponents)
                .ThenInclude(pcc => pcc.Component)
                    .ThenInclude(c => c.Type)
            .FirstOrDefaultAsync(p => p.Id == pcId);

        if (pc is null)
        {
            return null;
        }

        return new PcWithComponentsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents
                .Select(pcc => new PcComponentDto
                {
                    Amount = pcc.Amount,
                    Component = new ComponentDto
                    {
                        Code = pcc.Component.Code,
                        Name = pcc.Component.Name,
                        Description = pcc.Component.Description,
                        Manufacturer = new ComponentManufacturerDto
                        {
                            Id = pcc.Component.Manufacturer.Id,
                            Abbreviation = pcc.Component.Manufacturer.Abbreviation,
                            FullName = pcc.Component.Manufacturer.FullName,
                            FoundationDate = pcc.Component.Manufacturer.FoundationDate
                        },
                        Type = new ComponentTypeDto
                        {
                            Id = pcc.Component.Type.Id,
                            Abbreviation = pcc.Component.Type.Abbreviation,
                            Name = pcc.Component.Type.Name
                        }
                    }
                })
                .ToList()
        };
    }

    public async Task<PcListDto> CreateAsync(CreatePcDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _context.PCs.Add(pc);
        await _context.SaveChangesAsync();

        return MapToListDto(pc);
    }

    public async Task<PcListDto?> UpdateAsync(int id, UpdatePcDto dto)
    {
        var pc = await _context.PCs.FirstOrDefaultAsync(p => p.Id == id);

        if (pc is null)
        {
            return null;
        }

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _context.SaveChangesAsync();

        return MapToListDto(pc);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.PCs
            .Include(p => p.PCComponents)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pc is null)
        {
            return false;
        }

        // Powiązane rekordy PCComponents zostaną usunięte kaskadowo
        // zgodnie z konfiguracją relacji w AppDbContext.
        _context.PCs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }

    private static PcListDto MapToListDto(PC pc) => new()
    {
        Id = pc.Id,
        Name = pc.Name,
        Weight = pc.Weight,
        Warranty = pc.Warranty,
        CreatedAt = pc.CreatedAt,
        Stock = pc.Stock
    };
}
