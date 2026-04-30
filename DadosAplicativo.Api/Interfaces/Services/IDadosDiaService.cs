using DadosAplicativo.Api.DTOs.Requests;

namespace DadosAplicativo.Api.Interfaces.Services;

public interface IDadosDiaService
{
    Task<int> CriarAsync(DadosDiaRequest request);
    Task AtualizarAsync(int id, DadosDiaRequest request);
    Task ExcluirAsync(int id);
}
