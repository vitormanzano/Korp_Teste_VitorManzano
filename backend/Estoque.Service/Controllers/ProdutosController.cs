using Estoque.Service.Dtos;
using Estoque.Service.Exceptions;
using Estoque.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Estoque.Service.Controllers;

[ApiController]
[Route("produtos")]
public class ProdutosController(IProdutoService produtoService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProdutoResponse>>> Listar(CancellationToken cancellationToken)
    {
        var produtos = await produtoService.ListarAsync(cancellationToken);
        return Ok(produtos);
    }

    [HttpGet("{codigo}")]
    public async Task<ActionResult<ProdutoResponse>> ObterPorCodigo(
        string codigo,
        CancellationToken cancellationToken)
    {
        var produto = await produtoService.ObterPorCodigoAsync(codigo, cancellationToken);

        if (produto is null)
        {
            return NotFound(new { mensagem = $"Produto '{codigo}' não encontrado." });
        }

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> Criar(
        CriarProdutoRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var produto = await produtoService.CriarAsync(request, cancellationToken);
            return CreatedAtAction(nameof(ObterPorCodigo), new { codigo = produto.Codigo }, produto);
        }
        catch (ProdutoCodigoDuplicadoException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }

    [HttpPost("debitar")]
    public async Task<IActionResult> Debitar(List<ItemDebito> itens, CancellationToken cancellationToken)
    {
        try
        {
            await produtoService.DebitarAsync(itens, cancellationToken);
            return NoContent();
        }
        catch (ProdutoNaoEncontradoException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
        catch (SaldoInsuficienteException ex)
        {
            return Conflict(new { mensagem = ex.Message, codigo = ex.Codigo });
        }
        catch (ConcorrenciaException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
    }
}
