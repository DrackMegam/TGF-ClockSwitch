using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    // Esta clase de aquí representa la información que recogerá de una tabla.

    public class PersonaDto
    {
        [Key]
        [MaxLength(9)]
        public string Dni { get; set; } // No puede ser nulo.

        [MaxLength(50)]
        public string? Nombre { get; set; }

        [MaxLength(100)]
        public string? Apellidos { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; }

        [MaxLength(9)]
        public int? Telefono { get; set; }

        [MaxLength(100)]
        public string? Localidad { get; set; }
    }
}
