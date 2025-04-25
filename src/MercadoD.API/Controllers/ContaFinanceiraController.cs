using MassTransit;
using MassTransit.Mediator;
using MercadoD.Application.Data;
using MercadoD.Application.Loja.FluxoCaixa.GetContaFinanceira;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MercadoD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaFinanceiraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaFinanceiraController(IMediator mediator)
                => _mediator = mediator;

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Recupera Conta Financeira")]
        [ProducesResponseType(typeof(ContaFinanceiraDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetContaFinanceiraQuery query)
        {
            var client = _mediator.CreateRequestClient<GetContaFinanceiraQuery>();
            var response = await client.GetResponse<ContaFinanceiraDto, NotFoundResponse>(query);

            if (response.Is(out Response<ContaFinanceiraDto> ok))
                return Ok(ok.Message);

            return NotFound();
        }

        [HttpGet("{PaginaAtual}/{QtdRegistros}")]
        [SwaggerOperation(Summary = "Recupera um lista de Contas Financeiras")]
        [ProducesResponseType(typeof(PagedResult<ContaFinanceiraDto>), StatusCodes.Status200OK)]        
        public async Task<IActionResult> GetAll([FromRoute] GetAllContaFinanceiraQuery query)
        {
            var client = _mediator.CreateRequestClient<GetAllContaFinanceiraQuery>();
            var response = await client.GetResponse<PagedResult<ContaFinanceiraDto>>(query);
            return Ok(response.Message);
        }
    }
}
