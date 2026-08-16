namespace Estoque.Service.Exceptions;

public class ProdutoNaoEncontradoException(string codigo)
    : Exception($"Produto '{codigo}' não encontrado.")
{
    public string Codigo { get; } = codigo;
}
