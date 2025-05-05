using MassTransit;
using MassTransit.Testing;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Common.Data;
using MercadoD.Common.Time;
using MercadoD.Domain.Loja.FluxoCaixa;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace MercadoD.Domain.Tests.Loja.FluxoCaixa.CreateLancamentoFinanceiro
{
    public class CreateLancamentoFinanceiroTests
    {
        private readonly Mock<ILancamentoFinanceiroRepository> _mockLancamentoFinanceiroRepository;
        private readonly Mock<IContaFinanceiraRepository> _mockContaFinanceiraRepository;

        public CreateLancamentoFinanceiroTests()
        {
            _mockLancamentoFinanceiroRepository = new Mock<ILancamentoFinanceiroRepository>();
            _mockContaFinanceiraRepository = new Mock<IContaFinanceiraRepository>();
        }

        [Fact]
        public async Task Should_Create_LancamentoFinanceiro()
        {
            var contaFin = new ContaFinanceira(Guid.NewGuid(), "Test Account", ContaFinanceiraTipo.APagar);
            
            _mockContaFinanceiraRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(contaFin);

            var mockUnitOfWork = new Mock<IUnitWork>();

            await using var provider = new ServiceCollection()                
                .AddMassTransitTestHarness(x =>
                {
                    Application.DependencyInjection.AddMediatorConsumersLocal(x);
                })
                .AddScoped(_ => _mockLancamentoFinanceiroRepository.Object)
                .AddScoped(_ => _mockContaFinanceiraRepository.Object)
                .AddScoped(_ => mockUnitOfWork.Object)
                .BuildServiceProvider(true);

            var harness = provider.GetRequiredService<ITestHarness>();

            await harness.Start();

            Clock.Override(() => DateTime.UtcNow);

            var command = new CreateLancamentoFinanceiroCommand(
                contaFin.Id,
                100.00m,
                "Test Description", null, null, null);

            var client = harness.GetRequestClient<CreateLancamentoFinanceiroCommand>();
            var response = await client.GetResponse<CreateLancamentoFinanceiroCommandResponse,
                CreateLancamentoFinanceiroCommandResponseError>(command);

            Assert.IsType<CreateLancamentoFinanceiroCommandResponse>(response.Message);
            var msg = (CreateLancamentoFinanceiroCommandResponse) response.Message;

            Assert.NotEqual(Guid.Empty, msg.Id);
        }
    }
}
