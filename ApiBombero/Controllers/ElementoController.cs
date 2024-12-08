using Entities;
using Microsoft.AspNetCore.Mvc;
using repositories;

namespace controller;

[ApiController]
[Route("api/[controller]")]
public class ElementoController : ControllerBase{


    private readonly IElementoRepository elementoRepository;

    public ElementoController(IElementoRepository elementoRepository)
    {
        this.elementoRepository = elementoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Elemento>>> GetBomberos()
    {
        var elementos = await elementoRepository.GetAllAsync();
        return Ok(elementos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Elemento>> GetBombero(int id)
    {
        var elementos = await elementoRepository.GetByIdAsync(id);
        if (elementos == null)
        {
            return NotFound();
        }
        return Ok(elementos);
    }

    [HttpPost]
    public async Task<ActionResult<Elemento>> PostPersona(Elemento elemento)
    {
        var saved= await elementoRepository.SaveAsync(elemento);
        if(!saved){
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetBombero), new { id = elemento.id }, elemento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBombero(int id, Elemento elemento)
    {
        if (id != elemento.id)
        {
            return BadRequest();
        }
        var saved = await elementoRepository.UpdateAsync(elemento);
        if(!saved){
            return BadRequest();
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var elemento = await elementoRepository.GetByIdAsync(id);
        if (elemento == null)
        {
            return NotFound();
        }
        var saved = await elementoRepository.DeleteAsync(id);
        if(!saved){
            return BadRequest();
        }
        return NoContent();
    }

}