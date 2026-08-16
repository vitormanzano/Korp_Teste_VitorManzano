namespace Estoque.Service.Exceptions;

public class SaldoInsuficienteException(string codigo, int saldoDisponivel, int quantidadeSolicitada)
    : Exception(
        $"Produto '{codigo}' não tem saldo suficiente: disponível {saldoDisponivel}, solicitado {quantidadeSolicitada}.")
{
    public string Codigo { get; } = codigo;
    public int SaldoDisponivel { get; } = saldoDisponivel;
    public int QuantidadeSolicitada { get; } = quantidadeSolicitada;
}
