using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoMentoria.Models;

namespace GestaoMentoria;

public class ProjetosConfiguration : IEntityTypeConfiguration<Projeto>
{
  public void Configure(EntityTypeBuilder<Projeto> builder)
  {
    //TODO: estudar sobre configuração de entidades

  }
}


