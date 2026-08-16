using Faturamento.Service.Data;
using Faturamento.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Faturamento.Service.Repositories;

public class NotaFiscalRepository(FaturamentoDbContext context) : INotaFiscalRepository
{
    public async Task<IReadOnlyList<NotaFiscal>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.NotasFiscais
            .Include(n => n.Itens)
            .OrderBy(n => n.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<NotaFiscal?> ObterPorIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.NotasFiscais
            .Include(n => n.Itens)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task AdicionarAsync(NotaFiscal nota, CancellationToken cancellationToken = default)
    {
        await context.NotasFiscais.AddAsync(nota, cancellationToken);
    }

    public Task<int> SalvarAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
