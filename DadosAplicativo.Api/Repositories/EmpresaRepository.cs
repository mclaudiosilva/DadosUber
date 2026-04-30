using Dapper;
using DadosAplicativo.Api.Data;
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

        return await connection.QueryAsync<EmpresaResponse>(
            "sp_listar_empresas",
            commandType: CommandType.StoredProcedure);
    }
}
