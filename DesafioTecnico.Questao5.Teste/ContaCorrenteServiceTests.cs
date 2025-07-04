using DesafioTecnico.Application.DTOs;
using DesafioTecnico.Application.Services;
using DesafioTecnico.Domain.Entities;
using DesafioTecnico.Domain.Exceptions;
using DesafioTecnico.Domain.Interfaces;
using Moq;

namespace DesafioTecnico.Questao5.Teste
{
    [TestClass]
    public class ContaCorrenteServiceTests
    {
        private Mock<IContaCorrenteRepository> _mockContaRepository;
        private Mock<IMovimentoRepository> _mockMovimentoRepository;
        private ContaCorrenteService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockContaRepository = new Mock<IContaCorrenteRepository>();
            _mockMovimentoRepository = new Mock<IMovimentoRepository>();
            _service = new ContaCorrenteService(_mockContaRepository.Object, _mockMovimentoRepository.Object);
        }

        [TestMethod]
        public async Task MovimentarConta_ContaValida_DeveRetornarSucesso()
        {
            // Arrange
            var request = new MovimentacaoRequest
            {
                IdRequisicao = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            var conta = new ContaCorrente("B6BAFC09-6967-ED11-A567-055DFA4A16C9", 123, "Katherine Sanchez", true);
            _mockContaRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<string>()))
                .ReturnsAsync(conta);

            _mockMovimentoRepository.Setup(x => x.AdicionarAsync(It.IsAny<Movimento>()))
                .ReturnsAsync(Guid.NewGuid().ToString());

            // Act
            var result = await _service.MovimentarContaAsync(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.IdMovimento);
        }

        [TestMethod]
        public async Task MovimentarConta_ContaInvalida_DeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentacaoRequest
            {
                IdRequisicao = Guid.NewGuid().ToString(),
                IdContaCorrente = "CONTA-INEXISTENTE",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            _mockContaRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ContaCorrente)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidAccountException>(
                () => _service.MovimentarContaAsync(request));
        }

        [TestMethod]
        public async Task MovimentarConta_ContaInativa_DeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentacaoRequest
            {
                IdRequisicao = Guid.NewGuid().ToString(),
                IdContaCorrente = "F475F943-7067-ED11-A06B-7E5DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "C"
            };

            var conta = new ContaCorrente("F475F943-7067-ED11-A06B-7E5DFA4A16C9", 741, "Ameena Lynn", false);
            _mockContaRepository.Setup(x => x.ObterPorIdAsync(It.IsAny<string>()))
                .ReturnsAsync(conta);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InactiveAccountException>(
                () => _service.MovimentarContaAsync(request));
        }

        [TestMethod]
        public async Task MovimentarConta_ValorInvalido_DeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentacaoRequest
            {
                IdRequisicao = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = -100.00m,
                TipoMovimento = "C"
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidValueException>(
                () => _service.MovimentarContaAsync(request));
        }

        [TestMethod]
        public async Task MovimentarConta_TipoInvalido_DeveLancarExcecao()
        {
            // Arrange
            var request = new MovimentacaoRequest
            {
                IdRequisicao = Guid.NewGuid().ToString(),
                IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
                Valor = 100.00m,
                TipoMovimento = "X"
            };

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidTypeException>(
                () => _service.MovimentarContaAsync(request));
        }

        [TestMethod]
        public async Task ConsultarSaldo_ContaValida_DeveRetornarSaldo()
        {
            // Arrange
            var idContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9";
            var conta = new ContaCorrente(idContaCorrente, 123, "Katherine Sanchez", true);
            var saldoEsperado = 250.75m;

            _mockContaRepository.Setup(x => x.ObterPorIdAsync(idContaCorrente))
                .ReturnsAsync(conta);

            _mockMovimentoRepository.Setup(x => x.CalcularSaldoContaAsync(idContaCorrente))
                .ReturnsAsync(saldoEsperado);

            // Act
            var result = await _service.ConsultarSaldoAsync(idContaCorrente);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(123, result.NumeroContaCorrente);
            Assert.AreEqual("Katherine Sanchez", result.NomeTitular);
            Assert.AreEqual(saldoEsperado, result.ValorSaldo);
        }

        [TestMethod]
        public async Task ConsultarSaldo_ContaInexistente_DeveLancarExcecao()
        {
            // Arrange
            var idContaCorrente = "CONTA-INEXISTENTE";

            _mockContaRepository.Setup(x => x.ObterPorIdAsync(idContaCorrente))
                .ReturnsAsync((ContaCorrente)null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<InvalidAccountException>(
                () => _service.ConsultarSaldoAsync(idContaCorrente));
        }
    }
}
