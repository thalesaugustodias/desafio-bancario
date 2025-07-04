namespace DesafioTecnico.Application.DTOs
{
    public class SaldoResponse
    {
        public int NumeroContaCorrente { get; set; }

        public string NomeTitular { get; set; }

        public DateTime DataHoraConsulta { get; set; }

        public decimal ValorSaldo { get; set; }
    }
}
