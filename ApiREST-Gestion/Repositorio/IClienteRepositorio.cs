using ApiREST_Gestion.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiREST_Gestion.Repositorio
{
    public interface IClienteRepositorio
    {
        Task<List<ClienteDto>> GetClientes();
        Task<ClienteDto> GetById(int id);
        Task<ClienteDto> CreateUpdate(ClienteDto clienteDto);
        Task<bool> Delete(int id);
    }
}
