using System.Net;
using System.Net.Http.Json;
using Faturamento.Service.Dtos;
using Faturamento.Service.Exceptions;

namespace Faturamento.Service.Clients;

public class EstoqueClient(HttpClient http) : IEstoqueClient
{
    public async Task<ProdutoEstoqueResponse?> ObterProdutoAsync(
        string codigo,
        CancellationToken cancellationToken = default)
    {
        var response = await EnviarAsync(
            () => http.GetAsync($"produtos/{codigo}", cancellationToken));

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProdutoEstoqueResponse>(cancellationToken);
    }

    public async Task DebitarAsync(
        IEnumerable<ItemDebitoRequest> itens,
        CancellationToken cancellationToken = default)
    {
        var response = await EnviarAsync(
            () => http.PostAsJsonAsync("produtos/debitar", itens, cancellationToken));

        if (response.IsSuccessStatusCode)
        {
            return;
        }

        string mensagem;
        try
        {
            var erro = await response.Content.ReadFromJsonAsync<ErroEstoqueResponse>(cancellationToken);
            mensagem = erro?.Mensagem ?? "Estoque recusou o débito de saldo.";
        }
        catch
        {
            mensagem = "Estoque recusou o débito de saldo.";
        }

        throw new EstoqueIndisponivelException(mensagem);
    }

    private static async Task<HttpResponseMessage> EnviarAsync(Func<Task<HttpResponseMessage>> chamada)
    {
        try
        {
            return await chamada();
        }
        catch (HttpRequestException ex)
        {
            throw new EstoqueIndisponivelException(
                "Serviço de estoque indisponível no momento. Tente novamente em instantes.", ex);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            throw new EstoqueIndisponivelException(
                "Serviço de estoque não respondeu a tempo. Tente novamente em instantes.", ex);
        }
    }
}
