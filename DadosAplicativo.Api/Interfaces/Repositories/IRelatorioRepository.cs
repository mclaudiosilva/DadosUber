using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface IRelatorioRepository
{
    Task<DadosDiaResponse?> ConsultarDadosDoDiaAsync(DateTime data);
    Task<IEnumerable<LancamentoResponse>> ListarLancamentosPorDiaAsync(DateTime data);
    Task<IEnumerable<LancamentoResponse>> ListarLancamentosPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro);
    Task<IEnumerable<ResumoPeriodoResponse>> ResumoPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro);
    Task<DashboardPeriodoResponse?> DashboardPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro);
}
