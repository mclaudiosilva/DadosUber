using DadosAplicativo.Api.Interfaces.Services;
using DadosAplicativo.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;
using DadosAplicativo.Api.DTOs.Responses;

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
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<EmpresaResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarEmpresas()
    {
        var empresas = await _empresaService.ListarEmpresasAsync();
        return Ok(ApiResponse<IEnumerable<EmpresaResponse>>.Ok(empresas));
    }
}
