using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class LancamentoService : ILancamentoService
{
    private readonly ILancamentoRepository _lancamentoRepository;
    private readonly IDadosDiaRepository _dadosDiaRepository;
    private readonly IEmpresaRepository _empresaRepository;

    public LancamentoService(ILancamentoRepository lancamentoRepository, IDadosDiaRepository dadosDiaRepository, IEmpresaRepository empresaRepository)
    {
        _lancamentoRepository = lancamentoRepository;
        _dadosDiaRepository = dadosDiaRepository;
        _empresaRepository = empresaRepository;
    }

    public async Task<int> CriarAsync(LancamentoRequest request)
    {
        await Validar(request);
        return await _lancamentoRepository.CriarAsync(request);
    }

    public async Task AtualizarAsync(int id, LancamentoRequest request)
    {
        if (!await _lancamentoRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Lançamento não encontrado.");
        await Validar(request);
        await _lancamentoRepository.AtualizarAsync(id, request);
    }

    public async Task ExcluirAsync(int id)
    {
        if (!await _lancamentoRepository.ExistePorIdAsync(id))
            throw new KeyNotFoundException("Lançamento não encontrado.");
        await _lancamentoRepository.ExcluirAsync(id);
    }

    private async Task Validar(LancamentoRequest request)
    {
        if (request.ValorCorridas < 0)
            throw new ArgumentException("Valor de corrida não pode ser negativo.");

        if (request.IdDadosDoDia <= 0)
        {
            var idDadosDoDia = await _dadosDiaRepository.ObterIdPorDataAsync(request.Data);
            if (!idDadosDoDia.HasValue)
                throw new ArgumentException("Não há Dados do Dia cadastrados para esta data. Cadastre primeiro.");
            request.IdDadosDoDia = idDadosDoDia.Value;
        }
        else if (!await _dadosDiaRepository.ExistePorIdAsync(request.IdDadosDoDia))
        {
            throw new ArgumentException("IdDadosDoDia informado não existe.");
        }

        if (!await _dadosDiaRepository.ExistePorIdAsync(request.IdDadosDoDia))
            throw new ArgumentException("IdDadosDoDia informado não existe.");
        if (!await _empresaRepository.ExistePorIdAsync(request.IdEmpresa))
            throw new ArgumentException("IdEmpresa informado não existe.");
    }
}
