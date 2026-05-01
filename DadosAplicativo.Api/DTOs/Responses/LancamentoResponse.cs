namespace DadosAplicativo.Api.DTOs.Responses;

public class LancamentoResponse
{
    public int Id { get; set; }
    public int IdDadosDoDia { get; set; }
    public DateTime Data { get; set; }
    public int IdEmpresa { get; set; }
    public string NomeEmpresa { get; set; } = string.Empty;
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public decimal ValorCorridas { get; set; }
}
