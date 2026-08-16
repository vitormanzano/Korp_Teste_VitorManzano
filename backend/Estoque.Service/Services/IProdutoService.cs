using Estoque.Service.Dtos;

namespace Estoque.Service.Services;

public interface IProdutoService
{
    Task<IReadOnlyList<ProdutoResponse>> ListarAsync(CancellationToken cancellationToken = default);

    Task<ProdutoResponse> CriarAsync(CriarProdutoRequest request, CancellationToken cancellationToken = default);

    Task DebitarAsync(IEnumerable<ItemDebito> itens, CancellationToken cancellationToken = default);
}
