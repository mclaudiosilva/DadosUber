namespace DadosAplicativo.Api.DTOs.Responses;

public class DadosDiaResponse
{
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public decimal ValorAlimentacao { get; set; }
    public double TotalKmRodado { get; set; }
    public double MediaConsumo { get; set; }
    public decimal ValorMedioCombustivel { get; set; }
    public decimal ValorAbastecido { get; set; }
    public decimal? HorasTrabalhadas { get; set; }
    public decimal LitrosConsumidos { get; set; }
    public decimal GastoCombustivelReal { get; set; }
    public decimal TotalCorridas { get; set; }
    public decimal LucroRealEstimado { get; set; }
}
