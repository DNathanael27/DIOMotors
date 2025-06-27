# DIOMotors

API simples para gerenciamento de vendas de carros, desenvolvida em ASP.NET Core com endpoints REST e documentação via Swagger.

## Funcionalidades
- Listar carros disponíveis para venda
- Realizar a venda de um carro (transação)
- Listar transações (carros vendidos)
- Testes automatizados com xUnit

## Endpoints

### Listar carros disponíveis
`GET /carros`

### Vender um carro
`POST /vender/{id}`
- Exemplo: `POST /vender/1`
- Remove o carro da lista de disponíveis e adiciona à lista de vendidos

### Listar transações
`GET /transacoes`

## Como rodar o projeto
1. Clone o repositório:
   ```bash
   git clone https://github.com/SEU_USUARIO/DIOMotors.git
   ```
2. Acesse a pasta do projeto:
   ```bash
   cd DIOMotors
   ```
3. Execute a aplicação:
   ```bash
   dotnet run --project .\DIOMotors\DIOMotors.csproj
   ```
4. Acesse o Swagger UI para testar a API:
   - `https://localhost:5001/swagger` (ou porta exibida no terminal)

## Testes automatizados
Para rodar os testes:
```bash
dotnet test DIOMotors.Tests/DIOMotors.Tests.csproj
```

## Tecnologias
- .NET 9
- ASP.NET Core Minimal API
- Swagger (Swashbuckle)
- xUnit

---
Projeto criado para fins educacionais, simula um banco de dados em memória.
