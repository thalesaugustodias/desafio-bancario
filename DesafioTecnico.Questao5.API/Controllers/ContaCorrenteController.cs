using DesafioTecnico.Application.DTOs;
using DesafioTecnico.Application.Interfaces;
using DesafioTecnico.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnico.Questao5.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ContaCorrenteController(IContaCorrenteService contaCorrenteService) : ControllerBase
    {

        /// <summary>
        /// Realiza movimentação em uma conta corrente
        /// </summary>
        /// <param name="request">Dados da movimentação</param>
        /// <returns>ID do movimento gerado</returns>
        /// <response code="200">Movimentação realizada com sucesso</response>
        /// <response code="400">Dados inválidos ou erro de negócio</response>
        [HttpPost("movimentacao")]
        [ProducesResponseType(typeof(MovimentacaoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentacaoRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        Message = "Dados de entrada inválidos",
                        Type = "VALIDATION_ERROR"
                    });
                }

                var response = await contaCorrenteService.MovimentarContaAsync(request);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = ex.Message,
                    Type = ex.ErrorType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor",
                    Type = "INTERNAL_ERROR"
                });
            }
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente
        /// </summary>
        /// <param name="idContaCorrente">ID da conta corrente</param>
        /// <returns>Dados do saldo atual</returns>
        /// <response code="200">Saldo consultado com sucesso</response>
        /// <response code="400">Conta inválida ou inativa</response>
        [HttpGet("saldo/{idContaCorrente}")]
        [ProducesResponseType(typeof(SaldoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> ConsultarSaldo(string idContaCorrente)
        {
            try
            {
                if (string.IsNullOrEmpty(idContaCorrente))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Message = "ID da conta corrente é obrigatório",
                        Type = "VALIDATION_ERROR"
                    });
                }

                var response = await contaCorrenteService.ConsultarSaldoAsync(idContaCorrente);
                return Ok(response);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    Message = ex.Message,
                    Type = ex.ErrorType
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse
                {
                    Message = "Erro interno do servidor",
                    Type = "INTERNAL_ERROR"
                });
            }
        }
    }
}
