using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Services;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync();
}
