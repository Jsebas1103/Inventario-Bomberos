using Entities;
using Microsoft.AspNetCore.Mvc;
using repositories;

namespace controller;

[ApiController]
[Route("api/[controller]")]
public class DetallePrestamoController : ControllerBase{


    private readonly IDetallePrestamoRepository detallePrestamoRepository;


    public DetallePrestamoController(IDetallePrestamoRepository detallePrestamoRepository)
    
    {
        this.detallePrestamoRepository = detallePrestamoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetallePrestamo>>> GetBomberos()
    {
        var bomberos = await detallePrestamoRepository.GetAllAsync();
        return Ok(bomberos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetallePrestamo>> GetBombero(int id)
    {
        var bombero = await detallePrestamoRepository.GetByIdAsync2(id);
        if (bombero == null)
        {
            return NotFound();
        }
        return Ok(bombero);
    }

    [HttpPost]
    public async Task<ActionResult<DetallePrestamo>> PostPersona(DetallePrestamo detallePrestamo)
    {
        var saved= await detallePrestamoRepository.SaveAsync(detallePrestamo);
        if(!saved){
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetBombero), new { id = detallePrestamo.idPrestamo }, detallePrestamo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBombero(int id, DetallePrestamo detallePrestamo)
    {
        if (id != detallePrestamo.idPrestamo)
        {
            return BadRequest();
        }
        var saved = await detallePrestamoRepository.UpdateAsync(detallePrestamo);
        if(!saved){
            return BadRequest();
        }
        
        return NoContent();
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeletePersona(DetallePrestamo detallePrestamo)
    {
        var persona = await detallePrestamoRepository.GetByIdAsync(detallePrestamo);
        if (persona == null)
        {
            return NotFound();
        }
        var saved = await detallePrestamoRepository.DeleteAsync(detallePrestamo);
        if(!saved){
            return BadRequest();
        }
        return NoContent();
    }

}