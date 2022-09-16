using ApiREST_Gestion.Dtos;
using ApiREST_Gestion.Modelos;
using ApiREST_Gestion.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiREST_Gestion.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        protected ResponseDto _response;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _response = new ResponseDto();
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult>Registrar(UsuarioDto usuarioDto)
        {
            var respuesta = await _usuarioRepositorio
                            .Registrar(new Usuario { NombreUsusario = usuarioDto.NombreUsuario }, usuarioDto.Password);
            if(respuesta == -1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ya existe el Usuario";
                return BadRequest(_response);
            }

            if (respuesta == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el Usuario";
                return BadRequest(_response);
            }

            _response.DisplayMessage = "Usuario creado con exito";
            _response.Result = respuesta;
            return Ok(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UsuarioDto usuarioDto)
        {
            var respuesta = await _usuarioRepositorio.Login(usuarioDto.NombreUsuario, usuarioDto.Password);

            if(respuesta == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "El usuario no existe";
                return BadRequest(_response);
            }
            if(respuesta == "claveIncorrecta")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Ha ingresado la clave incorrecta";
                return BadRequest(_response);
            }

            _response.Result = respuesta;
            _response.DisplayMessage = "Usuario Conectado";
            return Ok(_response);
        } 
    }
}
