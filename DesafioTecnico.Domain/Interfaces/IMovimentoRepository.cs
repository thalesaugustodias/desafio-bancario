using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<string> AdicionarAsync(Movimento movimento);
        Task<decimal> CalcularSaldoContaAsync(string idContaCorrente);
    }
}
