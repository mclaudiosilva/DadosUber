using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class DadosDiaService : IDadosDiaService
{
    private readonly IDadosDiaRepository _dadosDiaRepository;

    public DadosDiaService(IDadosDiaRepository dadosDiaRepository)
    {
        _dadosDiaRepository = dadosDiaRepository;
    }

    public async Task<int> CriarAsync(DadosDiaRequest request)
    {
        Validar(request);
        if (await _dadosDiaRepository.ExistePorDataAsync(request.Data))
            throw new ArgumentException("Já existe registro para esta data.");

        return await _dadosDiaRepository.CriarAsync(request);
    }

    public async Task AtualizarAsync(int id, DadosDiaRequest request)
    {
        Validar(request);
        if (!await _dadosDiaRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Dados do dia não encontrado.");
        if (await _dadosDiaRepository.ExistePorDataAsync(request.Data, id))
            throw new ArgumentException("Já existe registro para esta data.");

        await _dadosDiaRepository.AtualizarAsync(id, request);
    }

    public async Task ExcluirAsync(int id)
    {
        if (!await _dadosDiaRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Dados do dia não encontrado.");
        if (await _dadosDiaRepository.ExisteLancamentoVinculadoAsync(id))
            throw new InvalidOperationException("Não é possível excluir, existem lançamentos vinculados.");
        await _dadosDiaRepository.ExcluirAsync(id);
    }

    private static void Validar(DadosDiaRequest request)
    {
        if (request.ValorAlimentacao < 0 || request.TotalKmRodado < 0 || request.MediaConsumo < 0 || request.ValorMedioCombustivel < 0 || request.ValorAbastecido < 0 || (request.HorasTrabalhadas.HasValue && request.HorasTrabalhadas.Value < 0))
            throw new ArgumentException("Não são permitidos valores negativos.");
    }
}
