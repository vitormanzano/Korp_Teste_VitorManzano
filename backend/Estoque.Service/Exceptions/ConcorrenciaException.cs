namespace Estoque.Service.Exceptions;

public class ConcorrenciaException()
    : Exception("Conflito de concorrência ao atualizar o saldo. Tente novamente.");
