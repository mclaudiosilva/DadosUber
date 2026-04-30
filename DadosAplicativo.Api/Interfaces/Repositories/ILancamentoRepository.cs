using DadosAplicativo.Api.DTOs.Requests;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface ILancamentoRepository
{
    Task<int> CriarAsync(LancamentoRequest request);
    Task<bool> AtualizarAsync(int id, LancamentoRequest request);
    Task<bool> ExcluirAsync(int id);
    Task<bool> ExistePorIdAsync(int idLancamento);
}
