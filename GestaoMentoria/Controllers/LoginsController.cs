using GestaoMentoria.Data;
using GestaoMentoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoMentoria;

[Route("api/logins")]
[ApiController]
public class LoginsController : ControllerBase
{
    private readonly ApplicationContext _context;

    public LoginsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpPost("recuperar-senha")]
    public async Task<ActionResult> RecuperarSenha(Usuario loginUser)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginUser.Email);

        if (usuario == null)
        {
            return NotFound(new { message = "Email não cadastrado. Tente Novamente" });
        }

        //var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
        var token = Guid.NewGuid().ToString();
        var link = Url.Action("ResetarSenha", "Account", new { token }, Request.Scheme);
        //await _emailService.SendEmailAsync(usuario.Email, "Recuperar Senha", $"Clique no link para recuperar sua senha: {link}");

        return Ok(new { message = "Seu E-mail de redefinição de senha foi enviado" });
    }

    [HttpPost("redefinir-senha/{id}")]
    public async Task<ActionResult> RedefinirSenha(ResetPassword resetPassword, Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return NotFound(new { message = "Usuário não encontrado." });
        }

        if (resetPassword.senha != resetPassword.confirmacaoSenha)
        {
            return BadRequest(new { message = "As senhas não conferem." });
        }

        usuario.Senha = resetPassword.senha;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Senha redefinida com sucesso." });
    }

    //Codigo temporario para teste
    public class ResetPassword
    {
        public string senha { get; set; }
        public string confirmacaoSenha { get; set; }
    }
    public class RecuperarSenhaRequest
    {
        public string Email { get; set; }
    }

}
