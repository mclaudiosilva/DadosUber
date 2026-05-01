using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Services;
using DadosAplicativo.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace DadosAplicativo.Api.Controllers;

[ApiController]
[Route("api/lancamentos")]
public class LancamentosController : ControllerBase
{
    private readonly ILancamentoService _lancamentoService;

    public LancamentosController(ILancamentoService lancamentoService)
    {
        _lancamentoService = lancamentoService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] LancamentoRequest request)
    {
        try { return Ok(ApiResponse<int>.Ok(await _lancamentoService.CriarAsync(request), "Lançamento criado com sucesso.")); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<int>.Fail(ex.Message)); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] LancamentoRequest request)
    {
        try { await _lancamentoService.AtualizarAsync(id, request); return Ok(ApiResponse<bool>.Ok(true, "Lançamento atualizado com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<bool>.Fail(ex.Message)); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try { await _lancamentoService.ExcluirAsync(id); return Ok(ApiResponse<bool>.Ok(true, "Lançamento excluído com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
    }
}
