using GestaoMentoria.Data;
using GestaoMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace GestaoMentoria.Controllers
{

    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {

        private readonly ApplicationContext _repositorio;

        public UsuariosController(ApplicationContext context)
        {
            _repositorio = context;
        }

        // GET: api/usuarios/1/completo
        [HttpGet("{id:guid}/completo")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioCompleto(Guid id)
        {

            var usuarioCompleto = await _repositorio.Usuarios
                .Where(u => u.Id == id)
                .Include(u => u.Perfil)
                .Include(u => u.ProjetosUsuarios!)
                .ThenInclude(pu => pu.Projeto)
                .Select(u => new UsuarioCompletoDto // Projeta para um DTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Perfil = new PerfilMinDto // Supondo que você tenha um DTO para o perfil
                    {
                        Id = u.Perfil.Id,
                        Nome = u.Perfil.Nome,
                        FotoUrl = u.Perfil.FotoUrl,
                        Funcao = u.Perfil.Funcao,
                        Classificacao = u.Perfil.Classificacao,
                        Trilha = u.Perfil.Trilha,
                        LinkedIn = u.Perfil.LinkedIn,
                        Email = u.Perfil.Email,
                        Telefone = u.Perfil.Telefone,
                        SobreMim = u.Perfil.SobreMim
                    },
                    Projetos = u.ProjetosUsuarios!.Select(pu => new ProjetoMinDto
                    {
                        Id = pu.Projeto.Id,
                        Nome = pu.Projeto.Nome,
                        Role = pu.Role.ToString(),
                        Status = pu.Projeto.Status.ToString()
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return Ok(usuarioCompleto);
        }

        // GET: api/usuarios/1
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Usuario>> GetUsuario(Guid id)
        {


            if (_repositorio.Usuarios is null)
            {
                return NotFound();
            }

            var result = await _repositorio.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioMinDto
                {
                    Id = u.Id,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {

            if (_repositorio.Usuarios == null)
            {
                return NotFound();
            }

            //TODO: DTO para retornar com melhor formatação
            var usuarios = await _repositorio.Usuarios
                .Include(u => u.Perfil)
                .Include(u => u.Projetos)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    Email = u.Email,
                    Senha = u.Senha,
                    Perfil = u.Perfil,
                    Projetos = u.Projetos.ToList()

                })
                .ToListAsync();

            if (usuarios is null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // POST: api/usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioMinDtoPerfil>> PostUsuario(CriarUsuarioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }


            var perfilDefault = new Perfil
            {
                Nome = "Nome Default",
                FotoUrl = "FotoUrl Default",
                Funcao = "Funcao Default",
                Classificacao = "Classificacao Default",
                Trilha = "Trilha Default",
                LinkedIn = "LinkedIn Default",
                Email = "Email Default",
                Telefone = "Telefone Default",
                SobreMim = "SobreMim Default"

            };

            _repositorio.Perfis.Add(perfilDefault);
            await _repositorio.SaveChangesAsync();




            var usuario = new Usuario
            {
                Email = dto.Email,
                Senha = dto.Senha,
                PerfilId = perfilDefault.Id
            };


            var result = _repositorio.Add(usuario);
            await _repositorio.SaveChangesAsync();

            //retornar o id e email do usuario criado
            return Ok(result.Entity.Id);
        }

        // DELETE: api/ususario/1
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUsuario(Guid id)
        {
            var usuario = await _repositorio.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _repositorio.Usuarios.Remove(usuario);
            await _repositorio.SaveChangesAsync();

            return NoContent();
        }
    }



    //DTO temporario

    public class CriarUsuarioDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Senha { get; set; }
    }
    public class UsuarioMinDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }

    public class UsuarioMinDtoPerfil
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PerfilMinDto? Perfil { get; set; }
    }

    public class PerfilMinDto
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

    public class ProjetoMinDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }

    public class UsuarioCompletoDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public PerfilMinDto Perfil { get; set; }
        public List<ProjetoMinDto> Projetos { get; set; }
    }
}
