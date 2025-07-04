using DesafioTecnico.Domain.Enums;

namespace DesafioTecnico.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; private set; }
        public int Numero { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }
        public List<Movimento> Movimentos { get; private set; }

        protected ContaCorrente() => Movimentos = [];

        public ContaCorrente(string idContaCorrente, int numero, string nome, bool ativo)
        {
            IdContaCorrente = idContaCorrente;
            Numero = numero;
            Nome = nome;
            Ativo = ativo;
            Movimentos = [];
        }

        public decimal CalcularSaldo()
        {
            decimal saldo = 0;
            foreach (var movimento in Movimentos)
            {
                if (movimento.TipoMovimento == TipoMovimento.Credito)
                    saldo += movimento.Valor;
                else if (movimento.TipoMovimento == TipoMovimento.Debito)
                    saldo -= movimento.Valor;
            }
            return saldo;
        }

        public void AdicionarMovimento(Movimento movimento)
        {
            Movimentos.Add(movimento);
        }
    }
}