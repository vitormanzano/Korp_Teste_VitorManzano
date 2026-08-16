namespace Faturamento.Service.Models;

public class NotaFiscal
{
    public int Id { get; set; }
    public StatusNota Status { get; set; } = StatusNota.Aberta;
    public List<ItemNotaFiscal> Itens { get; set; } = [];
}
