namespace Estoque.Service.Exceptions;

public class ProdutoCodigoDuplicadoException(string codigo)
    : Exception($"Já existe um produto com o código '{codigo}'.")
{
    public string Codigo { get; } = codigo;
}
