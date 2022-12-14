using System.ComponentModel.DataAnnotations;

namespace ApiREST_Gestion.Modelos
{
    public class Cliente
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Telefono { get; set; }
    }
}
