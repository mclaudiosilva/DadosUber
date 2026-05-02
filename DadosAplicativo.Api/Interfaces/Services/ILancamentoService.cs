using DadosAplicativo.Api.DTOs.Requests;

namespace DadosAplicativo.Api.Interfaces.Services;

public interface ILancamentoService
{
    Task<int> CriarAsync(LancamentoRequest request);
    Task AtualizarAsync(int id, LancamentoRequest request);
    Task ExcluirAsync(int id);
}
