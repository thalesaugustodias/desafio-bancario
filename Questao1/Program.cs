using System.Globalization;

namespace Questao1
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

            try
            {
                Console.Write("Entre o número da conta: ");
                int numero = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Número da conta não pode ser nulo."));

                Console.Write("Entre o titular da conta: ");
                string titular = Console.ReadLine() ?? throw new InvalidOperationException("Titular da conta não pode ser nulo.");

                Console.Write("Haverá depósito inicial (s/n)? ");
                char resposta = char.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Resposta não pode ser nula."));

                ContaBancaria conta;

                if (resposta == 's' || resposta == 'S')
                {
                    Console.Write("Entre o valor de depósito inicial: ");
                    decimal depositoInicial = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Valor de depósito inicial não pode ser nulo."));
                    conta = new ContaBancaria(numero, titular, depositoInicial);
                }
                else
                {
                    conta = new ContaBancaria(numero, titular);
                }

                Console.WriteLine();
                Console.WriteLine("Dados da conta:");
                Console.WriteLine(conta);

                Console.WriteLine();
                Console.Write("Entre um valor para depósito: ");
                decimal valorDeposito = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Valor de depósito não pode ser nulo."));
                conta.Depositar(valorDeposito);

                Console.WriteLine("Dados da conta atualizados:");
                Console.WriteLine(conta);

                Console.WriteLine();
                Console.Write("Entre um valor para saque: ");
                decimal valorSaque = decimal.Parse(Console.ReadLine() ?? throw new InvalidOperationException("Valor de saque não pode ser nulo."));
                conta.Sacar(valorSaque);

                Console.WriteLine("Dados da conta atualizados:");
                Console.WriteLine(conta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}