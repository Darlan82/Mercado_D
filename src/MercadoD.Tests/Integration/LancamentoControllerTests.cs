using MercadoD.API.Models;
using MercadoD.Application.Services;
using MercadoD.Domain.Entities;
using MercadoD.Domain.Enums;
using MercadoD.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MercadoD.Tests.Integration
{
    /// <summary>
    /// Custom WebApplicationFactory para configurar ambiente de testes.
    /// </summary>
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove DbContextOptions registration
                services.RemoveAll<DbContextOptions<MercadoDbContext>>();
                // Add InMemory database
                services.AddDbContext<MercadoDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));

                // Override ILancamentoService para evitar dependência do ServiceBus
                services.AddScoped<ILancamentoService, TestLancamentoService>();
            });
        }
    }

    /// <summary>
    /// Implementação de ILancamentoService para testes, apenas persiste no banco.
    /// </summary>
    public class TestLancamentoService : ILancamentoService
    {
        private readonly MercadoDbContext _dbContext;

        public TestLancamentoService(MercadoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegistrarLancamentoAsync(LancamentoFinanceiro lancamento)
        {
            _dbContext.Lancamentos.Add(lancamento);
            await _dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// DTO para leitura de saldo consolidado.
    /// </summary>
    public record SaldoDto(DateTime Data, string LojaId, decimal TotalSaldo);

    public class LancamentoControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public LancamentoControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Post_Lancamento_ReturnsCreatedAndPersists()
        {
            var client = _factory.CreateClient();
            var dto = new CreateLancamentoDto
            {
                Valor = 100m,
                Tipo = TipoLancamento.Credito,
                DataHora = DateTime.UtcNow,
                LojaId = "store1",
                Descricao = "Integration Test"
            };
            var response = await client.PostAsJsonAsync("/api/lancamentos", dto);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var payload = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = payload.GetProperty("id").GetGuid();
            Assert.NotEqual(Guid.Empty, id);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MercadoDbContext>();
            var entity = await db.Lancamentos.FindAsync(id);
            Assert.NotNull(entity);
            Assert.Equal(dto.Valor, entity!.Valor);
            Assert.Equal(dto.LojaId, entity.LojaId);
        }

        [Fact]
        public async Task Get_Saldos_ReturnsCorrectSaldo()
        {
            var client = _factory.CreateClient();
            var date = DateTime.UtcNow.Date;
            var dto1 = new CreateLancamentoDto { Valor = 150m, Tipo = TipoLancamento.Credito, DataHora = date.AddHours(2), LojaId = "store1" };
            var dto2 = new CreateLancamentoDto { Valor = 50m, Tipo = TipoLancamento.Debito, DataHora = date.AddHours(4), LojaId = "store1" };
            await client.PostAsJsonAsync("/api/lancamentos", dto1);
            await client.PostAsJsonAsync("/api/lancamentos", dto2);

            var response = await client.GetAsync($"/api/saldos?data={date:yyyy-MM-dd}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var saldos = await response.Content.ReadFromJsonAsync<List<SaldoDto>>();
            Assert.NotNull(saldos);
            var storeSaldo = saldos!.Single(s => s.LojaId == "store1");
            Assert.Equal(150m - 50m, storeSaldo.TotalSaldo);
            Assert.Equal(date, storeSaldo.Data.Date);
        }
    }
}