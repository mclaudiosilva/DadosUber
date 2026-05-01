using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.DTOs.Responses;

namespace DadosAplicativo.Api.Interfaces.Services;

public interface IAuthService
{
    Task<(int StatusCode, LoginResponse Response)> LoginAsync(LoginRequest request);
}
