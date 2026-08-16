using Faturamento.Service.Models;

namespace Faturamento.Service.Repositories;

public interface INotaFiscalRepository
{
    Task<IReadOnlyList<NotaFiscal>> ListarAsync(CancellationToken cancellationToken = default);

    Task<NotaFiscal?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default);

    Task AdicionarAsync(NotaFiscal nota, CancellationToken cancellationToken = default);

    Task<int> SalvarAsync(CancellationToken cancellationToken = default);
}
