using Estoque.Service.Dtos;
using Estoque.Service.Exceptions;
using Estoque.Service.Models;
using Estoque.Service.Repositories;

namespace Estoque.Service.Services;

public class ProdutoService(IProdutoRepository repository) : IProdutoService
{
    public async Task<IReadOnlyList<ProdutoResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var produtos = await repository.ListarAsync(cancellationToken);

        return produtos
            .Select(p => new ProdutoResponse(p.Codigo, p.Descricao, p.Saldo))
            .ToList();
    }

    public async Task<ProdutoResponse?> ObterPorCodigoAsync(
        string codigo,
        CancellationToken cancellationToken = default)
    {
        var produto = await repository.ObterPorCodigoAsync(codigo, cancellationToken);

        return produto is null ? null : new ProdutoResponse(produto.Codigo, produto.Descricao, produto.Saldo);
    }

    public async Task<ProdutoResponse> CriarAsync(
        CriarProdutoRequest request,
        CancellationToken cancellationToken = default)
    {
        if (await repository.ExisteComCodigoAsync(request.Codigo, cancellationToken))
        {
            throw new ProdutoCodigoDuplicadoException(request.Codigo);
        }

        var produto = new Produto
        {
            Codigo = request.Codigo,
            Descricao = request.Descricao,
            Saldo = request.SaldoInicial,
        };

        await repository.AdicionarAsync(produto, cancellationToken);
        await repository.SalvarAsync(cancellationToken);

        return new ProdutoResponse(produto.Codigo, produto.Descricao, produto.Saldo);
    }

    public async Task DebitarAsync(IEnumerable<ItemDebito> itens, CancellationToken cancellationToken = default)
    {
        var itensList = itens.ToList();
        var codigos = itensList.Select(i => i.Codigo).ToList();

        var produtos = await repository.ObterPorCodigosAsync(codigos, cancellationToken);
        var produtosPorCodigo = produtos.ToDictionary(p => p.Codigo);

        foreach (var item in itensList)
        {
            if (!produtosPorCodigo.TryGetValue(item.Codigo, out var produto))
            {
                throw new ProdutoNaoEncontradoException(item.Codigo);
            }

            if (produto.Saldo < item.Quantidade)
            {
                throw new SaldoInsuficienteException(item.Codigo, produto.Saldo, item.Quantidade);
            }
        }

        foreach (var item in itensList)
        {
            produtosPorCodigo[item.Codigo].Saldo -= item.Quantidade;
        }

        await repository.SalvarAsync(cancellationToken);
    }
}
