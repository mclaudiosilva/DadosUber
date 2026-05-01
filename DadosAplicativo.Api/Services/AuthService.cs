using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<(int StatusCode, LoginResponse Response)> LoginAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Usuario) || string.IsNullOrWhiteSpace(request.Senha))
        {
            return (401, new LoginResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." });
        }

        var usuario = await _authRepository.BuscarPorUsuarioAsync(request.Usuario);
        if (usuario is null)
        {
            return (401, new LoginResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." });
        }

        if (!usuario.Ativo)
        {
            return (403, new LoginResponse { Sucesso = false, Mensagem = "Usuário inativo." });
        }

        var senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash);
        if (!senhaValida)
        {
            await _authRepository.IncrementarTentativaLoginAsync(usuario.IdUsuario);
            return (401, new LoginResponse { Sucesso = false, Mensagem = "Usuário ou senha inválidos." });
        }

        await _authRepository.AtualizarLoginSucessoAsync(usuario.IdUsuario);

        return (200, new LoginResponse
        {
            Sucesso = true,
            Mensagem = "Login realizado com sucesso.",
            Usuario = new UsuarioLogadoResponse
            {
                IdUsuario = usuario.IdUsuario,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Usuario = usuario.Usuario,
                Perfil = usuario.Perfil
            }
        });
    }
}
