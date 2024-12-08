

using Entities;
using Microsoft.AspNetCore.Mvc;
using repositories;

[ApiController]
[Route("api/[controller]")]
public class PrestamoController : ControllerBase{


    private readonly IPrestamoRepository prestamoRepository;

    public PrestamoController(IPrestamoRepository prestamoRepository)
    
    {
        this.prestamoRepository = prestamoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prestamo>>> GetPrestamos()
    {
        var bomberos = await prestamoRepository.GetAllAsync();
        return Ok(bomberos);
    }

        [HttpGet("{id}")]
    public async Task<ActionResult<Prestamo>> GetPrestamo(int id)
    {
        var prestamo = await prestamoRepository.GetByIdAsync(id);
        if (prestamo == null)
        {
            return NotFound();
        }
        return Ok(prestamo);
    }

    [HttpPost]
    public async Task<ActionResult<Prestamo>> PostPrestamo(Prestamo prestamo)
    {
        var saved= await prestamoRepository.SaveAsync(prestamo);
        if(!saved){
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetPrestamo), new { id = prestamo.id }, prestamo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPrestamo(int id, Prestamo prestamo)
    {
        if (id != prestamo.id)
        {
            return BadRequest();
        }
        var saved = await prestamoRepository.UpdateAsync(prestamo);
        if(!saved){
            return BadRequest();
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePrestamo(int id)
    {
        var persona = await prestamoRepository.GetByIdAsync(id);
        if (persona == null)
        {
            return NotFound();
        }
        var saved = await prestamoRepository.DeleteAsync(id);
        if(!saved){
            return BadRequest();
        }
        return NoContent();
    }


}