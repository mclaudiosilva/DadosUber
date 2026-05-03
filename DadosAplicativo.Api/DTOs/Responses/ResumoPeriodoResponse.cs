namespace DadosAplicativo.Api.DTOs.Responses;

public class ResumoPeriodoResponse
{
    public DateTime Data { get; set; }
    public decimal TotalCorridas { get; set; }
    public decimal ValorAlimentacao { get; set; }
    public decimal ValorAbastecido { get; set; }
    public double TotalKmRodado { get; set; }
    public double MediaConsumo { get; set; }
    public decimal ValorMedioCombustivel { get; set; }
    public decimal LitrosConsumidos { get; set; }
    public decimal GastoCombustivelReal { get; set; }
    public decimal LucroRealEstimado { get; set; }
    public decimal TotalHorasTrabalhadas { get; set; }
    public decimal GanhoPorHora => TotalHorasTrabalhadas <= 0 ? 0 : LucroRealEstimado / TotalHorasTrabalhadas;
}
