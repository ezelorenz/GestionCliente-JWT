using ApiREST_Gestion.Data;
using ApiREST_Gestion.Dtos;
using ApiREST_Gestion.Modelos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiREST_Gestion.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClienteRepositorio(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetClientes()
        {
            List<Cliente> lista = await _context.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(lista);
        }

        public async Task<ClienteDto> GetById(int id)
        {
            Cliente cliente = await _context.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> CreateUpdate(ClienteDto clienteDto)
        {
            Cliente cliente = _mapper.Map<ClienteDto, Cliente>(clienteDto);
            if(cliente.Id > 0)
            {
                _context.Clientes.Update(cliente);
            }
            else
            {
                await _context.Clientes.AddAsync(cliente);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<Cliente, ClienteDto>(cliente);
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                Cliente cliente = await _context.Clientes.FindAsync(id);
                if(cliente == null)
                {
                    return false;
                }
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        
    }
}
