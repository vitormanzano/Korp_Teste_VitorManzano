using Microsoft.EntityFrameworkCore;

namespace Faturamento.Service.Data;

public class FaturamentoDbContext(DbContextOptions<FaturamentoDbContext> options) : DbContext(options)
{
}
