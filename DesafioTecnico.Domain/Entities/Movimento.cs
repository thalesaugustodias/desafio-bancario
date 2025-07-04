using DesafioTecnico.Domain.Enums;

namespace DesafioTecnico.Domain.Entities
{
    public class Movimento
    {
        public string IdMovimento { get; private set; }
        public string IdContaCorrente { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public TipoMovimento TipoMovimento { get; private set; }
        public decimal Valor { get; private set; }

        protected Movimento() { }

        public Movimento(string idMovimento, string idContaCorrente, DateTime dataMovimento, TipoMovimento tipoMovimento, decimal valor)
        {
            IdMovimento = idMovimento;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public static Movimento Criar(string idContaCorrente, TipoMovimento tipoMovimento, decimal valor)
        {
            return new Movimento(
                Guid.NewGuid().ToString(),
                idContaCorrente,
                DateTime.Now,
                tipoMovimento,
                valor
            );
        }
    }
}
