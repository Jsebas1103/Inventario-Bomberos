using Entities;
using Microsoft.AspNetCore.Mvc;
using repositories;

namespace controller{

[ApiController]
[Route ("api/[controller]")]
    public class  CategoriaController:ControllerBase{

        private ICategoriaRepository categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository){
            
            this.categoriaRepository=categoriaRepository;

        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
    {
        var categoria = await categoriaRepository.GetAllAsync();
        return Ok(categoria);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoriaId(int id)
    {
        var categoria = await categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Bombero>> PostCategoria(Categoria categoria)
    {
        await categoriaRepository.SaveAsync(categoria);
        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.id}, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
    {
        if (id != categoria.id)
        {
            return BadRequest();
        }
        await categoriaRepository.UpdateAsync(categoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var categoria = await categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        await categoriaRepository.DeleteAsync(id);
        return NoContent();
    }

    }
}