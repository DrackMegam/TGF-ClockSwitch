using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    // Esta clase de aquí representa la información que recogerá de una tabla.

    public class PersonaDto
    {
        [MaxLength(3)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }
    }
}
