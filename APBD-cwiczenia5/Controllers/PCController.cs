using APBD_Cwiczenia5.DTOs;
using APBD_Cwiczenia5.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Cwiczenia5.Controllers;

[ApiController]
[Route("api/pcs")]
public class PcsController(IPcService pcService) : ControllerBase
{
    private readonly IPcService _pcService = pcService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PcListDto>>> GetAll()
    {
        var pcs = await _pcService.GetAllAsync();
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<ActionResult<PcWithComponentsDto>> GetComponents(int id)
    {
        var pc = await _pcService.GetComponentsAsync(id);

        if (pc is null)
        {
            return NotFound();
        }

        return Ok(pc);
    }

    [HttpPost]
    public async Task<ActionResult<PcListDto>> Create([FromBody] CreatePcDto dto)
    {
        var created = await _pcService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetComponents), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PcListDto>> Update(int id, [FromBody] UpdatePcDto dto)
    {
        var updated = await _pcService.UpdateAsync(id, dto);

        if (updated is null)
        {
            return NotFound();
        }

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _pcService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
