using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;

    public EmpresaService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync() => _empresaRepository.ListarEmpresasAsync();

    public async Task<int> CriarAsync(EmpresaRequest request)
    {
        ValidarNome(request.NomeEmpresa);
        if (await _empresaRepository.NomeExisteAsync(request.NomeEmpresa))
            throw new ArgumentException("Já existe uma empresa com esse nome.");

        return await _empresaRepository.CriarAsync(request);
    }

    public async Task AtualizarAsync(int id, EmpresaRequest request)
    {
        ValidarNome(request.NomeEmpresa);
        if (!await _empresaRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Empresa não encontrada.");

        if (await _empresaRepository.NomeExisteAsync(request.NomeEmpresa, id))
            throw new ArgumentException("Já existe uma empresa com esse nome.");

        await _empresaRepository.AtualizarAsync(id, request);
    }

    public async Task ExcluirAsync(int id)
    {
        if (!await _empresaRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Empresa não encontrada.");

        if (await _empresaRepository.ExisteLancamentoVinculadoAsync(id))
            throw new InvalidOperationException("Não é possível excluir a empresa pois existem lançamentos vinculados.");

        await _empresaRepository.ExcluirAsync(id);
    }

    private static void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da empresa é obrigatório.");
    public Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync()
    {
        return _empresaRepository.ListarEmpresasAsync();
    }
}
