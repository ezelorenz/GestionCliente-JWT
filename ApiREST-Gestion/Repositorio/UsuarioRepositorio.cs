using ApiREST_Gestion.Data;
using ApiREST_Gestion.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ApiREST_Gestion.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly AplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioRepositorio(AplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> ExisteUsuario(string nombreUsuario)
        {
            if(await _context.Usuarios.AnyAsync(x=> x.NombreUsusario.ToLower().Equals(nombreUsuario.ToLower())))
            {
                return true;
            }
            return false;
        }

        public async Task<string> Login(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(
                            x => x.NombreUsusario.ToLower().Equals(nombreUsuario.ToLower()));

            if (usuario == null)
            {
                return "nouser";
            }
            else if(!VerificarPasswordHash(password,usuario.PasswordHash, usuario.PasswordSalt))
            {
                return "claveIncorrecta";
            }
            else
            {
                return CrearToken(usuario);
            }
        }

        public async Task<int> Registrar(Usuario usuario, string password)
        {
            try
            {
                if( await ExisteUsuario(usuario.NombreUsusario))
                {
                    return -1;
                }

                CrearPassword(password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return usuario.Id;
            }
            catch(Exception)
            {
                return -500;
            }
        }

        //Encargado de encriptar las claves
        private void CrearPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var verificacion = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i =0; i< verificacion.Length; i++)
                {
                    if(verificacion[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsusario)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescripcion = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripcion);

            return tokenHandler.WriteToken(token);
        }
    }
}
