namespace DesafioTecnico.Domain.Exceptions
{
    public class InactiveAccountException : BusinessException
    {
        public InactiveAccountException() : base("Conta corrente inativa", "INACTIVE_ACCOUNT")
        {
        }
    }
}