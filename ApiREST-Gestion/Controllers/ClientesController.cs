using ApiREST_Gestion.Dtos;
using ApiREST_Gestion.Modelos;
using ApiREST_Gestion.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiREST_Gestion.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDto _response;
        public ClientesController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _response = new ResponseDto();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> Get()
        {
            try
            {
                var clientes = await _clienteRepositorio.GetClientes();
                _response.Result= clientes;
                _response.DisplayMessage = "Lista de Clientes";
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<ActionResult<Cliente>>GetById(int id, string? name)
        {
            var cliente = await _clienteRepositorio.GetById(id);
            if (cliente == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No existe el cliente";
                return NotFound(_response);
            }
            _response.DisplayMessage = "Informacion del Cliente";
            _response.Result = cliente;
            
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>>Post(ClienteDto clienteDto)
        {
            try 
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = model;
                return new CreatedAtRouteResult("GetCliente", new { id = model.Id }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Grabar el Registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,ClienteDto clienteDto)
        {
            try
            {
                ClienteDto model = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = model;
                return Ok(model);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool estaEliminado = await _clienteRepositorio.Delete(id);
                if (estaEliminado)
                {
                    _response.Result = estaEliminado;
                    _response.DisplayMessage = "Cliente Eliminado con Exito";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar Cliente";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
