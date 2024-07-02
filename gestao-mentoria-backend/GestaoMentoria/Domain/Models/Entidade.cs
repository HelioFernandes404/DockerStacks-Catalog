namespace GestaoMentoria;

public class Entidade
{
  protected Entidade()
  {
    Id = Guid.NewGuid();
  }

  public Guid Id { get; private set; }
}
