namespace Faturamento.Service.Models;

public class ItemNotaFiscal
{
    public int Id { get; set; }
    public int NotaFiscalId { get; set; }
    public required string CodigoProduto { get; set; }
    public required string DescricaoProduto { get; set; }
    public int Quantidade { get; set; }
}
