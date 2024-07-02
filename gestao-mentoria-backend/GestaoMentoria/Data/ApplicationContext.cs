using GestaoMentoria.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace GestaoMentoria.Data
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<ProjetoUsuario> ProjetosUsuarios { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            var usuarioId = Guid.Parse("81fda13f-5137-449e-8e26-30b654c362c5");
            var perfilId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var projeto1Id = Guid.NewGuid();
            var projeto2Id = Guid.NewGuid();
            var projeto3Id = Guid.NewGuid();
            var projetoUsuario1Id = Guid.NewGuid();
            var projetoUsuario2Id = Guid.NewGuid();
            var projetoUsuario3Id = Guid.NewGuid();

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = usuarioId,
                    Email = "admin@gmail.com",
                    Senha = "admin",
                    PerfilId = perfilId,
                }
            );

            modelBuilder.Entity<Perfil>().HasData(
                new Perfil
                {
                    Id = perfilId,
                    Nome = "Lucas",
                    FotoUrl = "https://www.google.com",
                    Classificacao = "Prata",
                    Funcao = "Mentorado",
                    Trilha = "Desenvolvimento",
                    LinkedIn = "https://www.linkedin.com",
                    Email = "sistema@gmail.com",
                    Telefone = "11999999999",
                    SobreMim = "Sobre mim",
                    UsuarioId = usuarioId,
                }
            );

            modelBuilder.Entity<Projeto>().HasData(
                new Projeto { Id = projeto1Id, Nome = "Gestão de mentoria", Status = ProjetoStatus.Ativo },
                new Projeto { Id = projeto2Id, Nome = "Lab de IA", Status = ProjetoStatus.Ativo },
                new Projeto { Id = projeto3Id, Nome = "Liga Independente", Status = ProjetoStatus.Ativo }
            );

            modelBuilder.Entity<ProjetoUsuario>().HasData(
                new ProjetoUsuario
                {
                    Id = projetoUsuario1Id,
                    ProjetoId = projeto1Id,
                    UsuarioId = usuarioId,
                    Role = Role.Desenvolvedor,
                },
                new ProjetoUsuario
                {
                    Id = projetoUsuario2Id,
                    ProjetoId = projeto2Id,
                    UsuarioId = usuarioId,
                    Role = Role.Desenvolvedor,

                },
                new ProjetoUsuario
                {
                    Id = projetoUsuario3Id,
                    ProjetoId = projeto3Id,
                    UsuarioId = usuarioId,
                    Role = Role.Analista,
                }
            );
        }

    }
}

