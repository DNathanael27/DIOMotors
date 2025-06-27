using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace DIOMotors.Tests;

public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetCarrosDisponiveis_DeveRetornarLista()
    {
        var response = await _client.GetAsync("/carros");
        response.EnsureSuccessStatusCode();
        var carros = await response.Content.ReadFromJsonAsync<List<CarroDto>>();
        Assert.NotNull(carros);
        Assert.True(carros.Count > 0);
    }

    [Fact]
    public async Task VenderCarro_DeveRemoverDaListaDeDisponiveisEAdicionarEmTransacoes()
    {
        // Vender o carro de ID 1
        var response = await _client.PostAsync("/vender/1", null);
        response.EnsureSuccessStatusCode();
        var carroVendido = await response.Content.ReadFromJsonAsync<CarroDto>();
        Assert.NotNull(carroVendido);
        Assert.Equal(1, carroVendido.Id);

        // Verificar se não está mais em /carros
        var carros = await _client.GetFromJsonAsync<List<CarroDto>>("/carros");
        Assert.DoesNotContain(carros, c => c.Id == 1);

        // Verificar se está em /transacoes
        var transacoes = await _client.GetFromJsonAsync<List<CarroDto>>("/transacoes");
        Assert.Contains(transacoes, c => c.Id == 1);
    }

    [Fact]
    public async Task VenderCarroInexistente_DeveRetornarNotFound()
    {
        var response = await _client.PostAsync("/vender/999", null);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

public record CarroDto(int Id, string Modelo, string Nome, decimal Preco, string Placa, string Estado, string[] MetodosPagamento);

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

    }
}
