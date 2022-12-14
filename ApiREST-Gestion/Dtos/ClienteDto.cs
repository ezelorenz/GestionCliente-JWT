using System.ComponentModel.DataAnnotations;

namespace ApiREST_Gestion.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        
        public string Nombre { get; set; }
        
        public string Apellidos { get; set; }
        
        public string Direccion { get; set; }
        
        public string Telefono { get; set; }
    }
}
