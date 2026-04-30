using DadosAplicativo.Api.Interfaces.Services;
using DadosAplicativo.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.DTOs.Requests;

namespace DadosAplicativo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaService _empresaService;

    public EmpresasController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarEmpresas() => Ok(ApiResponse<IEnumerable<EmpresaResponse>>.Ok(await _empresaService.ListarEmpresasAsync()));

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] EmpresaRequest request)
    {
        try { return Ok(ApiResponse<int>.Ok(await _empresaService.CriarAsync(request), "Empresa criada com sucesso.")); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<int>.Fail(ex.Message)); }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] EmpresaRequest request)
    {
        try { await _empresaService.AtualizarAsync(id, request); return Ok(ApiResponse<bool>.Ok(true, "Empresa atualizada com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
        catch (ArgumentException ex) { return BadRequest(ApiResponse<bool>.Fail(ex.Message)); }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try { await _empresaService.ExcluirAsync(id); return Ok(ApiResponse<bool>.Ok(true, "Empresa excluída com sucesso.")); }
        catch (KeyNotFoundException ex) { return NotFound(ApiResponse<bool>.Fail(ex.Message)); }
        catch (InvalidOperationException ex) { return BadRequest(ApiResponse<bool>.Fail(ex.Message)); }
    }
}
