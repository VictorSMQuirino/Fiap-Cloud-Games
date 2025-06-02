# FIAP Cloud Games

## Descrição

A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gerenciamento de servidores para partidas online. Nesta primeira fase, o projeto foca no desenvolvimento de um serviço de cadastro de usuários e uma biblioteca de jogos adquiridos, que servirá como base para as fases futuras do projeto. Este desafio foi estruturado para aplicar os conhecimentos adquiridos nas disciplinas do curso de pós-graduação em arquitetura de sistemas .NET da FIAP.

## Objetivos

Os principais objetivos deste projeto são:

- Desenvolver uma API REST em .NET 8 para gerenciar usuários e seus jogos adquiridos.
- Garantir a persistência de dados utilizando algum banco de dados relacional.
- Aplicar práticas de qualidade de software e boas práticas de desenvolvimento.
- Preparar a base para funcionalidades futuras, como matchmaking e gerenciamento de servidores.

## Tecnologias Utilizadas

| Tecnologia            | Descrição                                      |
|-----------------------|-----------------------------------------------|
| **Backend**           | .NET 8                                       |
| **Banco de Dados**    | PostgreSQL                                   |
| **ORM**               | Entity Framework Core                        |
| **Documentação da API** | Swagger (OpenAPI)                           |
| **Testes**            | xUnit  |

## Instruções de Configuração

### Pré-requisitos

- **.NET 8 SDK**: Disponível em [Download .NET](https://dotnet.microsoft.com/download/dotnet/8.0).
- **PostgreSQL**: Servidor de banco de dados instalado e em execução.

### Instalação

1. Clone o repositório do projeto:
   ```bash
   git clone https://github.com/VictorSMQuirino/Fiap-Cloud-Games.git
   ```
2. Navegue até o diretório do projeto:
   ```bash
   cd Fiap-Cloud-Games
   ```
3. Restaure os pacotes NuGet:
   ```bash
   dotnet restore
   ```

### Configuração do Banco de Dados

1. Certifique-se de que o PostgreSQL está instalado e em execução.
2. Crie um novo banco de dados, por exemplo, `fcg_db`.
3. Atualize a string de conexão e outras informações nos arquivos `appsettings.json` ou `appsettings.Development.json` do projeto `FIAP_CloudGames.API`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=fcg_db;Username=seu_usuario;Password=sua_senha"
     },
     "Jwt": {
       "Key": "sua_chave_secreta",
       "Issuer": "seu_emissor",
       "Audience": "sua_audiencia",
       "ExpireMinutes": 30
     },
     "AdminUser": {
       "Id": "qualquer_id_guid",
       "Password": "sua_senha_admin"
     }
   }
   ``` 
   
4. Navegue até o diretório do projeto `FIAP_CloudGames.Infrastructure`:
   ```bash
   cd src\FIAP_CloudGames.Infrastructure
   ```

5. Aplique as migrações do Entity Framework Core:
   ```bash
   dotnet ef database update -s ..\FIAP_CloudGames.API\FIAP_CloudGames.API.csproj
   ```

### Executando a Aplicação

1. Compile o projeto:
   ```bash
   dotnet build
   ```
2. Execute a API:
   ```bash
   dotnet run --project .\src\FIAP_CloudGames.API\FIAP_CloudGames.API.csproj
   ```
   A API estará disponível em `https://localhost:7049` (ou na porta configurada no arquivo `launchSettings.json`).

## Uso

### Endpoints da API

A API fornece endpoints para gerenciamento de usuários e biblioteca de jogos. Abaixo estão os principais endpoints, com métodos HTTP, caminhos, corpos de requisição e respostas esperadas:

#### Gerenciamento de Usuários

| Método | Endpoint              | Descrição                    | Corpo da Requisição                                             | Resposta                       |
|--------|-----------------------|------------------------------|-----------------------------------------------------------------|--------------------------------|
| POST   | `/api/v1/Auth/sugnup` | Registrar um novo usuário    | `{ "name": "string", "email": "string", "password": "string" }` | 201 Created                    |
| GET    | `/api/v1/users/{id}`  | Obter detalhes de um usuário | -                                                               | 200 OK com detalhes do usuário |
| GET    | `/api/v1/users/`      | Obter lista de usuários      | -                                                               | 200 OK com lista de usuários   |

#### Catálogo de Jogos

| Método | Endpoint                  | Descrição          | Corpo da Requisição                                              | Resposta                   |
|--------|--------------------------|--------------------|------------------------------------------------------------------|----------------------------|
| GET    | `/api/v1/games` | Listar jogos       | -                                                                | 200 OK com lista de jogos  |
| POST   | `/api/v1/games` | Registrar um jogo. | `{ "title": "string", "price": 0, "releaseDate": "2025-06-02" }` | 201 Created com id do jogo |
| DELETE | `/api/v1/games` | Excluir um jogo    | -                                                                | 204 No Content             |

#### Gerenciamento de promoções
| Método | Endpoint                    | Descrição                            | Corpo da Requisição                                             | Resposta                       |
|--------|-----------------------------|--------------------------------------|-----------------------------------------------------------------|--------------------------------|
| GET    | `/api/v1/promotions`        | Listar promoções de jogos            | -                                                               | 200 OK com lista de promoçõe   |
| POST   | `/api/v1/promotions`        | Registrar uma promoção para um jogo. | `{ "gameId": "guid", "discountPercentage": 0, "deadline": "2025-06-02" }` | 201 Created com id da promoção |
| GET    | `/api/v1/promotions/active` | Ativar uma promoção existente        | -                                                               | 204 No Content                 |

### Documentação da API

A documentação interativa da API está disponível via Swagger UI em `https://localhost:7049/swagger` (ou na porta configurada). Use o Swagger para testar os endpoints diretamente no navegador.

### Exemplo de Uso com cURL

- **Registrar um usuário**:
  ```bash
  curl -X POST "https://localhost:7049/api/v1/Auth/signup" -H "Content-Type: application/json" -d '{"name":"joao","email":"joao@example.com","password":"Senha@123"}'
  ```

- **Listar jogos **:
  ```bash
  curl -X GET "https://localhost:7049/api/v1/games"
  ```

## Testes

O projeto inclui testes unitários para a lógica de negócios, localizados no diretório `tests/FIAP_CloudGames.Tests`.

### Executando os Testes

1. Navegue até o diretório de testes:
   ```bash
   cd tests/FIAP_CloudGames.Tests
   ```
2. Execute os testes:
   ```bash
   dotnet test
   ```

## Licença

Este projeto está licenciado sob a [MIT License](https://opensource.org/licenses/MIT).

## Autores

- Víctor Quirino

## Agradecimentos

- À FIAP, pela oportunidade de aprendizado e desenvolvimento do projeto.
- À comunidade .NET, por fornecer recursos e documentação extensos.