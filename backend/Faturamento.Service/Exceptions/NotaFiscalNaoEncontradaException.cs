namespace Faturamento.Service.Exceptions;

public class NotaFiscalNaoEncontradaException(string identificador)
    : Exception($"Nota fiscal '{identificador}' não encontrada.")
{
    public string Identificador { get; } = identificador;
}
