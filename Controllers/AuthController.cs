using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFamilyHomeApi.Data;
using WebFamilyHomeApi.DTOs;
using BCrypt.Net;

namespace WebFamilyHomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) ||
                string.IsNullOrWhiteSpace(dto.Senha))
            {
                return BadRequest("Nome e senha são obrigatórios");
            }

            var cliente = await _context.Clientes
                .Include(c => c.Grupo)
                .FirstOrDefaultAsync(c => c.Nome == dto.Nome);

            if (cliente == null)
                return Unauthorized("Usuário ou senha inválidos");

            var senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, cliente.Senha);

            if (!senhaValida)
                return Unauthorized("Usuário ou senha inválidos");

            var response = new LoginResponseDto
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                GrupoId = cliente.GrupoId,
                GrupoNome = cliente.Grupo?.Nome
            };

            return Ok(response);
        }
    }
}
