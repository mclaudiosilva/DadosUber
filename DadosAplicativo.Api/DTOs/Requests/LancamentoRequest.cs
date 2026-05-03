namespace DadosAplicativo.Api.DTOs.Requests;

public class LancamentoRequest
{
    public int IdDadosDoDia { get; set; }
    public DateTime Data { get; set; }
    public int IdEmpresa { get; set; }
    public TimeSpan HorarioInicio { get; set; }
    public TimeSpan HorarioFim { get; set; }
    public decimal ValorCorridas { get; set; }
}
