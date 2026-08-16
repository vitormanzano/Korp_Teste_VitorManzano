namespace Faturamento.Service.Dtos;

public record NotaFiscalResponse(string Numero, string Status, List<ItemNotaFiscalResponse> Itens);
