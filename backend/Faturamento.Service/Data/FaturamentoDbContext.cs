using Faturamento.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Faturamento.Service.Data;

public class FaturamentoDbContext(DbContextOptions<FaturamentoDbContext> options) : DbContext(options)
{
    public DbSet<NotaFiscal> NotasFiscais => Set<NotaFiscal>();
    public DbSet<ItemNotaFiscal> ItensNotaFiscal => Set<ItemNotaFiscal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FaturamentoDbContext).Assembly);
    }
}
