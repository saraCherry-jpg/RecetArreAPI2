using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Categorias;
//using RecetArreAPI2.DTOs.Categorias;
using RecetArreAPI2.DTOs.Ingredientes;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IngredientesController(ApplicationDbContext context, IMapper mapper) //CREAMOS CONSTRUCTOR
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Ingredientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientesDTO>>> GetIngredientes()
        {
            var ingredientes = await context.Ingredientes
                .OrderByDescending(c => c.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<IngredientesDTO>>(ingredientes));
        }

        // GET: api/Ingredientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientesDTO>> GetIngredientes(int id)
        {
            var ingredientes = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingredientes == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            return Ok(mapper.Map<IngredientesDTO>(ingredientes));
        }


        //// POST: api/ingredientes
        [HttpPost]
        public async Task<ActionResult<IngredientesDTO>> CreateIngredientes(IngredientesCreacionDto ingredientesCreacionDto)
        {
            // Validar que el nombre no esté duplicado
            var existe = await context.Ingredientes
                .AnyAsync(c => c.Nombre.ToLower() == ingredientesCreacionDto.Nombre.ToLower());

            if (existe)
            {
                return BadRequest(new { mensaje = "Ya existe un Ingrediente con ese nombre" });
            }


            var ingredientes = mapper.Map<Ingredientes>(ingredientesCreacionDto);
            ingredientes.CreadoUtc = DateTime.UtcNow;

            context.Ingredientes.Add(ingredientes);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(ingredientes), new { id = ingredientes.Id }, mapper.Map<IngredientesDTO>(ingredientes));
        }


        // PUT: api/ingredientes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredientes(int id, IngredientesModificacionDto IngredientesModificacionDto)
        {
            var ingredientes = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingredientes == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            // Validar que el nombre no esté duplicado (si cambió)
            if (!ingredientes.Nombre.Equals(IngredientesModificacionDto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await context.Ingredientes
                    .AnyAsync(c => c.Nombre.ToLower() == IngredientesModificacionDto.Nombre.ToLower() && c.Id != id);

                if (existe)
                {
                    return BadRequest(new { mensaje = "Ya existe un Ingrediente con ese nombre" });
                }
            }

            mapper.Map(IngredientesModificacionDto, ingredientes);
            context.Ingredientes.Update(ingredientes);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Ingrediente actualizada exitosamente", data = mapper.Map<IngredientesDTO>(ingredientes) });
        }

        // DELETE: api/ingredientes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredientes(int id)
        {
            var ingredientes = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingredientes == null)
            {
                return NotFound(new { mensaje = "Igrediente no encontrado" });
            }

            context.Ingredientes.Remove(ingredientes);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Ingrediente eliminada exitosamente" });
        }





    }
}
