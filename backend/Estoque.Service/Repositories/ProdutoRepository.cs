using Estoque.Service.Data;
using Estoque.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Service.Repositories;

public class ProdutoRepository(EstoqueDbContext context) : IProdutoRepository
{
    public async Task<IReadOnlyList<Produto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await context.Produtos
            .OrderBy(p => p.Codigo)
            .ToListAsync(cancellationToken);
    }

    public async Task<Produto?> ObterPorCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        return await context.Produtos
            .FirstOrDefaultAsync(p => p.Codigo == codigo, cancellationToken);
    }

    public async Task<IReadOnlyList<Produto>> ObterPorCodigosAsync(
        IEnumerable<string> codigos,
        CancellationToken cancellationToken = default)
    {
        return await context.Produtos
            .Where(p => codigos.Contains(p.Codigo))
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExisteComCodigoAsync(string codigo, CancellationToken cancellationToken = default)
    {
        return await context.Produtos
            .AnyAsync(p => p.Codigo == codigo, cancellationToken);
    }

    public async Task AdicionarAsync(Produto produto, CancellationToken cancellationToken = default)
    {
        await context.Produtos.AddAsync(produto, cancellationToken);
    }

    public Task<int> SalvarAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
