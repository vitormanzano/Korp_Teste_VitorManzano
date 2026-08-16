namespace Faturamento.Service.Exceptions;

public class ItemProdutoInexistenteException(string codigo)
    : Exception($"Produto '{codigo}' não existe no Estoque.")
{
    public string Codigo { get; } = codigo;
}
