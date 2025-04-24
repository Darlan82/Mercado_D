namespace MercadoD.API.Controllers;
using MercadoD.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// Controller para lançamentos financeiros e consulta de saldos.
/// </summary>
[ApiController]
// Define rota base /api/lancamentos
[Route("api/lancamentos")]
public class LancamentoController : ControllerBase
{
    //private readonly ILancamentoService _lancamentoService;
    //private readonly MercadoDbContext _dbContext;

    //public LancamentoController(ILancamentoService lancamentoService, MercadoDbContext dbContext)
    //{
    //    _lancamentoService = lancamentoService;
    //    _dbContext = dbContext;
    //}

    /// <summary>
    /// Registra um novo lançamento financeiro.
    /// </summary>
    /// <param name="dto">Dados do lançamento.</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateLancamentoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        //try
        //{
        //    // Mapeia DTO para entidade de domínio usando Mapster
        //    var lancamento = dto.Adapt<LancamentoFinanceiro>();

        //    await _lancamentoService.RegistrarLancamentoAsync(lancamento);

        //    // Retorna 201 Created com o id do novo recurso
        //    var uri = $"/api/lancamentos/{lancamento.Id}";
        //    return Created(uri, new { id = lancamento.Id });
        //}
        //catch (ArgumentException ex)
        //{
        //    return BadRequest(new { error = ex.Message });
        //}

        return Ok();
    }

    /// <summary>
    /// Retorna os saldos consolidados para a data informada.
    /// </summary>
    /// <param name="data">Data no formato yyyy-MM-dd.</param>
    [HttpGet("/api/saldos")]
    public async Task<IActionResult> GetSaldos([FromQuery] string data)
    {
        //if (string.IsNullOrWhiteSpace(data))
        //    return BadRequest(new { error = "Parâmetro 'data' é obrigatório." });

        //if (!DateTime.TryParseExact(data, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        //    return BadRequest(new { error = "Parâmetro 'data' inválido. Use yyyy-MM-dd." });

        //var lancamentos = await _dbContext.Lancamentos
        //    .Where(l => l.DataHora.Date == date.Date)
        //    .ToListAsync();

        //var saldos = lancamentos
        //    .GroupBy(l => l.LojaId)
        //    .Select(g => new SaldoConsolidado(
        //        data: date.Date,
        //        lojaId: g.Key,
        //        totalSaldo: g.Sum(x => x.Tipo == TipoLancamento.Credito ? x.Valor : -x.Valor)))
        //    .ToList();

        //return Ok(saldos);
        return Ok();
    }
}