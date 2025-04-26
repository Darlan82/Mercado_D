namespace MercadoD.API.Controllers;

using MassTransit;
using MassTransit.Mediator;
using MercadoD.Application.Data;
using MercadoD.Application.Loja.FluxoCaixa.CreateLancamentoFinanceiro;
using MercadoD.Application.Loja.FluxoCaixa.GetLancamentoFinanceiro;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

/// <summary>
/// Controller para Lançamentos Financeiros.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[SwaggerTag("Lida com as operações relacionadas aos Lancamento Financeiro.")]
public class LancamentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public LancamentoController(IMediator mediator)
            => _mediator = mediator;

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Recupera Lancamento Financeiro")]
    [ProducesResponseType(typeof(LancamentoFinanceiroDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] GetLancamentoFinanceiroQuery query)
    {
        var client = _mediator.CreateRequestClient<GetLancamentoFinanceiroQuery>();
        var response = await client.GetResponse<LancamentoFinanceiroDto, NotFoundResponse> (query);

        if (response.Is(out Response<LancamentoFinanceiroDto> ok))
            return Ok(ok.Message);

        return NotFound();
    }

    /// <summary>
    /// Registra um novo lançamento financeiro.
    /// </summary>
    /// <param name="dto">Dados do lançamento.</param>
    [HttpPost]
    [SwaggerOperation(Summary = "Cria um novo Lancamento Financeiro")]    
    [ProducesResponseType(typeof(LancamentoFinanceiroDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(CreateLancamentoFinanceiroCommand command)
    {
        var client = _mediator.CreateRequestClient<CreateLancamentoFinanceiroCommand>();
        var response = await client.GetResponse<CreateLancamentoFinanceiroCommandResponse>(command);
        return CreatedAtAction(nameof(Get), new { id = response.Message.Id }, response.Message);
    }

    /// <summary>
    /// Retorna os saldos consolidados para a data informada.
    /// </summary>
    /// <param name="data">Data no formato yyyy-MM-dd.</param>
    //[HttpGet("/api/saldos")]
    //public async Task<IActionResult> GetSaldos([FromQuery] string data)
    //{
    //    //if (string.IsNullOrWhiteSpace(data))
    //    //    return BadRequest(new { error = "Parâmetro 'data' é obrigatório." });

    //    //if (!DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
    //    //    return BadRequest(new { error = "Parâmetro 'data' inválido. Use yyyy-MM-dd." });

    //    //var lancamentos = await _dbContext.Lancamentos
    //    //    .Where(l => l.DataHora.Date == date.Date)
    //    //    .ToListAsync();

    //    //var saldos = lancamentos
    //    //    .GroupBy(l => l.LojaId)
    //    //    .Select(g => new SaldoConsolidado(
    //    //        data: date.Date,
    //    //        lojaId: g.Key,
    //    //        totalSaldo: g.Sum(x => x.Tipo == TipoLancamento.Credito ? x.Valor : -x.Valor)))
    //    //    .ToList();

    //    //return Ok(saldos);
    //    return Ok();
    //}
}