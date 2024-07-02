using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoMentoria.Models;

namespace GestaoMentoria;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
  public void Configure(EntityTypeBuilder<Usuario> builder)
  {
    builder.HasOne(u => u.Perfil)
    .WithOne(p => p.Usuario)
    .HasForeignKey<Perfil>(p => p.UsuarioId);

    builder.Navigation(u => u.Perfil).AutoInclude();

    
    builder.Navigation(u => u.Projetos).AutoInclude();
    builder.Navigation(u => u.ProjetosUsuarios).AutoInclude();
  }
}


