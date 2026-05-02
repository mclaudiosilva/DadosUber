using Dapper;
using DadosAplicativo.Api.Data;
using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Repositories;

namespace DadosAplicativo.Api.Repositories;

public class LancamentoRepository : ILancamentoRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public LancamentoRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<int> CriarAsync(LancamentoRequest request)
    {
        const string sql = @"INSERT INTO lancamento (id_dados_do_dia, data, id_empresa, valor_corridas)
VALUES (@IdDadosDoDia, @Data, @IdEmpresa, @ValorCorridas);
SELECT LAST_INSERT_ID();";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task<bool> AtualizarAsync(int id, LancamentoRequest request)
    {
        const string sql = @"UPDATE lancamento SET id_dados_do_dia = @IdDadosDoDia, data = @Data, id_empresa = @IdEmpresa,
valor_corridas = @ValorCorridas WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id, request.IdDadosDoDia, request.Data, request.IdEmpresa, request.ValorCorridas });
        return rows > 0;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        const string sql = "DELETE FROM lancamento WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }

    public async Task<bool> ExistePorIdAsync(int idLancamento)
    {
        const string sql = "SELECT COUNT(1) FROM lancamento WHERE id = @IdLancamento";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { IdLancamento = idLancamento });
        return count > 0;
    }
}
