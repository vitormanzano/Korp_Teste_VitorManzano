using Microsoft.EntityFrameworkCore;

namespace Estoque.Service.Data;

public class EstoqueDbContext(DbContextOptions<EstoqueDbContext> options) : DbContext(options)
{
}
