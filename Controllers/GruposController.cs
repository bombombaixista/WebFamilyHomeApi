using Microsoft.AspNetCore.Mvc;
using WebFamilyHomeApi.Models;

namespace WebFamilyHomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GruposController : ControllerBase
    {
        private static List<Grupo> grupos = new List<Grupo>();
        private static int proximoId = 1;

        // GET: api/grupos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(grupos);
        }

        // GET: api/grupos/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var grupo = grupos.FirstOrDefault(g => g.Id == id);

            if (grupo == null)
                return NotFound("Grupo não encontrado");

            return Ok(grupo);
        }

        // POST: api/grupos
        [HttpPost]
        public IActionResult Post([FromBody] Grupo grupo)
        {
            if (grupo == null || string.IsNullOrWhiteSpace(grupo.Nome))
                return BadRequest("Nome do grupo é obrigatório");

            grupo.Id = proximoId++;
            grupos.Add(grupo);

            return CreatedAtAction(nameof(GetById), new { id = grupo.Id }, grupo);
        }

        // PUT: api/grupos/1
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Grupo grupoAtualizado)
        {
            if (grupoAtualizado == null || string.IsNullOrWhiteSpace(grupoAtualizado.Nome))
                return BadRequest("Nome do grupo é obrigatório");

            var grupo = grupos.FirstOrDefault(g => g.Id == id);

            if (grupo == null)
                return NotFound("Grupo não encontrado");

            grupo.Nome = grupoAtualizado.Nome;

            return Ok(grupo);
        }

        // DELETE: api/grupos/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var grupo = grupos.FirstOrDefault(g => g.Id == id);

            if (grupo == null)
                return NotFound("Grupo não encontrado");

            grupos.Remove(grupo);

            return NoContent(); // 204
        }
    }
}
