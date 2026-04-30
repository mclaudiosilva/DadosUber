namespace DadosAplicativo.Api.DTOs.Responses;

public class DashboardPeriodoResponse
{
    public int DiasTrabalhados { get; set; }
    public decimal TotalCorridas { get; set; }
    public decimal TotalAlimentacao { get; set; }
    public decimal TotalAbastecido { get; set; }
    public double TotalKmRodado { get; set; }
    public decimal LitrosConsumidos { get; set; }
    public decimal GastoCombustivelReal { get; set; }
    public decimal LucroRealEstimado { get; set; }
    public decimal GanhoPorKm { get; set; }
}
