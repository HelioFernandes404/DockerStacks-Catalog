# Gestao-Mentoria-backend
O projeto Gestao-Mentoria, uma aplicação ASP.NET Core.

## Link da documentação via postman

- Postman: [Documentação](https://documenter.getpostman.com/view/34900232/2sA3XJk4nB)

## Pré-requisitos

- .NET SDK 8.0 ou superior: [Download .NET SDK](https://dotnet.microsoft.com/download)
- MySQL: [Download MySQL](https://www.mysql.com/downloads/)

## Pacotes NuGet

Este projeto utiliza os seguintes pacotes NuGet:

- `Microsoft.AspNetCore.OpenApi` versão `7.0.18`
- `Microsoft.EntityFrameworkCore.Design` versão `7.0.0`
- `Microsoft.EntityFrameworkCore.Tools` versão `7.0.0`
- `MySql.EntityFrameworkCore` versão `7.0.0`
- `Swashbuckle.AspNetCore` versão `6.5.0`

## Instalação

1. **Clone o repositório:**

    ```bash
    git clone https://github.com/Creath-Tech/gestao-mentoria-backend.git
    cd seu-repositorio
    ```

2. **Restaure os pacotes NuGet:**

    ```bash
    dotnet restore
    ```

3. **Configurar a conexão com o banco de dados:**

    No arquivo `appsettings.json`, configure a string de conexão do MySQL:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=GestaoMentoriaDb;User=root;Password=sua-senha;"
      }
    }
    ```

4. **Aplicar as migrações do Entity Framework Core:**

    ```bash
    dotnet ef database update
    ```

5. **Executar a aplicação:**

    ```bash
    dotnet run
    ```

## Documentação da API

Após iniciar a aplicação, você pode acessar a documentação da API gerada pelo Swagger em:

```
http://localhost:08080/swagger
```

## Estrutura do Projeto

```plaintext
GestaoMentoria/
├── Controllers/
├── Models/
├── Data/
├── Properties/
├── appsettings.json
└── Program.cs
```

- **Controllers**: Contém os controladores da API.
- **Models**: Contém as classes de modelo da aplicação.
- **Data**: Contém a configuração do contexto do Entity Framework Core.
- **Properties**: Contém arquivos de configuração do projeto.

# Executando o Back-end em uma Única Imagem

## Docker Compose

Crie um arquivo `docker-compose.yaml` com o seguinte conteúdo:

```yaml
version: "3.4"

services:
  api:
    image: heliofernandes/backend-gestaomentoria:latest
    container_name: myapi
    depends_on:
      - database
    ports:
      - "5001:80"
      - "5002:443"
    restart: always
    environment:
      DB_HOST: database
      ASPNETCORE_ENVIRONMENT: Development
      ASPNETCORE_URLS: "http://+:80;https://+:443"
      ASPNETCORE_Kestrel__Certificates__Default__Password: "1234"
      ASPNETCORE_Kestrel__Certificates__Default__Path: "/root/.aspnet/https/aspnetapp.pfx"
      ConnectionStrings__DefaultConnection: "Host=dpg-cps4hoij1k6c738herc0-a.virginia-postgres.render.com;Username=gtmuser;Password=zZQpokGPhZS3o8DBjSCf7yAESOOtokIb;Database=gestaodementoriapostgresql;Port=5432"

  database:
    image: postgres:latest
    container_name: mydatabase
    volumes:
      - db-volume:/var/lib/postgresql/data
    environment:
      POSTGRES_USER: gtmuser
      POSTGRES_PASSWORD: zZQpokGPhZS3o8DBjSCf7yAESOOtokIb
      POSTGRES_DB: gestaodementoriapostgresql
    ports:
      - "5432:5432"
    restart: always

volumes:
  db-volume:
```

Após criar o arquivo `docker-compose.yaml`, execute o seguinte comando para iniciar os serviços:

```sh
docker compose up
```

## Contribuição

Se você deseja contribuir para este projeto, sinta-se à vontade para abrir uma issue ou enviar um pull request.
