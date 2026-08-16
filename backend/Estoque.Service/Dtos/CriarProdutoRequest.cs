namespace Estoque.Service.Dtos;

public record CriarProdutoRequest(string Codigo, string Descricao, int SaldoInicial);
