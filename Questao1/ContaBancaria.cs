namespace Questao1
{
    public class ContaBancaria
    {
        private readonly int _numero;
        private string _titular;
        private decimal _saldo;
        private const decimal TAXA_SAQUE = 3.50m;

        public ContaBancaria(int numero, string titular)
        {
            _numero = numero;
            _titular = titular;
            _saldo = 0.00m;
        }

        public ContaBancaria(int numero, string titular, decimal depositoInicial)
        {
            _numero = numero;
            _titular = titular;
            _saldo = depositoInicial;
        }

        public int Numero => _numero;

        public string Titular
        {
            get => _titular;
            set => _titular = value;
        }

        public decimal Saldo => _saldo;

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do depósito deve ser positivo");

            _saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("Valor do saque deve ser positivo");

            _saldo -= (valor + TAXA_SAQUE);
        }

        public override string ToString()
        {
            return $"Conta {_numero}, Titular: {_titular}, Saldo: $ {_saldo:F2}";
        }
    }
}
