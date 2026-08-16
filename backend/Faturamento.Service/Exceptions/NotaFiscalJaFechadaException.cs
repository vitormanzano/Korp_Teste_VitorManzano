namespace Faturamento.Service.Exceptions;

public class NotaFiscalJaFechadaException(string numero)
    : Exception($"Nota fiscal '{numero}' já está Fechada.")
{
    public string Numero { get; } = numero;
}
