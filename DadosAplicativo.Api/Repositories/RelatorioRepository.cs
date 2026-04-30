using System.Data;
using System.Text.Json;
using Dapper;
using DadosAplicativo.Api.Data;
using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;

namespace DadosAplicativo.Api.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public RelatorioRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<DadosDiaResponse?> ConsultarDadosDoDiaAsync(DateTime data)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("p_data", data.Date);

        return await connection.QueryFirstOrDefaultAsync<DadosDiaResponse>(
            "sp_consultar_dados_do_dia",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<LancamentoResponse>> ListarLancamentosPorDiaAsync(DateTime data)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("p_data", data.Date);

        return await connection.QueryAsync<LancamentoResponse>(
            "sp_listar_lancamentos_por_dia",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<LancamentoResponse>> ListarLancamentosPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var parameters = CriarParametrosPeriodo(filtro);

        return await connection.QueryAsync<LancamentoResponse>(
            "sp_listar_lancamentos_periodo_empresa",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<ResumoPeriodoResponse>> ResumoPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var parameters = CriarParametrosPeriodo(filtro);

        return await connection.QueryAsync<ResumoPeriodoResponse>(
            "sp_resumo_periodo_empresa",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<DashboardPeriodoResponse?> DashboardPeriodoEmpresaAsync(FiltroPeriodoEmpresaRequest filtro)
    {
        using var connection = _dbConnectionFactory.CreateConnection();

        var parameters = CriarParametrosPeriodo(filtro);

        return await connection.QueryFirstOrDefaultAsync<DashboardPeriodoResponse>(
            "sp_dashboard_periodo_empresa",
            parameters,
            commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters CriarParametrosPeriodo(FiltroPeriodoEmpresaRequest filtro)
    {
        var parameters = new DynamicParameters();
        parameters.Add("p_data_inicio", filtro.DataInicio.Date);
        parameters.Add("p_data_fim", filtro.DataFim.Date);

        string? idsEmpresasJson = null;
        if (filtro.IdsEmpresas is { Count: > 0 })
        {
            idsEmpresasJson = JsonSerializer.Serialize(filtro.IdsEmpresas);
        }

        parameters.Add("p_ids_empresas_json", idsEmpresasJson, DbType.String);
        return parameters;
    }
}
