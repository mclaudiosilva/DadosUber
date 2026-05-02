namespace DadosAplicativo.Api.Models.Auth;

public class UsuarioAuthModel
{
    public long IdUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public string Perfil { get; set; } = string.Empty;
    public int TentativasLogin { get; set; }
}
