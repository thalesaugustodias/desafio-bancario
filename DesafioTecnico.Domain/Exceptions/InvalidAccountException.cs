namespace DesafioTecnico.Domain.Exceptions
{
    public class InvalidAccountException : BusinessException
    {
        public InvalidAccountException() : base("Conta corrente não encontrada ou inválida", "INVALID_ACCOUNT")
        {
        }
    }
}