using System.ComponentModel.DataAnnotations;

namespace GestaoMentoria.Models
{
    public class Usuario : Entidade
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Senha { get; set; }

        public Guid PerfilId { get; set; }
        public Perfil? Perfil { get; set; }

        public List<Projeto>? Projetos { get; set; }
        public List<ProjetoUsuario>? ProjetosUsuarios { get; set; }
    }

    public class Perfil : Entidade
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

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }

    public class Projeto : Entidade
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public ProjetoStatus Status { get; set; }

        public List<Usuario>? Usuarios { get; set; } = new List<Usuario>();
    }


    public class ProjetoUsuario : Entidade
    {
        public Guid Id { get; set; }

        public Guid ProjetoId { get; set; }
        public Projeto Projeto { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Role Role { get; set; }
    }

}
