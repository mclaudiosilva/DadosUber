using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Services;
using DadosAplicativo.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace DadosAplicativo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("dados-dia/{data}")]
    [ProducesResponseType(typeof(ApiResponse<DadosDiaResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DadosDiaResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConsultarDadosDoDia([FromRoute] DateTime data)
    {
        var resultado = await _relatorioService.ConsultarDadosDoDiaAsync(data);
        if (resultado is null)
        {
            return NotFound(ApiResponse<DadosDiaResponse>.Fail("Nenhum dado encontrado para a data informada."));
        }

        return Ok(ApiResponse<DadosDiaResponse>.Ok(resultado));
    }

    [HttpGet("lancamentos-dia/{data}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<LancamentoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarLancamentosPorDia([FromRoute] DateTime data)
    {
        var resultado = await _relatorioService.ListarLancamentosPorDiaAsync(data);
        return Ok(ApiResponse<IEnumerable<LancamentoResponse>>.Ok(resultado));
    }

    [HttpPost("lancamentos-periodo")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<LancamentoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<LancamentoResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListarLancamentosPeriodo([FromBody] FiltroPeriodoEmpresaRequest request)
    {
        try
        {
            var resultado = await _relatorioService.ListarLancamentosPeriodoEmpresaAsync(request);
            return Ok(ApiResponse<IEnumerable<LancamentoResponse>>.Ok(resultado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<IEnumerable<LancamentoResponse>>.Fail(ex.Message));
        }
    }

    [HttpPost("resumo-periodo")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ResumoPeriodoResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ResumoPeriodoResponse>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResumoPeriodo([FromBody] FiltroPeriodoEmpresaRequest request)
    {
        try
        {
            var resultado = await _relatorioService.ResumoPeriodoEmpresaAsync(request);
            return Ok(ApiResponse<IEnumerable<ResumoPeriodoResponse>>.Ok(resultado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<IEnumerable<ResumoPeriodoResponse>>.Fail(ex.Message));
        }
    }

    [HttpPost("dashboard-periodo")]
    [ProducesResponseType(typeof(ApiResponse<DashboardPeriodoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<DashboardPeriodoResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<DashboardPeriodoResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DashboardPeriodo([FromBody] FiltroPeriodoEmpresaRequest request)
    {
        try
        {
            var resultado = await _relatorioService.DashboardPeriodoEmpresaAsync(request);
            if (resultado is null)
            {
                return NotFound(ApiResponse<DashboardPeriodoResponse>.Fail("Nenhum dado encontrado para o período informado."));
            }

            return Ok(ApiResponse<DashboardPeriodoResponse>.Ok(resultado));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ApiResponse<DashboardPeriodoResponse>.Fail(ex.Message));
        }
    }
}
