// Banco de dados simulado
var carrosDisponiveis = new List<Carro>
{
    new Carro(1, "Fiat Uno", "Uno Mille", 15000, "ABC1A23", "SP", new[] {"Dinheiro", "Cartão", "Pix"}),
    new Carro(2, "Volkswagen Gol", "Gol G6", 25000, "DEF4B56", "RJ", new[] {"Dinheiro", "Cartão"}),
    new Carro(3, "Chevrolet Onix", "Onix LT", 40000, "GHI7C89", "MG", new[] {"Pix", "Cartão"})
};

// Lista de transações (carros vendidos)
var transacoes = new List<Carro>();

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/carros", () => carrosDisponiveis)
   .WithName("GetCarrosDisponiveis");

app.MapPost("/vender/{id}", (int id) =>
{
    var carro = carrosDisponiveis.FirstOrDefault(c => c.Id == id);
    if (carro is null)
        return Results.NotFound($"Carro com ID {id} não encontrado ou já vendido.");
    carrosDisponiveis.Remove(carro);
    transacoes.Add(carro);
    return Results.Ok(carro);
})
.WithName("VenderCarro");

app.MapGet("/transacoes", () => transacoes)
   .WithName("GetTransacoes");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Modelo de Carro
record Carro(
    int Id,
    string Modelo,
    string Nome,
    decimal Preco,
    string Placa,
    string Estado,
    string[] MetodosPagamento
);

public partial class Program {}
