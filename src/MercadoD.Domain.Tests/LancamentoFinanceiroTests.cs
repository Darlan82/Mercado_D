using MercadoD.Common.Time;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Domain.Loja.FluxoCaixa.DomainEvents;
using MercadoD.Domain.Loja.FluxoCaixa.Exceptions;

namespace MercadoD.Domain.Tests
{
    public class LancamentoFinanceiroTests
    {
        [Theory]
        [InlineData("2023-10-2", "2023-10-1")]
        internal void Should_Throw_Invalid_DtPagamento_Domain_Exception(string sDtLancamento, string sDtPagamento)
        {
            // Arrange
            var dtLancamento = Clock.CreateStringUtc(sDtLancamento);
            var dtPagamento = Clock.CreateStringUtc(sDtPagamento);

            // Assert
            Assert.Throws<InvalidDtPagamentoDomainException>(() =>
            {
                //Act
                LancamentoFinanceiro.Create(
                    Guid.NewGuid(),
                    100.00m,
                    "Test Description",
                    dtLancamento,
                    dtLancamento.AddDays(30),
                    dtPagamento);
            });            
        }

        [Fact]
        internal void Should_Create_LancamentoFinanceiro_For_Valid_Input_Data()
        {
            // Arrange
            var now = Clock.CreateDateUtc(2023, 10, 1);
            using var context = Clock.Override(() => now);

            var contaId = Guid.NewGuid();
            var valor = 100.00m; // Example value  
            var descricao = "Test Description"; // Example description  
            var dtLancamento = Clock.UtcNow; // Example date in Utc  
            var dtVencimento = Clock.UtcNow.AddDays(30); // Example due date in Utc  

            // Act  
            var lancamento = LancamentoFinanceiro.Create(contaId, valor, descricao, dtLancamento, dtVencimento, null);

            // Assert  
            Assert.NotNull(lancamento);
            var domainEvents = lancamento.DomainEvents;
            Assert.NotNull(domainEvents);
            Assert.Single(domainEvents);

            var domainEvent = domainEvents.FirstOrDefault();
            Assert.NotNull(domainEvent);
            Assert.IsType<LancamentoFinanceiroCreatedDomainEvent>(domainEvent);
        }
    }
}