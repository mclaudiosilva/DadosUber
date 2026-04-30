using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync();
}
