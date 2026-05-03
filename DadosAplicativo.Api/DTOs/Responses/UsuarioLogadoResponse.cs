namespace DadosAplicativo.Api.DTOs.Responses;

public class UsuarioLogadoResponse
{
    public long IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Perfil { get; set; } = string.Empty;
}
