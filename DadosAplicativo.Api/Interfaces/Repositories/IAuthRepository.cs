using DadosAplicativo.Api.Models.Auth;

namespace DadosAplicativo.Api.Interfaces.Repositories;

public interface IAuthRepository
{
    Task<UsuarioAuthModel?> BuscarPorUsuarioAsync(string usuario);
    Task AtualizarLoginSucessoAsync(long idUsuario);
    Task IncrementarTentativaLoginAsync(long idUsuario);
}
