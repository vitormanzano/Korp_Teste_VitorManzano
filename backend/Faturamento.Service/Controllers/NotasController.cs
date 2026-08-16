using Faturamento.Service.Dtos;
using Faturamento.Service.Exceptions;
using Faturamento.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Faturamento.Service.Controllers;

[ApiController]
[Route("notas")]
public class NotasController(INotaFiscalService notaFiscalService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NotaFiscalResponse>>> Listar(CancellationToken cancellationToken)
    {
        var notas = await notaFiscalService.ListarAsync(cancellationToken);
        return Ok(notas);
    }

    [HttpPost]
    public async Task<ActionResult<NotaFiscalResponse>> Criar(
        CriarNotaFiscalRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var nota = await notaFiscalService.CriarAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, nota);
        }
        catch (ItemProdutoInexistenteException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (EstoqueIndisponivelException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new { mensagem = ex.Message });
        }
    }

    [HttpPost("{numero}/imprimir")]
    public async Task<ActionResult<NotaFiscalResponse>> Imprimir(string numero, CancellationToken cancellationToken)
    {
        try
        {
            var nota = await notaFiscalService.ImprimirAsync(numero, cancellationToken);
            return Ok(nota);
        }
        catch (NotaFiscalNaoEncontradaException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (NotaFiscalJaFechadaException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (EstoqueIndisponivelException ex)
        {
            return StatusCode(StatusCodes.Status502BadGateway, new { mensagem = ex.Message });
        }
    }
}
