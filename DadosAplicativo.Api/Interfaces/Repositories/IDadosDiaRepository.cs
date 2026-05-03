using DadosAplicativo.Api.DTOs.Requests;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface IDadosDiaRepository
{
    Task<int> CriarAsync(DadosDiaRequest request);
    Task<bool> AtualizarAsync(int id, DadosDiaRequest request);
    Task<bool> ExcluirAsync(int id);
    Task<bool> ExistePorDataAsync(DateTime data, int? idIgnorar = null);
    Task<bool> ExisteLancamentoVinculadoAsync(int idDadosDia);
    Task<bool> ExistePorIdAsync(int idDadosDia);
    Task<int?> ObterIdPorDataAsync(DateTime data);
}
