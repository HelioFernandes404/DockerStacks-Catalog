using GestaoMentoria.Data;
using GestaoMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GestaoMentoria.Controllers
{
    [Route("api/perfis")]
    [ApiController]
    public class PerfisController : ControllerBase
    {

        private readonly ApplicationContext _context;

        public PerfisController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Perfil>> GetPerfil(Guid id)
        {

            if (_context.Perfis is null)
            {
                return NotFound();
            }

            var result = await _context.Perfis.FindAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            var perfilDto = new PerfilDto
            {
                Id = result.Id,
                Nome = result.Nome,
                FotoUrl = result.FotoUrl,
                Funcao = result.Funcao,
                Classificacao = result.Classificacao,
                Trilha = result.Trilha,
                LinkedIn = result.LinkedIn,
                Email = result.Email,
                Telefone = result.Telefone,
                SobreMim = result.SobreMim,

            };

            return Ok(perfilDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfis()
        {

            if (_context.Perfis == null)
            {
                return NotFound();
            }

            var result = await _context.Perfis.ToListAsync();

            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutPerfil(Guid id, PerfilDto dto)
        {
            if (id != dto.Id) return BadRequest();

            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var entity = await _context.Perfis.FindAsync(id);

            if (entity == null) return NotFound();

            entity.Nome = dto.Nome;
            entity.FotoUrl = dto.FotoUrl;
            entity.Classificacao = dto.Classificacao;
            entity.Funcao = dto.Funcao;
            entity.Trilha = dto.Trilha;
            entity.LinkedIn = dto.LinkedIn;
            entity.Email = dto.Email;
            entity.Telefone = dto.Telefone;
            entity.SobreMim = dto.SobreMim;

            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Verifica se a entidade ainda existe no banco de dados
                if (!PerfilExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool PerfilExists(Guid id)
        {
            return (_context.Perfis?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

    //DTOS temporarios
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilDto? Perfil { get; set; }
        public List<ProjetoDto>? Projetos { get; set; } = new List<ProjetoDto>();
    }

    public class PerfilDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string FotoUrl { get; set; }
        public string Funcao { get; set; }
        public string Classificacao { get; set; }
        public string Trilha { get; set; }
        public string LinkedIn { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string SobreMim { get; set; }
    }

    public class ProjetoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public List<UsuarioDto>? Usuarios { get; set; } = new List<UsuarioDto>();
    }



}





