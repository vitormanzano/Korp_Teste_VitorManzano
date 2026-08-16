namespace Faturamento.Service.Exceptions;

public class EstoqueIndisponivelException : Exception
{
    public EstoqueIndisponivelException(string mensagem)
        : base(mensagem)
    {
    }

    public EstoqueIndisponivelException(string mensagem, Exception innerException)
        : base(mensagem, innerException)
    {
    }
}
