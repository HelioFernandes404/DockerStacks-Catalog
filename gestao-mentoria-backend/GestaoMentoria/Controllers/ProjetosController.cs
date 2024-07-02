using GestaoMentoria.Data;
using GestaoMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestaoMentoria;


[Route("api/projetos")]
[ApiController]
public class ProjetosController : ControllerBase
{
  private readonly ApplicationContext _context;

  public ProjetosController(ApplicationContext context)
  {
    _context = context;
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<Projeto>> GetProjeto(Guid id)
  {
    //TODO: Implementar a busca de um projeto pelo id
    return Ok();
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Projeto>>> GetProjetos()
  {
    //TODO: Implementar a busca por todos os projetos
    return Ok();
  }

  [HttpPost]
  public async Task<ActionResult<Projeto>> PostProjeto(Projeto projeto)
  {
    if (!ModelState.IsValid)
    {
      return ValidationProblem(ModelState);
    }

    _context.Projetos.Add(projeto);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetProjeto", new { id = projeto.Id }, projeto);
  }

  [HttpPost("adicionar-usuario")]
  public async Task<ActionResult> AdicionarUsuarioAoProjeto()
  {
    //TODO: Implementar a adição de um usuário a um projeto
    return Ok();
  }



}
