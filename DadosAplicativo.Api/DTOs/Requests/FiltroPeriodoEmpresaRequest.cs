namespace DadosAplicativo.Api.DTOs.Requests;

public class FiltroPeriodoEmpresaRequest
{
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public List<int>? IdsEmpresas { get; set; }
}
