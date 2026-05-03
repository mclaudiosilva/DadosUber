using Dapper;
using DadosAplicativo.Api.Data;
using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;
using System.Data;

namespace DadosAplicativo.Api.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public EmpresaRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryAsync<EmpresaResponse>("sp_listar_empresas", commandType: CommandType.StoredProcedure);
    }

    public async Task<int> CriarAsync(EmpresaRequest request)
    {
        const string sql = @"INSERT INTO empresa (nome_empresa) VALUES (@NomeEmpresa); SELECT LAST_INSERT_ID();";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, request);
    }

    public async Task<bool> AtualizarAsync(int id, EmpresaRequest request)
    {
        const string sql = "UPDATE empresa SET nome_empresa = @NomeEmpresa WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id, request.NomeEmpresa });
        return rows > 0;
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        const string sql = "DELETE FROM empresa WHERE id = @Id";
        using var connection = _dbConnectionFactory.CreateConnection();
        var rows = await connection.ExecuteAsync(sql, new { Id = id });
        return rows > 0;
    }

    public async Task<bool> NomeExisteAsync(string nomeEmpresa, int? idIgnorar = null)
    {
        const string sql = "SELECT COUNT(1) FROM empresa WHERE nome_empresa = @NomeEmpresa AND (@IdIgnorar IS NULL OR id <> @IdIgnorar)";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { NomeEmpresa = nomeEmpresa, IdIgnorar = idIgnorar });
        return count > 0;
    }

    public async Task<bool> ExisteLancamentoVinculadoAsync(int idEmpresa)
    {
        const string sql = "SELECT COUNT(1) FROM lancamento WHERE id_empresa = @IdEmpresa";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { IdEmpresa = idEmpresa });
        return count > 0;
    }

    public async Task<bool> ExistePorIdAsync(int idEmpresa)
    {
        const string sql = "SELECT COUNT(1) FROM empresa WHERE id = @IdEmpresa";
        using var connection = _dbConnectionFactory.CreateConnection();
        var count = await connection.ExecuteScalarAsync<int>(sql, new { IdEmpresa = idEmpresa });
        return count > 0;

        
    }
}
