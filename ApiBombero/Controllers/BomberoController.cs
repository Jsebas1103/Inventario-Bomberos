using Entities;
using Microsoft.AspNetCore.Mvc;
using repositories;

namespace controller;

[ApiController]
[Route("api/[controller]")]
public class BomberoController : ControllerBase{


    private readonly IBomberoRepository bomberoRepository;

    public BomberoController(IBomberoRepository bomberoRepository)
    
    {
        this.bomberoRepository = bomberoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bombero>>> GetBomberos()
    {
        var bomberos = await bomberoRepository.GetAllAsync();
        return Ok(bomberos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bombero>> GetBombero(int id)
    {
        var bombero = await bomberoRepository.GetByIdAsync(id);
        if (bombero == null)
        {
            return NotFound();
        }
        return Ok(bombero);
    }

    [HttpPost]
    public async Task<ActionResult<Bombero>> PostPersona(Bombero bombero)
    {
        var saved= await bomberoRepository.SaveAsync(bombero);
        if(!saved){
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetBombero), new { id = bombero.id }, bombero);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBombero(int id, Bombero bombero)
    {
        if (id != bombero.id)
        {
            return BadRequest();
        }
        var saved = await bomberoRepository.UpdateAsync(bombero);
        if(!saved){
            return BadRequest();
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var persona = await bomberoRepository.GetByIdAsync(id);
        if (persona == null)
        {
            return NotFound();
        }
        var saved = await bomberoRepository.DeleteAsync(id);
        if(!saved){
            return BadRequest();
        }
        return NoContent();
    }

}