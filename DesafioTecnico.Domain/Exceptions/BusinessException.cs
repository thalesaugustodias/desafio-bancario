namespace DesafioTecnico.Domain.Exceptions
{
    public abstract class BusinessException(string message, string errorType) : Exception(message)
    {
        public string ErrorType { get; private set; } = errorType;
    }
}
