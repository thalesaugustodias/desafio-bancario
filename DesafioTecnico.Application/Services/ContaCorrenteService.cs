using DesafioTecnico.Application.DTOs;
using DesafioTecnico.Application.Interfaces;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Enums;
using DesafioTecnico.Domain.Exceptions;
using DesafioTecnico.Domain.Interfaces;

namespace DesafioTecnico.Application.Services
{
    public class ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository,IMovimentoRepository movimentoRepository) : IContaCorrenteService
    {
        public async Task<MovimentacaoResponse> MovimentarContaAsync(MovimentacaoRequest request)
        {
            if (request.Valor <= 0)
                throw new InvalidValueException();

            if (string.IsNullOrEmpty(request.TipoMovimento) ||
                (request.TipoMovimento != "C" && request.TipoMovimento != "D"))
                throw new InvalidTypeException();

            var conta = await contaCorrenteRepository.ObterPorIdAsync(request.IdContaCorrente) ?? throw new InvalidAccountException();
            if (!conta.Ativo)
                throw new InactiveAccountException();

            var tipoMovimento = request.TipoMovimento == "C" ? TipoMovimento.Credito : TipoMovimento.Debito;
            var movimento = Movimento.Criar(request.IdContaCorrente, tipoMovimento, request.Valor);

            var idMovimento = await movimentoRepository.AdicionarAsync(movimento);

            return new MovimentacaoResponse
            {
                IdMovimento = idMovimento
            };
        }

        public async Task<SaldoResponse> ConsultarSaldoAsync(string idContaCorrente)
        {
            var conta = await contaCorrenteRepository.ObterPorIdAsync(idContaCorrente) ?? throw new InvalidAccountException();

            if (!conta.Ativo)
                throw new InactiveAccountException();

            var saldo = await movimentoRepository.CalcularSaldoContaAsync(idContaCorrente);

            return new SaldoResponse
            {
                NumeroContaCorrente = conta.Numero,
                NomeTitular = conta.Nome,
                DataHoraConsulta = DateTime.Now,
                ValorSaldo = saldo
            };
        }
    }
}
