using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    public class SuscripcionDto
    {
        [Key]
        [MaxLength(5)]
        public int Id { get; set; }

        [MaxLength(5)]
        public int IdSuscriptor { get; set; }

        [MaxLength(5)]
        public int IdObjetivo { get; set; }

    }
}
