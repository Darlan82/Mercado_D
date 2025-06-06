﻿using MassTransit;
using MercadoD.Domain.Loja.FluxoCaixa;
using MercadoD.Domain.Loja.FluxoCaixa.DomainEvents;
using MercadoD.Common.Data;
using MercadoD.Common.Time;
using MercadoD.Common.ValueType;
using Microsoft.Extensions.Logging;

namespace MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro.DomainEventHandlers
{
    internal class LancamentoFinanceiroCreatedDomainEventHandler : IConsumer<LancamentoFinanceiroCreatedDomainEvent>
    {
        private readonly ILancamentoFinanceiroRepository _repository;
        private readonly ISaldoConsolidadoDiarioRepository _saldoConsolidadoDiarioRep;
        private readonly ILogger<LancamentoFinanceiroCreatedDomainEventHandler> _logger;
        private readonly IUnitWork _unitWork;

        public LancamentoFinanceiroCreatedDomainEventHandler(ILancamentoFinanceiroRepository repository, 
            ISaldoConsolidadoDiarioRepository saldoConsolidadoDiarioRep, 
            ILogger<LancamentoFinanceiroCreatedDomainEventHandler> logger, IUnitWork unitWork)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _saldoConsolidadoDiarioRep = saldoConsolidadoDiarioRep ?? throw new ArgumentNullException(nameof(saldoConsolidadoDiarioRep));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _unitWork = unitWork ?? throw new ArgumentNullException(nameof(unitWork));
        }

        public async Task Consume(ConsumeContext<LancamentoFinanceiroCreatedDomainEvent> context)
        {
            await _unitWork.ExecuteTransactionAsync(async () =>
            {
                _logger.LogInformation("Iniciando contabilização de saldo para o lançamento financeiro: {LancamentoId}", context.Message.Id);

                var lancamento = await _repository.GetByIdAsync(context.Message.Id);
                if (lancamento == null)
                    return;

                lancamento.Conta.ContabilizarSaldo(lancamento);

                await ContabilizaSaldosDiarios(lancamento);
            });

            _logger.LogInformation("Contabilização de saldo para o lançamento financeiro realizada: {LancamentoId}", context.Message.Id);
        }

        private async Task ContabilizaSaldosDiarios(LancamentoFinanceiro lancamento)
        {
            var saldoLancamento = await GetOrCreateSaldo(lancamento.ContaId, lancamento.DtLancamento);
            saldoLancamento.SomaSaldoPrevisto(lancamento.Valor);

            if (!lancamento.DtPagamento.HasValue ||
                    lancamento.DtPagamento.Value > Clock.UtcNow)            
                return;            

            var saldoPagamento = lancamento.IsSameDayLancamentoAndDtPagamento()
                ? saldoLancamento
                : await GetOrCreateSaldo(lancamento.ContaId, lancamento.DtPagamento.Value);

            saldoPagamento.SomaSaldoRealizado(lancamento.Valor);
        }

        private async Task<SaldoConsolidadoDiario> GetOrCreateSaldo(Guid contaId, DateTime dtLancamento)
        {
            var dayLancamento = DayStamp.FromDateTime(dtLancamento);
            var saldoLancamento = await _saldoConsolidadoDiarioRep.GetByContaIdDate(contaId, dayLancamento);                    
            if (saldoLancamento == null)
            {
                saldoLancamento = new SaldoConsolidadoDiario(dayLancamento, contaId);
                await _saldoConsolidadoDiarioRep.InsertAsync(saldoLancamento);
            }

            return saldoLancamento;
        }
    }
}
