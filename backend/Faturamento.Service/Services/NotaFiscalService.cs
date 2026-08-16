using Faturamento.Service.Clients;
using Faturamento.Service.Dtos;
using Faturamento.Service.Exceptions;
using Faturamento.Service.Models;
using Faturamento.Service.Repositories;

namespace Faturamento.Service.Services;

public class NotaFiscalService(INotaFiscalRepository repository, IEstoqueClient estoqueClient)
    : INotaFiscalService
{
    public async Task<IReadOnlyList<NotaFiscalResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var notas = await repository.ListarAsync(cancellationToken);
        return notas.Select(MapearParaResponse).ToList();
    }

    public async Task<NotaFiscalResponse> CriarAsync(
        CriarNotaFiscalRequest request,
        CancellationToken cancellationToken = default)
    {
        var itens = new List<ItemNotaFiscal>();

        foreach (var item in request.Itens)
        {
            var produto = await estoqueClient.ObterProdutoAsync(item.CodigoProduto, cancellationToken);

            if (produto is null)
            {
                throw new ItemProdutoInexistenteException(item.CodigoProduto);
            }

            itens.Add(new ItemNotaFiscal
            {
                CodigoProduto = produto.Codigo,
                DescricaoProduto = produto.Descricao,
                Quantidade = item.Quantidade,
            });
        }

        var nota = new NotaFiscal
        {
            Status = StatusNota.Aberta,
            Itens = itens,
        };

        await repository.AdicionarAsync(nota, cancellationToken);
        await repository.SalvarAsync(cancellationToken);

        return MapearParaResponse(nota);
    }

    public async Task<NotaFiscalResponse> ImprimirAsync(string numero, CancellationToken cancellationToken = default)
    {
        var id = ParseNumero(numero);
        var nota = await repository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new NotaFiscalNaoEncontradaException(numero);

        if (nota.Status != StatusNota.Aberta)
        {
            throw new NotaFiscalJaFechadaException(numero);
        }

        var itensDebito = nota.Itens.Select(i => new ItemDebitoRequest(i.CodigoProduto, i.Quantidade));

        // Se o Estoque falhar aqui (fora do ar ou recusar o débito), a exceção sobe sem
        // alterar o status da nota — ela permanece Aberta e nenhum saldo foi debitado.
        await estoqueClient.DebitarAsync(itensDebito, cancellationToken);

        nota.Status = StatusNota.Fechada;
        await repository.SalvarAsync(cancellationToken);

        return MapearParaResponse(nota);
    }

    private static int ParseNumero(string numero)
    {
        var parte = numero.StartsWith("NF-", StringComparison.OrdinalIgnoreCase) ? numero[3..] : numero;

        return int.TryParse(parte, out var id) ? id : throw new NotaFiscalNaoEncontradaException(numero);
    }

    private static NotaFiscalResponse MapearParaResponse(NotaFiscal nota) =>
        new(
            $"NF-{nota.Id:D6}",
            nota.Status.ToString(),
            nota.Itens
                .Select(i => new ItemNotaFiscalResponse(i.CodigoProduto, i.DescricaoProduto, i.Quantidade))
                .ToList());
}
