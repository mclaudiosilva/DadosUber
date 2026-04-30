using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync();
    Task<int> CriarAsync(EmpresaRequest request);
    Task<bool> AtualizarAsync(int id, EmpresaRequest request);
    Task<bool> ExcluirAsync(int id);
    Task<bool> NomeExisteAsync(string nomeEmpresa, int? idIgnorar = null);
    Task<bool> ExisteLancamentoVinculadoAsync(int idEmpresa);
    Task<bool> ExistePorIdAsync(int idEmpresa);
}
