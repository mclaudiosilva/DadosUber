using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _relatorioRepository;

    public RelatorioService(IRelatorioRepository relatorioRepository)
    {
        _relatorioRepository = relatorioRepository;
    }

    public Task<DadosDiaResponse?> ConsultarDadosDoDiaAsync(DateTime data)
    {
        return _relatorioRepository.ConsultarDadosDoDiaAsync(data);
    }

    public Task<IEnumerable<LancamentoResponse>> ListarLancamentosPorDiaAsync(DateTime data)
    {
        return _relatorioRepository.ListarLancamentosPorDiaAsync(data);
    }

    public Task<IEnumerable<LancamentoResponse>> ListarLancamentosPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        ValidarPeriodo(filtro.DataInicio, filtro.DataFim);
        return _relatorioRepository.ListarLancamentosPeriodoEmpresaAsync(filtro);
    }

    public Task<IEnumerable<ResumoPeriodoResponse>> ResumoPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        ValidarPeriodo(filtro.DataInicio, filtro.DataFim);
        return _relatorioRepository.ResumoPeriodoEmpresaAsync(filtro);
    }

    public Task<DashboardPeriodoResponse?> DashboardPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        ValidarPeriodo(filtro.DataInicio, filtro.DataFim);
        return _relatorioRepository.DashboardPeriodoEmpresaAsync(filtro);
    }

    private static void ValidarPeriodo(DateTime dataInicio, DateTime dataFim)
    {
        if (dataInicio.Date > dataFim.Date)
        {
            throw new ArgumentException("DataInicio deve ser menor ou igual a DataFim.");
        }
    }
}
