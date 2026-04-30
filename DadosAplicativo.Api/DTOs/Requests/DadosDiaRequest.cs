namespace DadosAplicativo.Api.DTOs.Requests;

public class DadosDiaRequest
{
    public DateTime Data { get; set; }
    public decimal ValorAlimentacao { get; set; }
    public double TotalKmRodado { get; set; }
    public double MediaConsumo { get; set; }
    public decimal ValorMedioCombustivel { get; set; }
    public decimal ValorAbastecido { get; set; }
}
