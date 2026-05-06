using Dapper;
using DadosAplicativo.Api.Data;
using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Repositories;

namespace DadosAplicativo.Api.Repositories;

public class DadosDiaRepository : IDadosDiaRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public DadosDiaRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> CriarAsync(DadosDiaRequest request)
    {
        const string sql = @"INSERT INTO dados_do_dia (data, valorAlimentacao, totalKmRodado, mediaConsumo, valorMedioCombustivel, valorAbastecido, horas_trabalhadas)
VALUES (@Data, @ValorAlimentacao, @TotalKmRodado, @MediaConsumo, @ValorMedioCombustivel, @ValorAbastecido, @HorasTrabalhadas);SELECT LAST_INSERT_ID();";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task<bool> AtualizarAsync(int id, DadosDiaRequest request)
    {
        const string sql = @"UPDATE dados_do_dia 
SET data = @Data, valorAlimentacao = @ValorAlimentacao, totalKmRodado = @TotalKmRodado, mediaConsumo = @MediaConsumo,
valorMedioCombustivel = @ValorMedioCombustivel, valorAbastecido = @ValorAbastecido, horas_trabalhadas = @HorasTrabalhadas
WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id, request.Data, request.ValorAlimentacao, request.TotalKmRodado, request.MediaConsumo, request.ValorMedioCombustivel, request.ValorAbastecido, request.HorasTrabalhadas });

       
        return rows > 0;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        const string sql = "DELETE FROM dados_do_dia WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }

    public async Task<bool> ExistePorDataAsync(DateTime data, int? idIgnorar = null)
    {
        const string sql = "SELECT COUNT(1) FROM dados_do_dia WHERE data = @Data AND (@IdIgnorar IS NULL OR id <> @IdIgnorar)";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { Data = data.Date, IdIgnorar = idIgnorar });
        return count > 0;
    }

    public async Task<bool> ExisteLancamentoVinculadoAsync(int idDadosDia)
    {
        const string sql = "SELECT COUNT(1) FROM lancamento WHERE id_dados_do_dia = @IdDadosDia";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { IdDadosDia = idDadosDia });
        return count > 0;
    }

    public async Task<bool> ExistePorIdAsync(int idDadosDia)
    {
        const string sql = "SELECT COUNT(1) FROM dados_do_dia WHERE id = @IdDadosDia";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { IdDadosDia = idDadosDia });
        return count > 0;
    }
    public async Task<int?> ObterIdPorDataAsync(DateTime data)
    {
        const string sql = "SELECT id FROM dados_do_dia WHERE data = @Data LIMIT 1";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int?>(sql, new { Data = data.Date });
    }

}
