using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Services;
using DadosAplicativo.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace DadosAplicativo.Api.Controllers;

[ApiController]
[Route("api/dados-dia")]
public class DadosDiaController : ControllerBase
{
    private readonly IDadosDiaService _dadosDiaService;

    public DadosDiaController(IDadosDiaService dadosDiaService)
    {
        _dadosDiaService = dadosDiaService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] DadosDiaRequest request)
    {
        try { return Ok(ApiResponse<int>.Ok(await _dadosDiaService.CriarAsync(request), "Dados do dia criado com sucesso.")); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<int>.Fail(ex.Message)); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] DadosDiaRequest request)
    {
        try { await _dadosDiaService.AtualizarAsync(id, request); return Ok(ApiResponse<bool>.Ok(true, "Dados do dia atualizado com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<bool>.Fail(ex.Message)); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try { await _dadosDiaService.ExcluirAsync(id); return Ok(ApiResponse<bool>.Ok(true, "Dados do dia excluído com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
        catch (InvalidOperationException ex) { return BadRequest(ApiResponse<bool>.Fail(ex.Message)); }
    }
}
