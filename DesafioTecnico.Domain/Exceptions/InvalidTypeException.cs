namespace DesafioTecnico.Domain.Exceptions
{
    public class InvalidTypeException : BusinessException
    {
        public InvalidTypeException() : base("Tipo de movimento deve ser 'C' (Crédito) ou 'D' (Débito)", "INVALID_TYPE")
        {
        }
    }
}