using System.ComponentModel.DataAnnotations;

namespace ApiREST_Gestion.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string NombreUsusario { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
