using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Services;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync();
    Task<int> CriarAsync(EmpresaRequest request);
    Task AtualizarAsync(int id, EmpresaRequest request);
    Task ExcluirAsync(int id);
}
