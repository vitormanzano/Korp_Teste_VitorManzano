using Estoque.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Service.Data;

public class EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstoqueDbContext).Assembly);
    }
}
