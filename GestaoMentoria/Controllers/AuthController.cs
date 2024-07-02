using GestaoMentoria.Data;
using GestaoMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoMentoria.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public AuthController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<FazerLoginDto>> AuthUsuario(FazerLoginDto FazerLoginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == FazerLoginDto.Email);

            if (usuario == null)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            if (usuario.Senha != FazerLoginDto.Senha)
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            var authDto = new AuthDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
            };

            return Ok(authDto);
        }


    }

    public class AuthDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class FazerLoginDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
