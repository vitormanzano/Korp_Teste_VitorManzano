namespace Estoque.Service.Models;

public class Produto
{
    public int Id { get; set; }
    public required string Codigo { get; set; }
    public required string Descricao { get; set; }
    public int Saldo { get; set; }
}
