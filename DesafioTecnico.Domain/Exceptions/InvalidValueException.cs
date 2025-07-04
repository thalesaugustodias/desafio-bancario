namespace DesafioTecnico.Domain.Exceptions
{
    public class InvalidValueException : BusinessException
    {
        public InvalidValueException() : base("Valor deve ser positivo", "INVALID_VALUE")
        {
        }
    }
}
