using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    public class HistorialFullDto
    {
        [Key]
        [MaxLength(9)]
        public int Id { get; set; }

        [MaxLength(9)]
        public int IdTarea { get; set; }

        [MaxLength(5)]
        public int IdUsuario { get; set; }

        public string? NombreTarea { get; set; }

        public string? EstadoTarea { get; set; }

        public double? HorasLunes { get; set; }

        public double? HorasMartes { get; set; }

        public double? HorasMiercoles { get; set; }

        public double? HorasJueves { get; set; }

        public double? HorasViernes { get; set; }

        public double? HorasSabado { get; set; }

        public double? HorasDomingo { get; set; }

        public int Ano { get; set; }

        public int Semana { get; set; }
    }
}
