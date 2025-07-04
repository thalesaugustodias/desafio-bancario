using DesafioTecnico.Domain.Entities;

namespace DesafioTecnico.Domain.Interfaces
{
    public interface IContaCorrenteRepository
    {
        Task<ContaCorrente> ObterPorIdAsync(string idContaCorrente);
        Task<ContaCorrente> ObterComMovimentosAsync(string idContaCorrente);
    }
}
