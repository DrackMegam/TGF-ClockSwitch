using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    public class UsuarioDto
    {
        [Key]
        [MaxLength(5)]
        public int IdUsuario { get; set; }

        [MaxLength(9)]
        public string? DniPersona { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(1)]
        public int IsAdmin { get; set; }

    }
}
