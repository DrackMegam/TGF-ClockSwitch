using System.ComponentModel.DataAnnotations;

namespace ClockSwitch_Backend.DTO
{
    public class HorasGlobalesDto
    {
        [MaxLength(5)]
        public int IdUsuario { get; set; }

        public string? NombreUsuario { get; set; }

        public double? SumatorioLunes { get; set; }

        public double? SumatorioMartes { get; set; }

        public double? SumatorioMiercoles { get; set; }

        public double? SumatorioJueves { get; set; }

        public double? SumatorioViernes { get; set; }

        public double? SumatorioSabado { get; set; }

        public double? SumatorioDomingo { get; set; }

        public double? Total { get; set; }
    }
}
