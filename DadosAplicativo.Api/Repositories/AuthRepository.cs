using Dapper;
using DadosAplicativo.Api.Data;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Models.Auth;

namespace DadosAplicativo.Api.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public AuthRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<UsuarioAuthModel?> BuscarPorUsuarioAsync(string usuario)
    {
        const string sql = @"SELECT 
id_usuario AS IdUsuario,
nome AS Nome,
email AS Email,
usuario AS Usuario,
senha_hash AS SenhaHash,
ativo AS Ativo,
perfil AS Perfil,
tentativas_login AS TentativasLogin
FROM usuarios
WHERE usuario = @Usuario
LIMIT 1;";
        using var connection = _dbConnectionFactory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<UsuarioAuthModel>(sql, new { Usuario = usuario });
    }

    public async Task AtualizarLoginSucessoAsync(long idUsuario)
    {
        const string sql = @"UPDATE usuarios 
SET ultimo_login = NOW(), tentativas_login = 0, bloqueado_ate = NULL, atualizado_em = NOW()
WHERE id_usuario = @IdUsuario";
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario });
    }

    public async Task IncrementarTentativaLoginAsync(long idUsuario)
    {
        const string sql = @"UPDATE usuarios 
SET tentativas_login = COALESCE(tentativas_login, 0) + 1, atualizado_em = NOW()
WHERE id_usuario = @IdUsuario";
        using var connection = _dbConnectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario });
    }
}
