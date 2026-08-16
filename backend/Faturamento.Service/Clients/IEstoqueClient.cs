using Faturamento.Service.Dtos;

namespace Faturamento.Service.Clients;

public interface IEstoqueClient
{
    Task<ProdutoEstoqueResponse?> ObterProdutoAsync(string codigo, CancellationToken cancellationToken = default);

    Task DebitarAsync(IEnumerable<ItemDebitoRequest> itens, CancellationToken cancellationToken = default);
}
