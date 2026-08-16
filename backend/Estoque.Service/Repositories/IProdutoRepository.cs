using Estoque.Service.Models;

namespace Estoque.Service.Repositories;

public interface IProdutoRepository
{
    Task<IReadOnlyList<Produto>> ListarAsync(CancellationToken cancellationToken = default);

    Task<Produto?> ObterPorCodigoAsync(string codigo, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Produto>> ObterPorCodigosAsync(
        IEnumerable<string> codigos,
        CancellationToken cancellationToken = default);

    Task<bool> ExisteComCodigoAsync(string codigo, CancellationToken cancellationToken = default);

    Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default);

    Task<int> SalvarAsync(CancellationToken cancellationToken = default);
}
