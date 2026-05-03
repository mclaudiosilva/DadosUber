namespace DadosAplicativo.Api.DTOs.Responses;

public class LoginResponse
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; } = string.Empty;
    public UsuarioLogadoResponse? Usuario { get; set; }
}
