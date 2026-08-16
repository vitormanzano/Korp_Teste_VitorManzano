using Faturamento.Service.Dtos;

namespace Faturamento.Service.Services;

public interface INotaFiscalService
{
    Task<IReadOnlyList<NotaFiscalResponse>> ListarAsync(CancellationToken cancellationToken = default);

    Task<NotaFiscalResponse> CriarAsync(
        CriarNotaFiscalRequest request,
        CancellationToken cancellationToken = default);

    Task<NotaFiscalResponse> ImprimirAsync(string numero, CancellationToken cancellationToken = default);
}
