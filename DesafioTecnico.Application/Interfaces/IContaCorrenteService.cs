using DesafioTecnico.Application.DTOs;

namespace DesafioTecnico.Application.Interfaces
{
    public interface IContaCorrenteService
    {
        Task<MovimentacaoResponse> MovimentarContaAsync(MovimentacaoRequest request);
        Task<SaldoResponse> ConsultarSaldoAsync(string idContaCorrente);
    }
}
