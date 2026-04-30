using DadosAplicativo.Api.DTOs.Responses;
using DadosAplicativo.Api.Interfaces.Repositories;
using DadosAplicativo.Api.Interfaces.Services;

namespace DadosAplicativo.Api.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;

    public EmpresaService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public Task<IEnumerable<EmpresaResponse>> ListarEmpresasAsync()
    {
        return _empresaRepository.ListarEmpresasAsync();
    }
}
