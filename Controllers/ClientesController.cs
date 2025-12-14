using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFamilyHomeApi.Data;
using WebFamilyHomeApi.DTOs;
using WebFamilyHomeApi.Models;

namespace WebFamilyHomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Grupo)
                .Select(c => new ClienteResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    GrupoId = c.GrupoId,
                    GrupoNome = c.Grupo != null ? c.Grupo.Nome : null
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: api/clientes/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Grupo)
                .Where(c => c.Id == id)
                .Select(c => new ClienteResponseDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    GrupoId = c.GrupoId,
                    GrupoNome = c.Grupo != null ? c.Grupo.Nome : null
                })
                .FirstOrDefaultAsync();

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            return Ok(cliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<IActionResult> Post(ClienteCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) ||
                string.IsNullOrWhiteSpace(dto.Senha))
            {
                return BadRequest("Nome e senha são obrigatórios");
            }

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            var cliente = new Cliente
            {
                Nome = dto.Nome,
                Senha = senhaHash,
                GrupoId = dto.GrupoId
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                new { id = cliente.Id }, null);
        }


        // PUT: api/clientes/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            cliente.Nome = dto.Nome;
            cliente.GrupoId = dto.GrupoId;

            if (!string.IsNullOrWhiteSpace(dto.Senha))
            {
                cliente.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE: api/clientes/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                return NotFound("Cliente não encontrado");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
